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

public record AddProjectMilestoneHistoryCommand : ProjectMilestoneHistoryState, IRequest<Validation<Error, ProjectMilestoneHistoryState>>;

public class AddProjectMilestoneHistoryCommandHandler : BaseCommandHandler<ApplicationContext, ProjectMilestoneHistoryState, AddProjectMilestoneHistoryCommand>, IRequestHandler<AddProjectMilestoneHistoryCommand, Validation<Error, ProjectMilestoneHistoryState>>
{
	private readonly IdentityContext _identityContext;
    public AddProjectMilestoneHistoryCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddProjectMilestoneHistoryCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, ProjectMilestoneHistoryState>> Handle(AddProjectMilestoneHistoryCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddProjectMilestoneHistoryCommandValidator : AbstractValidator<AddProjectMilestoneHistoryCommand>
{
    readonly ApplicationContext _context;

    public AddProjectMilestoneHistoryCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<ProjectMilestoneHistoryState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ProjectMilestoneHistory with id {PropertyValue} already exists");
        
    }
}
