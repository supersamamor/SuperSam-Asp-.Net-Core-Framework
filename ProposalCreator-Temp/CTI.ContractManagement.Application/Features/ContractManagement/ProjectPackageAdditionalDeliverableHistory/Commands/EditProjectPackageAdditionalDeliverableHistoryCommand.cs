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

public record EditProjectPackageAdditionalDeliverableHistoryCommand : ProjectPackageAdditionalDeliverableHistoryState, IRequest<Validation<Error, ProjectPackageAdditionalDeliverableHistoryState>>;

public class EditProjectPackageAdditionalDeliverableHistoryCommandHandler : BaseCommandHandler<ApplicationContext, ProjectPackageAdditionalDeliverableHistoryState, EditProjectPackageAdditionalDeliverableHistoryCommand>, IRequestHandler<EditProjectPackageAdditionalDeliverableHistoryCommand, Validation<Error, ProjectPackageAdditionalDeliverableHistoryState>>
{
    public EditProjectPackageAdditionalDeliverableHistoryCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditProjectPackageAdditionalDeliverableHistoryCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, ProjectPackageAdditionalDeliverableHistoryState>> Handle(EditProjectPackageAdditionalDeliverableHistoryCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditProjectPackageAdditionalDeliverableHistoryCommandValidator : AbstractValidator<EditProjectPackageAdditionalDeliverableHistoryCommand>
{
    readonly ApplicationContext _context;

    public EditProjectPackageAdditionalDeliverableHistoryCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ProjectPackageAdditionalDeliverableHistoryState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ProjectPackageAdditionalDeliverableHistory with id {PropertyValue} does not exists");
        
    }
}
