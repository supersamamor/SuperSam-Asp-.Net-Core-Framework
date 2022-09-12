using CTI.Common.Core.Queries;
using CTI.ContractManagement.Core.ContractManagement;
using CTI.ContractManagement.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CTI.ContractManagement.Application.Features.ContractManagement.ProjectCategory.Queries;

public record GetProjectCategoryByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<ProjectCategoryState>>;

public class GetProjectCategoryByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, ProjectCategoryState, GetProjectCategoryByIdQuery>, IRequestHandler<GetProjectCategoryByIdQuery, Option<ProjectCategoryState>>
{
    public GetProjectCategoryByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
		
}
