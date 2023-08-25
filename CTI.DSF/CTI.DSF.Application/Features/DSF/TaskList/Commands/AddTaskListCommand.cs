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

namespace CTI.DSF.Application.Features.DSF.TaskList.Commands;

public record AddTaskListCommand : TaskListState, IRequest<Validation<Error, TaskListState>>;

public class AddTaskListCommandHandler : BaseCommandHandler<ApplicationContext, TaskListState, AddTaskListCommand>, IRequestHandler<AddTaskListCommand, Validation<Error, TaskListState>>
{
	private readonly IdentityContext _identityContext;
    public AddTaskListCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddTaskListCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    public async Task<Validation<Error, TaskListState>> Handle(AddTaskListCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await AddTaskList(request, cancellationToken));


	public async Task<Validation<Error, TaskListState>> AddTaskList(AddTaskListCommand request, CancellationToken cancellationToken)
	{
		TaskListState entity = Mapper.Map<TaskListState>(request);
		UpdateTaskApproverList(entity);
		UpdateTaskTagList(entity);
		_ = await Context.AddAsync(entity, cancellationToken);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, TaskListState>(entity);
	}
	
	private void UpdateTaskApproverList(TaskListState entity)
	{
		if (entity.TaskApproverList?.Count > 0)
		{
			foreach (var taskApprover in entity.TaskApproverList!)
			{
				Context.Entry(taskApprover).State = EntityState.Added;
			}
		}
	}
	private void UpdateTaskTagList(TaskListState entity)
	{
		if (entity.TaskTagList?.Count > 0)
		{
			foreach (var taskTag in entity.TaskTagList!)
			{
				Context.Entry(taskTag).State = EntityState.Added;
			}
		}
	}
	
}

public class AddTaskListCommandValidator : AbstractValidator<AddTaskListCommand>
{
    readonly ApplicationContext _context;

    public AddTaskListCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<TaskListState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("TaskList with id {PropertyValue} already exists");
        
    }
}
