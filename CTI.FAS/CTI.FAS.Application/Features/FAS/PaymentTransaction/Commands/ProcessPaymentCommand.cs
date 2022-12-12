using CTI.Common.Identity.Abstractions;
using CTI.FAS.Core.Constants;
using CTI.FAS.Core.FAS;
using CTI.FAS.CsvGenerator.Services;
using CTI.FAS.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.FAS.Application.Features.FAS.PaymentTransaction.Commands;

public record ProcessPaymentCommand(IList<string> NewPaymentTransactionIdList, string BankId) : IRequest<string>;

public class ProcessPaymentCommandHandler : IRequestHandler<ProcessPaymentCommand, string>
{
    private readonly ApplicationContext _context;
    private readonly PaymentTransactionCsvService _paymentTransactionCsvService;
    private readonly IAuthenticatedUser _authenticatedUser;
    private readonly IdentityContext _identityContext;
    public ProcessPaymentCommandHandler(ApplicationContext context, PaymentTransactionCsvService paymentTransactionCsvService,
        IAuthenticatedUser authenticatedUser, IdentityContext identityContext)
    {
        _context = context;
        _paymentTransactionCsvService = paymentTransactionCsvService;
        _authenticatedUser = authenticatedUser;
        _identityContext = identityContext;
    }

    public async Task<string> Handle(ProcessPaymentCommand request, CancellationToken cancellationToken) =>
        await AddEnrolledPayee(request, cancellationToken);


    public async Task<string> AddEnrolledPayee(ProcessPaymentCommand request, CancellationToken cancellationToken)
    {
        var paymentStatus = PaymentTransactionStatus.Generated;
        var date = DateTime.Now.Date;
        var paymentTransactionToProcessList = await _context.PaymentTransaction
            .Include(l => l.EnrolledPayee).ThenInclude(l => l!.Creditor)
            .Include(l => l.EnrolledPayee).ThenInclude(l => l!.Company)
            .Where(l => request.NewPaymentTransactionIdList.Contains(l.Id)).AsNoTracking().ToListAsync(cancellationToken);
        var entityCode = paymentTransactionToProcessList.FirstOrDefault()?.EnrolledPayee?.Company?.Code;
        var batchCount = (await _context.Batch
                             .Where(l => l.Date == date && l.BatchStatusType == paymentStatus)
                             .AsNoTracking().CountAsync(cancellationToken: cancellationToken)) + 1;
        var companyId = (await _context.PaymentTransaction
            .Where(l => l.Id == request.NewPaymentTransactionIdList.FirstOrDefault()).Include(l => l.EnrolledPayee)
            .AsNoTracking().FirstOrDefaultAsync(cancellationToken: cancellationToken))!
            .EnrolledPayee!.CompanyId;
        var csvDocument = _paymentTransactionCsvService.Export(paymentTransactionToProcessList, entityCode, batchCount, _authenticatedUser.UserId!);
        var groupId = (await _identityContext.Users.Where(l => l.Id == _authenticatedUser.UserId).AsNoTracking().FirstOrDefaultAsync(cancellationToken: cancellationToken))?.GroupId;
        var batchToAdd = new BatchState()
        {
            Date = date,
            Batch = batchCount,
            FilePath = csvDocument.CompleteFilePath,
            Url = csvDocument.FileUrl,
            UserId = _authenticatedUser.UserId,
            CompanyId = companyId,
            BatchStatusType = paymentStatus,
        };
        await _context.AddAsync(batchToAdd, cancellationToken);
        //Todo: Use Bulk Update
        foreach (var item in paymentTransactionToProcessList)
        {
            item.EnrolledPayee = null;
            item.TagAsGeneratedAndSetBatch(batchToAdd.Id, groupId, request.BankId);
        }
        _context.UpdateRange(paymentTransactionToProcessList);
        _ = await _context.SaveChangesAsync(cancellationToken);
        return csvDocument.FileUrl;
    }
}
