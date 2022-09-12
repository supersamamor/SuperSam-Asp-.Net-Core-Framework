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

public record EditProjectDeliverableCommand : ProjectDeliverableState, IRequest<Validation<Error, ProjectDeliverableState>>;

public class EditProjectDeliverableCommandHandler : BaseCommandHandler<ApplicationContext, ProjectDeliverableState, EditProjectDeliverableCommand>, IRequestHandler<EditProjectDeliverableCommand, Validation<Error, ProjectDeliverableState>>
{
    public EditProjectDeliverableCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditProjectDeliverableCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, ProjectDeliverableState>> Handle(EditProjectDeliverableCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditProjectDeliverableCommandValidator : AbstractValidator<EditProjectDeliverableCommand>
{
    readonly ApplicationContext _context;

    public EditProjectDeliverableCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ProjectDeliverableState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ProjectDeliverable with id {PropertyValue} does not exists");
        
    }
}
