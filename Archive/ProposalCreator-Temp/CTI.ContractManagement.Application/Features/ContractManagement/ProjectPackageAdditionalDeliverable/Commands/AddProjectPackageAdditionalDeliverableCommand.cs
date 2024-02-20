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

namespace CTI.ContractManagement.Application.Features.ContractManagement.ProjectPackageAdditionalDeliverable.Commands;

public record AddProjectPackageAdditionalDeliverableCommand : ProjectPackageAdditionalDeliverableState, IRequest<Validation<Error, ProjectPackageAdditionalDeliverableState>>;

public class AddProjectPackageAdditionalDeliverableCommandHandler : BaseCommandHandler<ApplicationContext, ProjectPackageAdditionalDeliverableState, AddProjectPackageAdditionalDeliverableCommand>, IRequestHandler<AddProjectPackageAdditionalDeliverableCommand, Validation<Error, ProjectPackageAdditionalDeliverableState>>
{
	private readonly IdentityContext _identityContext;
    public AddProjectPackageAdditionalDeliverableCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddProjectPackageAdditionalDeliverableCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, ProjectPackageAdditionalDeliverableState>> Handle(AddProjectPackageAdditionalDeliverableCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddProjectPackageAdditionalDeliverableCommandValidator : AbstractValidator<AddProjectPackageAdditionalDeliverableCommand>
{
    readonly ApplicationContext _context;

    public AddProjectPackageAdditionalDeliverableCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<ProjectPackageAdditionalDeliverableState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ProjectPackageAdditionalDeliverable with id {PropertyValue} already exists");
        
    }
}
