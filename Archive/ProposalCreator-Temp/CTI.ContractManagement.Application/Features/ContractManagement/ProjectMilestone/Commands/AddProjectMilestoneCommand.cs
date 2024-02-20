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

public record AddProjectMilestoneCommand : ProjectMilestoneState, IRequest<Validation<Error, ProjectMilestoneState>>;

public class AddProjectMilestoneCommandHandler : BaseCommandHandler<ApplicationContext, ProjectMilestoneState, AddProjectMilestoneCommand>, IRequestHandler<AddProjectMilestoneCommand, Validation<Error, ProjectMilestoneState>>
{
	private readonly IdentityContext _identityContext;
    public AddProjectMilestoneCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddProjectMilestoneCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, ProjectMilestoneState>> Handle(AddProjectMilestoneCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddProjectMilestoneCommandValidator : AbstractValidator<AddProjectMilestoneCommand>
{
    readonly ApplicationContext _context;

    public AddProjectMilestoneCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<ProjectMilestoneState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ProjectMilestone with id {PropertyValue} already exists");
        
    }
}
