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

public record AddTaskApproverCommand : TaskApproverState, IRequest<Validation<Error, TaskApproverState>>;

public class AddTaskApproverCommandHandler : BaseCommandHandler<ApplicationContext, TaskApproverState, AddTaskApproverCommand>, IRequestHandler<AddTaskApproverCommand, Validation<Error, TaskApproverState>>
{
	private readonly IdentityContext _identityContext;
    public AddTaskApproverCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddTaskApproverCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, TaskApproverState>> Handle(AddTaskApproverCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
}

public class AddTaskApproverCommandValidator : AbstractValidator<AddTaskApproverCommand>
{
    readonly ApplicationContext _context;

    public AddTaskApproverCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<TaskApproverState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("TaskApprover with id {PropertyValue} already exists");
        
    }
}
