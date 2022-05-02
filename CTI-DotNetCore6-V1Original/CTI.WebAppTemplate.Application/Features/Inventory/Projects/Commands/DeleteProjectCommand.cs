using AutoMapper;
using CTI.WebAppTemplate.Application.Common;
using CTI.WebAppTemplate.Core.Inventory;
using CTI.WebAppTemplate.Infrastructure.Data;
using CTI.WebAppTemplate.Infrastructure.Extensions;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using static LanguageExt.Prelude;

namespace CTI.WebAppTemplate.Application.Features.Inventory.Projects.Commands;

public record DeleteProjectCommand : BaseCommand, IRequest<Validation<Error, ProjectState>>;

public class DeleteProjectCommandHandler : BaseCommandHandler<ApplicationContext, ProjectState, DeleteProjectCommand>, IRequestHandler<DeleteProjectCommand, Validation<Error, ProjectState>>
{
    public DeleteProjectCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteProjectCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ProjectState>> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _context.GetSingle<ProjectState>(p => p.Id == request.Id, cancellationToken, true);
        return await project.MatchAsync(
            Some: async p => await Delete(p, cancellationToken),
            None: () => Fail<Error, ProjectState>($"Project not found"));
    }
}
