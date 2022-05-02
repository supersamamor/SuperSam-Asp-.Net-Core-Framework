using CTI.Common.Utility.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Common;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.Inventory;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using MediatR;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.Inventory.Projects.Queries;

public record GetProjectsQuery : BaseQuery, IRequest<PagedListResponse<ProjectState>>;

public class GetProjectsQueryHandler : BaseQueryHandler<ApplicationContext, ProjectState, GetProjectsQuery>, IRequestHandler<GetProjectsQuery, PagedListResponse<ProjectState>>
{
    public GetProjectsQueryHandler(ApplicationContext context) : base(context)
    {
    }
}
