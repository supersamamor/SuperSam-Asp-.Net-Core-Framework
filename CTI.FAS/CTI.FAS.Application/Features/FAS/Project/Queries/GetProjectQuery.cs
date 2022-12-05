using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.FAS.Core.FAS;
using CTI.FAS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;
using CTI.Common.Identity.Abstractions;

namespace CTI.FAS.Application.Features.FAS.Project.Queries;

public record GetProjectQuery : BaseQuery, IRequest<PagedListResponse<ProjectState>>;

public class GetProjectQueryHandler : BaseQueryHandler<ApplicationContext, ProjectState, GetProjectQuery>, IRequestHandler<GetProjectQuery, PagedListResponse<ProjectState>>
{   
    private readonly IAuthenticatedUser _authenticatedUser;
    private readonly IdentityContext _identityContext;
    public GetProjectQueryHandler(ApplicationContext context, IAuthenticatedUser authenticatedUser, IdentityContext identityContext) : base(context)
    {
        _authenticatedUser = authenticatedUser;
        _identityContext = identityContext;
    }
    public override async Task<PagedListResponse<ProjectState>> Handle(GetProjectQuery request, CancellationToken cancellationToken = default)
	{
        var query = (from project in Context.Project
                     select project)
                               .AsNoTracking();

        if (_authenticatedUser.ClaimsPrincipal != null && !_authenticatedUser.ClaimsPrincipal.IsInRole(Core.Constants.Roles.Admin))
        {
            var pplusUserId = await _identityContext.Users.Where(l => l.Id == _authenticatedUser.UserId).AsNoTracking().Select(l => l.PplusId).FirstOrDefaultAsync();
            query = from a in query
                    join ue in Context.UserEntity on a.Id equals ue.CompanyId
                    join c in Context.Company on ue.CompanyId equals c.Id
                    where ue.PplusUserId == _authenticatedUser.UserId && a.IsDisabled == false && c.IsDisabled == false
                    select a;
        }
        return await query.Include(l => l.Company)
                    .AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
                        request.SortColumn, request.SortOrder,
                        request.PageNumber, request.PageSize,
                        cancellationToken);
    }
		
}
