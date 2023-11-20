using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.DSF.Core.DSF;
using CTI.DSF.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.DSF.Application.Features.DSF.TaskApprover.Commands;

public record EditTaskApproverCommand : TaskApproverState, IRequest<Validation<Error, TaskApproverState>>;

public class EditTaskApproverCommandHandler : BaseCommandHandler<ApplicationContext, TaskApproverState, EditTaskApproverCommand>, IRequestHandler<EditTaskApproverCommand, Validation<Error, TaskApproverState>>
{
    public EditTaskApproverCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditTaskApproverCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, TaskApproverState>> Handle(EditTaskApproverCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditTaskApproverCommandValidator : AbstractValidator<EditTaskApproverCommand>
{
    readonly ApplicationContext _context;

    public EditTaskApproverCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<TaskApproverState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("TaskApprover with id {PropertyValue} does not exists");
        
    }
}
