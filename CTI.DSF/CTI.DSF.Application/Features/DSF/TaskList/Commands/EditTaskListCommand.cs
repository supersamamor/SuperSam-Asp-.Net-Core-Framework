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

public record EditTaskListCommand : TaskListState, IRequest<Validation<Error, TaskListState>>;

public class EditTaskListCommandHandler : BaseCommandHandler<ApplicationContext, TaskListState, EditTaskListCommand>, IRequestHandler<EditTaskListCommand, Validation<Error, TaskListState>>
{
    public EditTaskListCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditTaskListCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, TaskListState>> Handle(EditTaskListCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await EditTaskList(request, cancellationToken));


	public async Task<Validation<Error, TaskListState>> EditTaskList(EditTaskListCommand request, CancellationToken cancellationToken)
	{
		var entity = await Context.TaskList.Where(l => l.Id == request.Id).SingleAsync(cancellationToken: cancellationToken);
		Mapper.Map(request, entity);
		await UpdateTaskApproverList(entity, request, cancellationToken);
		await UpdateTaskTagList(entity, request, cancellationToken);
		Context.Update(entity);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, TaskListState>(entity);
	}
	
	private async Task UpdateTaskApproverList(TaskListState entity, EditTaskListCommand request, CancellationToken cancellationToken)
	{
		IList<TaskApproverState> taskApproverListForDeletion = new List<TaskApproverState>();
		var queryTaskApproverForDeletion = Context.TaskApprover.Where(l => l.TaskListId == request.Id).AsNoTracking();
		if (entity.TaskApproverList?.Count > 0)
		{
			queryTaskApproverForDeletion = queryTaskApproverForDeletion.Where(l => !(entity.TaskApproverList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		taskApproverListForDeletion = await queryTaskApproverForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var taskApprover in taskApproverListForDeletion!)
		{
			Context.Entry(taskApprover).State = EntityState.Deleted;
		}
		if (entity.TaskApproverList?.Count > 0)
		{
			foreach (var taskApprover in entity.TaskApproverList.Where(l => !taskApproverListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<TaskApproverState>(x => x.Id == taskApprover.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(taskApprover).State = EntityState.Added;
				}
				else
				{
					Context.Entry(taskApprover).State = EntityState.Modified;
				}
			}
		}
	}
	private async Task UpdateTaskTagList(TaskListState entity, EditTaskListCommand request, CancellationToken cancellationToken)
	{
		IList<TaskTagState> taskTagListForDeletion = new List<TaskTagState>();
		var queryTaskTagForDeletion = Context.TaskTag.Where(l => l.TaskListId == request.Id).AsNoTracking();
		if (entity.TaskTagList?.Count > 0)
		{
			queryTaskTagForDeletion = queryTaskTagForDeletion.Where(l => !(entity.TaskTagList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		taskTagListForDeletion = await queryTaskTagForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var taskTag in taskTagListForDeletion!)
		{
			Context.Entry(taskTag).State = EntityState.Deleted;
		}
		if (entity.TaskTagList?.Count > 0)
		{
			foreach (var taskTag in entity.TaskTagList.Where(l => !taskTagListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<TaskTagState>(x => x.Id == taskTag.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(taskTag).State = EntityState.Added;
				}
				else
				{
					Context.Entry(taskTag).State = EntityState.Modified;
				}
			}
		}
	}
	
}

public class EditTaskListCommandValidator : AbstractValidator<EditTaskListCommand>
{
    readonly ApplicationContext _context;

    public EditTaskListCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<TaskListState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("TaskList with id {PropertyValue} does not exists");
        
    }
}
