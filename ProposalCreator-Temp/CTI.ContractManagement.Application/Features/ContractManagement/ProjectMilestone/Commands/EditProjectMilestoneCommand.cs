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

namespace CTI.ContractManagement.Application.Features.ContractManagement.ProjectMilestone.Commands;

public record EditProjectMilestoneCommand : ProjectMilestoneState, IRequest<Validation<Error, ProjectMilestoneState>>;

public class EditProjectMilestoneCommandHandler : BaseCommandHandler<ApplicationContext, ProjectMilestoneState, EditProjectMilestoneCommand>, IRequestHandler<EditProjectMilestoneCommand, Validation<Error, ProjectMilestoneState>>
{
    public EditProjectMilestoneCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditProjectMilestoneCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, ProjectMilestoneState>> Handle(EditProjectMilestoneCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditProjectMilestoneCommandValidator : AbstractValidator<EditProjectMilestoneCommand>
{
    readonly ApplicationContext _context;

    public EditProjectMilestoneCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ProjectMilestoneState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ProjectMilestone with id {PropertyValue} does not exists");
        
    }
}
