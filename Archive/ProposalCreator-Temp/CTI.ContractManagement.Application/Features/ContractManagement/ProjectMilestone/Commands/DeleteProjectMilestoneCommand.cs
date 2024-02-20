using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.ContractManagement.Core.ContractManagement;
using CTI.ContractManagement.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;

namespace CTI.ContractManagement.Application.Features.ContractManagement.ProjectMilestone.Commands;

public record DeleteProjectMilestoneCommand : BaseCommand, IRequest<Validation<Error, ProjectMilestoneState>>;

public class DeleteProjectMilestoneCommandHandler : BaseCommandHandler<ApplicationContext, ProjectMilestoneState, DeleteProjectMilestoneCommand>, IRequestHandler<DeleteProjectMilestoneCommand, Validation<Error, ProjectMilestoneState>>
{
    public DeleteProjectMilestoneCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteProjectMilestoneCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ProjectMilestoneState>> Handle(DeleteProjectMilestoneCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteProjectMilestoneCommandValidator : AbstractValidator<DeleteProjectMilestoneCommand>
{
    readonly ApplicationContext _context;

    public DeleteProjectMilestoneCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ProjectMilestoneState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ProjectMilestone with id {PropertyValue} does not exists");
    }
}
