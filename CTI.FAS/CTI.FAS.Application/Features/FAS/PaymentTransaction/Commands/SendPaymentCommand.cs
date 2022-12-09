using CTI.Common.Identity.Abstractions;
using CTI.FAS.Application.Services;
using CTI.FAS.Core.Constants;
using CTI.FAS.Core.FAS;
using CTI.FAS.CsvGenerator.Services;
using CTI.FAS.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using static LanguageExt.Prelude;

namespace CTI.FAS.Application.Features.FAS.PaymentTransaction.Commands;

public record SendPaymentCommand(IList<string> NewPaymentTransactionIdList, Microsoft.AspNetCore.Mvc.ActionContext PageContext) : IRequest<string>;

public class SendPaymentCommandHandler : IRequestHandler<SendPaymentCommand, string>
{
    private readonly ApplicationContext _context;
    private readonly IAuthenticatedUser _authenticatedUser;
    private readonly string _staticFolderPath;
    private readonly IdentityContext _identityContext;
    public SendPaymentCommandHandler(ApplicationContext context,
        IAuthenticatedUser authenticatedUser, IConfiguration configuration, IdentityContext identityContext)
    {
        _context = context;
        _authenticatedUser = authenticatedUser;
        _staticFolderPath = configuration.GetValue<string>("UsersUpload:UploadFilesPath");
        _identityContext = identityContext;
    }

    public async Task<string> Handle(SendPaymentCommand request, CancellationToken cancellationToken) =>
        await AddEnrolledPayee(request, cancellationToken);


    public async Task<string> AddEnrolledPayee(SendPaymentCommand request, CancellationToken cancellationToken)
    {
        var paymentStatus = PaymentTransactionStatus.Sent;
        var date = DateTime.Now.Date;
        var paymentTransactionToProcessList = await _context.PaymentTransaction
            .Where(l => request.NewPaymentTransactionIdList.Contains(l.Id)).AsNoTracking().ToListAsync(cancellationToken);
        var batchCount = (await _context.Batch
                           .Where(l => l.Date == date && l.BatchStatusType == paymentStatus)
                           .AsNoTracking().CountAsync(cancellationToken: cancellationToken)) + 1;
        var companyId = (await _context.PaymentTransaction
            .Where(l => l.Id == request.NewPaymentTransactionIdList.FirstOrDefault()).Include(l => l.EnrolledPayee).AsNoTracking().FirstOrDefaultAsync(cancellationToken: cancellationToken))!
            .EnrolledPayee!.CompanyId;
        var batchToAdd = new BatchState()
        {
            Date = date,
            Batch = batchCount,
            FilePath = "",
            Url = "",
            UserId = _authenticatedUser.UserId,
            CompanyId = companyId,
            BatchStatusType = paymentStatus,
        };
        await _context.AddAsync(batchToAdd, cancellationToken);
        //Todo: Use Bulk Update
        foreach (var item in paymentTransactionToProcessList)
        {
            var group = await _identityContext.Group.Where(l => l.Id == item.GroupCode).AsNoTracking().FirstOrDefaultAsync(cancellationToken: cancellationToken);
            var rotativaService = new RotativaService<string>("", "PaymentTransaction\\Pdf\\ESettle", $"ESettle_{MakeValidFileName(item.DocumentNumber)}_{DateTime.Now:MMddyyyy_HHmm}.pdf",
                                                          GlobalConstants.UploadFilesPath, _staticFolderPath, "ESettle");
            var rotativaDocument = await rotativaService.GeneratePDFAsync(request.PageContext);
            item.TagAsSent(batchToAdd.Id, _authenticatedUser?.UserId, rotativaDocument.FileUrl, rotativaDocument.CompleteFilePath);
        }
        _context.UpdateRange(paymentTransactionToProcessList);
        _ = await _context.SaveChangesAsync(cancellationToken);
        return "";
    }
    private static string MakeValidFileName(string name)
    {
        string invalidChars = System.Text.RegularExpressions.Regex.Escape(new string(System.IO.Path.GetInvalidFileNameChars()));
        string invalidRegStr = string.Format(@"([{0}]*\.+$)|([{0}]+)", invalidChars);
        return System.Text.RegularExpressions.Regex.Replace(name, invalidRegStr, "_");
    }
}
