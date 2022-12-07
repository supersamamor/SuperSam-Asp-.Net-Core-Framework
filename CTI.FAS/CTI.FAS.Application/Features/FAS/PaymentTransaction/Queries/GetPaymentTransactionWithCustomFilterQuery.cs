using CTI.FAS.Core.FAS;
using CTI.FAS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;
using CTI.Common.Identity.Abstractions;

namespace CTI.FAS.Application.Features.FAS.PaymentTransaction.Queries;

public record GetPaymentTransactionWithCustomFilterQuery : IRequest<IList<PaymentTransactionState>>
{
    public string? Status { get; set; }
    public string? Entity { get; set; }
    public string? PaymentType { get; set; }
    public string? AccountTransaction { get; set; }
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
    public string? BatchId { get; set; }
}

public class GetPaymentTransactionWithCustomFilterQueryHandler : IRequestHandler<GetPaymentTransactionWithCustomFilterQuery, IList<PaymentTransactionState>>
{
    private readonly ApplicationContext _context;
    private readonly IAuthenticatedUser _authenticatedUser;
    private readonly IdentityContext _identityContext;
    public GetPaymentTransactionWithCustomFilterQueryHandler(ApplicationContext context, IAuthenticatedUser authenticatedUser, IdentityContext identityContext)
    {
        _context = context;
        _authenticatedUser = authenticatedUser;
        _identityContext = identityContext;
    }

    public async Task<IList<PaymentTransactionState>> Handle(GetPaymentTransactionWithCustomFilterQuery request, CancellationToken cancellationToken = default)
    {
        var query = (from paymentTransaction in _context.PaymentTransaction
                     select paymentTransaction)
                            .AsNoTracking();
        if (!string.IsNullOrEmpty(request.Status))
        {
            query = query.Where(l => l.Status == request.Status);
        }
        if (!string.IsNullOrEmpty(request.Entity))
        {
            query = query.Where(l => l.EnrolledPayee!.CompanyId == request.Entity);
        }
        if (!string.IsNullOrEmpty(request.PaymentType))
        {
            query = query.Where(l => l.PaymentType == request.PaymentType);
        }
        if (!string.IsNullOrEmpty(request.AccountTransaction))
        {
            query = query.Where(l => l.AccountTransaction == request.AccountTransaction);
        }
        if (request.DateFrom != null && request.DateFrom != DateTime.MinValue)
        {
            query = query.Where(l => l.DocumentDate >= request.DateFrom);
        }
        if (request.DateTo != null && request.DateTo != DateTime.MinValue)
        {
            query = query.Where(l => l.DocumentDate <= request.DateTo);
        }
        if (_authenticatedUser.ClaimsPrincipal != null && !_authenticatedUser.ClaimsPrincipal.IsInRole(Core.Constants.Roles.Admin))
        {
            var pplusUserId = await _identityContext.Users.Where(l => l.Id == _authenticatedUser.UserId).AsNoTracking().Select(l => l.PplusId).FirstOrDefaultAsync(cancellationToken: cancellationToken);
            query = from a in query
                    join ue in _context.UserEntity on a.EnrolledPayee!.CompanyId equals ue.CompanyId
                    where ue.PplusUserId == pplusUserId && a.EnrolledPayee!.Company!.IsDisabled == false
                    select a;
        } 
        return await query.Include(l => l.Batch).Include(l => l!.EnrolledPayee).ThenInclude(l => l!.Creditor)
                    .AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
    }
}
