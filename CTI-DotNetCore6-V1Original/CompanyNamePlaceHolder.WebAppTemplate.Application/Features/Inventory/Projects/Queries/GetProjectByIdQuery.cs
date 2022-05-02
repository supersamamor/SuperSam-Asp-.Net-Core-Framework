using CompanyNamePlaceHolder.WebAppTemplate.Application.Common;
using CompanyNamePlaceHolder.WebAppTemplate.Core.Inventory;
using CompanyNamePlaceHolder.WebAppTemplate.Infrastructure.Data;
using LanguageExt;
using MediatR;

namespace CompanyNamePlaceHolder.WebAppTemplate.Application.Features.Inventory.Projects.Queries;

public record GetProjectByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<ProjectState>>;

public class GetProjectByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, ProjectState, GetProjectByIdQuery>, IRequestHandler<GetProjectByIdQuery, Option<ProjectState>>
{
    public GetProjectByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
}
