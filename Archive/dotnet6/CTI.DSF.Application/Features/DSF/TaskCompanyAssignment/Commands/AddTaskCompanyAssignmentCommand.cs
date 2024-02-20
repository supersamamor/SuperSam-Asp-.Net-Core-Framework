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

namespace CTI.DSF.Application.Features.DSF.TaskCompanyAssignment.Commands;

public record AddTaskCompanyAssignmentCommand : TaskCompanyAssignmentState, IRequest<Validation<Error, TaskCompanyAssignmentState>>;

public class AddTaskCompanyAssignmentCommandHandler : BaseCommandHandler<ApplicationContext, TaskCompanyAssignmentState, AddTaskCompanyAssignmentCommand>, IRequestHandler<AddTaskCompanyAssignmentCommand, Validation<Error, TaskCompanyAssignmentState>>
{
	private readonly IdentityContext _identityContext;
    public AddTaskCompanyAssignmentCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddTaskCompanyAssignmentCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    public async Task<Validation<Error, TaskCompanyAssignmentState>> Handle(AddTaskCompanyAssignmentCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await AddTaskCompanyAssignment(request, cancellationToken));


	public async Task<Validation<Error, TaskCompanyAssignmentState>> AddTaskCompanyAssignment(AddTaskCompanyAssignmentCommand request, CancellationToken cancellationToken)
	{
		TaskCompanyAssignmentState entity = Mapper.Map<TaskCompanyAssignmentState>(request);
		UpdateTaskApproverList(entity);
		_ = await Context.AddAsync(entity, cancellationToken);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, TaskCompanyAssignmentState>(entity);
	}
	
	private void UpdateTaskApproverList(TaskCompanyAssignmentState entity)
	{
		if (entity.TaskApproverList?.Count > 0)
		{
			foreach (var taskApprover in entity.TaskApproverList!)
			{
				Context.Entry(taskApprover).State = EntityState.Added;
			}
		}
	}
	
}

public class AddTaskCompanyAssignmentCommandValidator : AbstractValidator<AddTaskCompanyAssignmentCommand>
{
    readonly ApplicationContext _context;

    public AddTaskCompanyAssignmentCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<TaskCompanyAssignmentState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("TaskCompanyAssignment with id {PropertyValue} already exists");
        
    }
}
