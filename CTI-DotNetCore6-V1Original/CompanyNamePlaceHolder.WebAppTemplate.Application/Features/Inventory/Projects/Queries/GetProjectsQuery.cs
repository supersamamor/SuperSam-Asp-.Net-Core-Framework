using CTI.Common.Utility.Models;
using CompanyNamePlaceHolder.WebAppTemplate.Application.Common;
using CompanyNamePlaceHolder.WebAppTemplate.Core.Inventory;
using CompanyNamePlaceHolder.WebAppTemplate.Infrastructure.Data;
using MediatR;

namespace CompanyNamePlaceHolder.WebAppTemplate.Application.Features.Inventory.Projects.Queries;

public record GetProjectsQuery : BaseQuery, IRequest<PagedListResponse<ProjectState>>;

public class GetProjectsQueryHandler : BaseQueryHandler<ApplicationContext, ProjectState, GetProjectsQuery>, IRequestHandler<GetProjectsQuery, PagedListResponse<ProjectState>>
{
    public GetProjectsQueryHandler(ApplicationContext context) : base(context)
    {
    }
}
