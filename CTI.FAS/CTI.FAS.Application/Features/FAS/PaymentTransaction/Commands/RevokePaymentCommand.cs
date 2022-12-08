using CTI.Common.Identity.Abstractions;
using CTI.FAS.CsvGenerator.Services;
using CTI.FAS.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.FAS.Application.Features.FAS.PaymentTransaction.Commands;

public record RevokePaymentCommand(IList<string> NewPaymentTransactionIdList) : IRequest<string>;

public class RevokePaymentCommandHandler : IRequestHandler<RevokePaymentCommand, string>
{
    private readonly ApplicationContext _context;
    private readonly PaymentTransactionCsvService _paymentTransactionCsvService;
    private readonly IAuthenticatedUser _authenticatedUser;
    public RevokePaymentCommandHandler(ApplicationContext context, PaymentTransactionCsvService paymentTransactionCsvService,
        IAuthenticatedUser authenticatedUser)
    {
        _context = context;
        _paymentTransactionCsvService = paymentTransactionCsvService;
        _authenticatedUser = authenticatedUser;
    }

    public async Task<string> Handle(RevokePaymentCommand request, CancellationToken cancellationToken) =>
        await AddEnrolledPayee(request, cancellationToken);


    public async Task<string> AddEnrolledPayee(RevokePaymentCommand request, CancellationToken cancellationToken)
    {
        var date = DateTime.Now.Date;
        var paymentTransactionToProcessList = await _context.PaymentTransaction
           .Include(l => l.EnrolledPayee).ThenInclude(l => l!.Creditor)
           .Include(l => l.EnrolledPayee).ThenInclude(l => l!.Company)
           .Where(l => request.NewPaymentTransactionIdList.Contains(l.Id)).AsNoTracking().ToListAsync(cancellationToken);
        var entityCode = paymentTransactionToProcessList.FirstOrDefault()?.EnrolledPayee?.Company?.Code;
        var batchCount = (await _context.Batch
                             .Where(l => l.Date == date)
                             .AsNoTracking().CountAsync(cancellationToken: cancellationToken)) + 1;
        var csvDocument = _paymentTransactionCsvService.Export(paymentTransactionToProcessList, entityCode, batchCount, _authenticatedUser.UserId!);
        var companyId = (await _context.PaymentTransaction
            .Where(l => l.Id == request.NewPaymentTransactionIdList.FirstOrDefault()).Include(l => l.EnrolledPayee).AsNoTracking().FirstOrDefaultAsync(cancellationToken: cancellationToken))!
            .EnrolledPayee!.CompanyId;
        //Todo: Use Bulk Update
        foreach (var item in paymentTransactionToProcessList)
        {
            item.TagAsRevoked();
        }
        _context.UpdateRange(paymentTransactionToProcessList);
        _ = await _context.SaveChangesAsync(cancellationToken);
        return csvDocument.FileUrl;
    }
}
