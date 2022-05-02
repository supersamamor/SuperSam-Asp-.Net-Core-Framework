using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Common;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.Inventory;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using LanguageExt;
using MediatR;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.Inventory.Projects.Queries;

public record GetProjectByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<ProjectState>>;

public class GetProjectByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, ProjectState, GetProjectByIdQuery>, IRequestHandler<GetProjectByIdQuery, Option<ProjectState>>
{
    public GetProjectByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
}
