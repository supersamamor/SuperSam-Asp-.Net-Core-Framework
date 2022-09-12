using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.ContractManagement.Core.ContractManagement;
using CTI.ContractManagement.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.ContractManagement.Application.Features.ContractManagement.ProjectCategory.Queries;

public record GetProjectCategoryQuery : BaseQuery, IRequest<PagedListResponse<ProjectCategoryState>>;

public class GetProjectCategoryQueryHandler : BaseQueryHandler<ApplicationContext, ProjectCategoryState, GetProjectCategoryQuery>, IRequestHandler<GetProjectCategoryQuery, PagedListResponse<ProjectCategoryState>>
{
    public GetProjectCategoryQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
