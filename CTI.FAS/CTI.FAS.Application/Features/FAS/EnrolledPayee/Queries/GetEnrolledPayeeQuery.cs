using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.FAS.Core.FAS;
using CTI.FAS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;
using CTI.Common.Identity.Abstractions;

namespace CTI.FAS.Application.Features.FAS.EnrolledPayee.Queries;

public record GetEnrolledPayeeQuery : BaseQuery, IRequest<PagedListResponse<EnrolledPayeeState>>;

public class GetEnrolledPayeeQueryHandler : BaseQueryHandler<ApplicationContext, EnrolledPayeeState, GetEnrolledPayeeQuery>, IRequestHandler<GetEnrolledPayeeQuery, PagedListResponse<EnrolledPayeeState>>
{
    private readonly IAuthenticatedUser _authenticatedUser;
    private readonly IdentityContext _identityContext;
    public GetEnrolledPayeeQueryHandler(ApplicationContext context, IAuthenticatedUser authenticatedUser, IdentityContext identityContext) : base(context)
    {
        _authenticatedUser = authenticatedUser;
        _identityContext = identityContext;
    }

    public override async Task<PagedListResponse<EnrolledPayeeState>> Handle(GetEnrolledPayeeQuery request, CancellationToken cancellationToken = default)
    {
        var query = (from creditor in Context.EnrolledPayee
                     select creditor)
                            .AsNoTracking();

        if (_authenticatedUser.ClaimsPrincipal != null && !_authenticatedUser.ClaimsPrincipal.IsInRole(Core.Constants.Roles.Admin))
        {
            var pplusUserId = await _identityContext.Users.Where(l => l.Id == _authenticatedUser.UserId).AsNoTracking().Select(l => l.PplusId).FirstOrDefaultAsync(cancellationToken: cancellationToken);
            query = from a in query
                    join c in Context.Company on a.CompanyId equals c.Id
                    join ue in Context.UserEntity on c.Id equals ue.CompanyId
                    where ue.PplusUserId == _authenticatedUser.UserId && c.IsDisabled == false
                    select a;
        }
        return await query.Include(l => l.Company).ThenInclude(l => l!.DatabaseConnectionSetup).Include(l => l.Creditor)
                    .AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
                        request.SortColumn, request.SortOrder,
                        request.PageNumber, request.PageSize,
                        cancellationToken);
    }                                                                                
}
