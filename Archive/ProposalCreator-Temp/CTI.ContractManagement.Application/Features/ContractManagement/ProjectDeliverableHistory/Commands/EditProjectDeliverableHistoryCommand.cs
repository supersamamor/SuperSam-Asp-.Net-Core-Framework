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

public record EditProjectDeliverableHistoryCommand : ProjectDeliverableHistoryState, IRequest<Validation<Error, ProjectDeliverableHistoryState>>;

public class EditProjectDeliverableHistoryCommandHandler : BaseCommandHandler<ApplicationContext, ProjectDeliverableHistoryState, EditProjectDeliverableHistoryCommand>, IRequestHandler<EditProjectDeliverableHistoryCommand, Validation<Error, ProjectDeliverableHistoryState>>
{
    public EditProjectDeliverableHistoryCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditProjectDeliverableHistoryCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, ProjectDeliverableHistoryState>> Handle(EditProjectDeliverableHistoryCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditProjectDeliverableHistoryCommandValidator : AbstractValidator<EditProjectDeliverableHistoryCommand>
{
    readonly ApplicationContext _context;

    public EditProjectDeliverableHistoryCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ProjectDeliverableHistoryState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ProjectDeliverableHistory with id {PropertyValue} does not exists");
        
    }
}
