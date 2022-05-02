using CTI.WebAppTemplate.Application.Common;
using CTI.WebAppTemplate.Core.Inventory;
using CTI.WebAppTemplate.Infrastructure.Data;
using LanguageExt;
using MediatR;

namespace CTI.WebAppTemplate.Application.Features.Inventory.Projects.Queries;

public record GetProjectByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<ProjectState>>;

public class GetProjectByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, ProjectState, GetProjectByIdQuery>, IRequestHandler<GetProjectByIdQuery, Option<ProjectState>>
{
    public GetProjectByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
}
