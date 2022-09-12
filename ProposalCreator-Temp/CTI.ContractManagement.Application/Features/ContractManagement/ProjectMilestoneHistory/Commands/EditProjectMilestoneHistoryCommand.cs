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
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.ContractManagement.Application.Features.ContractManagement.ProjectMilestoneHistory.Commands;

public record EditProjectMilestoneHistoryCommand : ProjectMilestoneHistoryState, IRequest<Validation<Error, ProjectMilestoneHistoryState>>;

public class EditProjectMilestoneHistoryCommandHandler : BaseCommandHandler<ApplicationContext, ProjectMilestoneHistoryState, EditProjectMilestoneHistoryCommand>, IRequestHandler<EditProjectMilestoneHistoryCommand, Validation<Error, ProjectMilestoneHistoryState>>
{
    public EditProjectMilestoneHistoryCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditProjectMilestoneHistoryCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, ProjectMilestoneHistoryState>> Handle(EditProjectMilestoneHistoryCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditProjectMilestoneHistoryCommandValidator : AbstractValidator<EditProjectMilestoneHistoryCommand>
{
    readonly ApplicationContext _context;

    public EditProjectMilestoneHistoryCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ProjectMilestoneHistoryState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ProjectMilestoneHistory with id {PropertyValue} does not exists");
        
    }
}
