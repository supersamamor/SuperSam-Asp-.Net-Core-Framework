using CTI.Common.Utility.Models;
using CTI.WebAppTemplate.Application.Common;
using CTI.WebAppTemplate.Core.Inventory;
using CTI.WebAppTemplate.Infrastructure.Data;
using MediatR;

namespace CTI.WebAppTemplate.Application.Features.Inventory.Projects.Queries;

public record GetProjectsQuery : BaseQuery, IRequest<PagedListResponse<ProjectState>>;

public class GetProjectsQueryHandler : BaseQueryHandler<ApplicationContext, ProjectState, GetProjectsQuery>, IRequestHandler<GetProjectsQuery, PagedListResponse<ProjectState>>
{
    public GetProjectsQueryHandler(ApplicationContext context) : base(context)
    {
    }
}
