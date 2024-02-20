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

namespace CTI.ContractManagement.Application.Features.ContractManagement.ProjectDeliverableHistory.Commands;

public record AddProjectDeliverableHistoryCommand : ProjectDeliverableHistoryState, IRequest<Validation<Error, ProjectDeliverableHistoryState>>;

public class AddProjectDeliverableHistoryCommandHandler : BaseCommandHandler<ApplicationContext, ProjectDeliverableHistoryState, AddProjectDeliverableHistoryCommand>, IRequestHandler<AddProjectDeliverableHistoryCommand, Validation<Error, ProjectDeliverableHistoryState>>
{
	private readonly IdentityContext _identityContext;
    public AddProjectDeliverableHistoryCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddProjectDeliverableHistoryCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, ProjectDeliverableHistoryState>> Handle(AddProjectDeliverableHistoryCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddProjectDeliverableHistoryCommandValidator : AbstractValidator<AddProjectDeliverableHistoryCommand>
{
    readonly ApplicationContext _context;

    public AddProjectDeliverableHistoryCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<ProjectDeliverableHistoryState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ProjectDeliverableHistory with id {PropertyValue} already exists");
        
    }
}
