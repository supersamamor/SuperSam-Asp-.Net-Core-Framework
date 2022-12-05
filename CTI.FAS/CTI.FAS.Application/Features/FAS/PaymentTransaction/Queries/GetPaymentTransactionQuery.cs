using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.FAS.Core.FAS;
using CTI.FAS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;
using CTI.Common.Identity.Abstractions;

namespace CTI.FAS.Application.Features.FAS.PaymentTransaction.Queries;

public record GetPaymentTransactionQuery : BaseQuery, IRequest<PagedListResponse<PaymentTransactionState>>;

public class GetPaymentTransactionQueryHandler : BaseQueryHandler<ApplicationContext, PaymentTransactionState, GetPaymentTransactionQuery>, IRequestHandler<GetPaymentTransactionQuery, PagedListResponse<PaymentTransactionState>>
{
    private readonly IAuthenticatedUser _authenticatedUser;
    private readonly IdentityContext _identityContext;
    public GetPaymentTransactionQueryHandler(ApplicationContext context, IAuthenticatedUser authenticatedUser, IdentityContext identityContext) : base(context)
    {
        _authenticatedUser = authenticatedUser;
        _identityContext = identityContext;
    }
   
	public override async Task<PagedListResponse<PaymentTransactionState>> Handle(GetPaymentTransactionQuery request, CancellationToken cancellationToken = default)
    {
        var query = (from paymentTransaction in Context.PaymentTransaction
                     select paymentTransaction)
                            .AsNoTracking();

        if (_authenticatedUser.ClaimsPrincipal != null && !_authenticatedUser.ClaimsPrincipal.IsInRole(Core.Constants.Roles.Admin))
        {
            var pplusUserId = await _identityContext.Users.Where(l => l.Id == _authenticatedUser.UserId).AsNoTracking().Select(l => l.PplusId).FirstOrDefaultAsync(cancellationToken: cancellationToken);
            query = from a in query
                    join ep in Context.EnrolledPayee on a.EnrolledPayeeId equals ep.Id
                    join c in Context.Company on ep.CompanyId equals c.Id
                    join ue in Context.UserEntity on c.Id equals ue.CompanyId
                    where ue.PplusUserId == pplusUserId && c.IsDisabled == false
                    select a;
        }
        return await query.Include(l => l.Batch).Include(l => l!.EnrolledPayee)
                    .AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
                        request.SortColumn, request.SortOrder,
                        request.PageNumber, request.PageSize,
                        cancellationToken);
    } 
}
