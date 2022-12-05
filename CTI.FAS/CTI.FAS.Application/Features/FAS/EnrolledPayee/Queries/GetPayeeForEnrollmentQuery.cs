using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.FAS.Core.FAS;
using CTI.FAS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;
using CTI.Common.Identity.Abstractions;
using CTI.FAS.Core.Constants;

namespace CTI.FAS.Application.Features.FAS.EnrolledPayee.Queries;

public record GetPayeeForEnrollmentQuery : BaseQuery, IRequest<PagedListResponse<EnrolledPayeeState>>;

public class GetPayeeForEnrollmentQueryHandler : BaseQueryHandler<ApplicationContext, EnrolledPayeeState, GetPayeeForEnrollmentQuery>, IRequestHandler<GetPayeeForEnrollmentQuery, PagedListResponse<EnrolledPayeeState>>
{
    private readonly IAuthenticatedUser _authenticatedUser;
    private readonly IdentityContext _identityContext;
    public GetPayeeForEnrollmentQueryHandler(ApplicationContext context, IAuthenticatedUser authenticatedUser, IdentityContext identityContext) : base(context)
    {
        _authenticatedUser = authenticatedUser;
        _identityContext = identityContext;
    }

    public override async Task<PagedListResponse<EnrolledPayeeState>> Handle(GetPayeeForEnrollmentQuery request, CancellationToken cancellationToken = default)
    {
        var query = (from creditor in Context.EnrolledPayee
                     select creditor)
                            .AsNoTracking();

        if (_authenticatedUser.ClaimsPrincipal != null && !_authenticatedUser.ClaimsPrincipal.IsInRole(Core.Constants.Roles.Admin))
        {          
            query = from a in query                   
                    where a.CreatedBy == _authenticatedUser.UserId && a.Status == EnrollmentStatus.New
                    select a;
        }
        return await query.Include(l => l.Company).ThenInclude(l => l!.DatabaseConnectionSetup).Include(l => l.Creditor)
                    .AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
                        request.SortColumn, request.SortOrder,
                        request.PageNumber, request.PageSize,
                        cancellationToken);
    }                                                                                
}
