using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;
using CTI.Common.Identity.Abstractions;

namespace CTI.ELMS.Application.Features.ELMS.Project.Queries;

public record GetProjectQuery : BaseQuery, IRequest<PagedListResponse<ProjectState>>;

public class GetProjectQueryHandler : BaseQueryHandler<ApplicationContext, ProjectState, GetProjectQuery>, IRequestHandler<GetProjectQuery, PagedListResponse<ProjectState>>
{
    private readonly IAuthenticatedUser _authenticatedUser;
    public GetProjectQueryHandler(ApplicationContext context, IAuthenticatedUser authenticatedUser) : base(context)
    {
        _authenticatedUser = authenticatedUser;
    }
    public override async Task<PagedListResponse<ProjectState>> Handle(GetProjectQuery request, CancellationToken cancellationToken = default)
    {
        var query = (from project in Context.Project
                     select project).AsNoTracking(); 
        if (_authenticatedUser.ClaimsPrincipal != null && !_authenticatedUser.ClaimsPrincipal.IsInRole(Core.Constants.Roles.Admin))
        {
            query = from a in query       
                    join ap in Context.UserProjectAssignment on a.Id equals ap.ProjectID
                    where ap.UserId == _authenticatedUser.UserId
                    select a;
        }
        return await query.Include(l => l.EntityGroup).ThenInclude(l => l!.PPlusConnectionSetup)
                           .ToPagedResponse(request.SearchColumns, request.SearchValue,
                           request.SortColumn, request.SortOrder,
                           request.PageNumber, request.PageSize,
                           cancellationToken);
    }

}
