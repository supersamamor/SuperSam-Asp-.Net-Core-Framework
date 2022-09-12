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

namespace CTI.ContractManagement.Application.Features.ContractManagement.ProjectDeliverable.Commands;

public record AddProjectDeliverableCommand : ProjectDeliverableState, IRequest<Validation<Error, ProjectDeliverableState>>;

public class AddProjectDeliverableCommandHandler : BaseCommandHandler<ApplicationContext, ProjectDeliverableState, AddProjectDeliverableCommand>, IRequestHandler<AddProjectDeliverableCommand, Validation<Error, ProjectDeliverableState>>
{
	private readonly IdentityContext _identityContext;
    public AddProjectDeliverableCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddProjectDeliverableCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, ProjectDeliverableState>> Handle(AddProjectDeliverableCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddProjectDeliverableCommandValidator : AbstractValidator<AddProjectDeliverableCommand>
{
    readonly ApplicationContext _context;

    public AddProjectDeliverableCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<ProjectDeliverableState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ProjectDeliverable with id {PropertyValue} already exists");
        
    }
}
