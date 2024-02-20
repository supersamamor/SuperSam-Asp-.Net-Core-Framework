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

namespace CTI.ContractManagement.Application.Features.ContractManagement.ProjectMilestoneHistory.Commands;

public record DeleteProjectMilestoneHistoryCommand : BaseCommand, IRequest<Validation<Error, ProjectMilestoneHistoryState>>;

public class DeleteProjectMilestoneHistoryCommandHandler : BaseCommandHandler<ApplicationContext, ProjectMilestoneHistoryState, DeleteProjectMilestoneHistoryCommand>, IRequestHandler<DeleteProjectMilestoneHistoryCommand, Validation<Error, ProjectMilestoneHistoryState>>
{
    public DeleteProjectMilestoneHistoryCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteProjectMilestoneHistoryCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ProjectMilestoneHistoryState>> Handle(DeleteProjectMilestoneHistoryCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteProjectMilestoneHistoryCommandValidator : AbstractValidator<DeleteProjectMilestoneHistoryCommand>
{
    readonly ApplicationContext _context;

    public DeleteProjectMilestoneHistoryCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ProjectMilestoneHistoryState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ProjectMilestoneHistory with id {PropertyValue} does not exists");
    }
}
