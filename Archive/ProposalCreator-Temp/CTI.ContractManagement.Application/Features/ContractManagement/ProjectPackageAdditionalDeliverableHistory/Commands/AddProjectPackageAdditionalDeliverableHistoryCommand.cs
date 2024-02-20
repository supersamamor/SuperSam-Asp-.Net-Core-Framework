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

namespace CTI.ContractManagement.Application.Features.ContractManagement.ProjectPackageAdditionalDeliverableHistory.Commands;

public record AddProjectPackageAdditionalDeliverableHistoryCommand : ProjectPackageAdditionalDeliverableHistoryState, IRequest<Validation<Error, ProjectPackageAdditionalDeliverableHistoryState>>;

public class AddProjectPackageAdditionalDeliverableHistoryCommandHandler : BaseCommandHandler<ApplicationContext, ProjectPackageAdditionalDeliverableHistoryState, AddProjectPackageAdditionalDeliverableHistoryCommand>, IRequestHandler<AddProjectPackageAdditionalDeliverableHistoryCommand, Validation<Error, ProjectPackageAdditionalDeliverableHistoryState>>
{
	private readonly IdentityContext _identityContext;
    public AddProjectPackageAdditionalDeliverableHistoryCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddProjectPackageAdditionalDeliverableHistoryCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, ProjectPackageAdditionalDeliverableHistoryState>> Handle(AddProjectPackageAdditionalDeliverableHistoryCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddProjectPackageAdditionalDeliverableHistoryCommandValidator : AbstractValidator<AddProjectPackageAdditionalDeliverableHistoryCommand>
{
    readonly ApplicationContext _context;

    public AddProjectPackageAdditionalDeliverableHistoryCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<ProjectPackageAdditionalDeliverableHistoryState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ProjectPackageAdditionalDeliverableHistory with id {PropertyValue} already exists");
        
    }
}
