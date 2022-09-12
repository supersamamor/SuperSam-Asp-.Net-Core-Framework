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

public record EditProjectPackageAdditionalDeliverableCommand : ProjectPackageAdditionalDeliverableState, IRequest<Validation<Error, ProjectPackageAdditionalDeliverableState>>;

public class EditProjectPackageAdditionalDeliverableCommandHandler : BaseCommandHandler<ApplicationContext, ProjectPackageAdditionalDeliverableState, EditProjectPackageAdditionalDeliverableCommand>, IRequestHandler<EditProjectPackageAdditionalDeliverableCommand, Validation<Error, ProjectPackageAdditionalDeliverableState>>
{
    public EditProjectPackageAdditionalDeliverableCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditProjectPackageAdditionalDeliverableCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, ProjectPackageAdditionalDeliverableState>> Handle(EditProjectPackageAdditionalDeliverableCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditProjectPackageAdditionalDeliverableCommandValidator : AbstractValidator<EditProjectPackageAdditionalDeliverableCommand>
{
    readonly ApplicationContext _context;

    public EditProjectPackageAdditionalDeliverableCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ProjectPackageAdditionalDeliverableState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ProjectPackageAdditionalDeliverable with id {PropertyValue} does not exists");
        
    }
}
