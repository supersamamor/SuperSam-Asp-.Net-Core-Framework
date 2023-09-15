using AutoMapper;
using CompanyNamePlaceHolder.Common.Core.Commands;
using CompanyNamePlaceHolder.Common.Data;
using CompanyNamePlaceHolder.Common.Utility.Validators;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.TaskList.Commands;

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
		await UpdateAssignmentList(entity, request, cancellationToken);
		Context.Update(entity);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, TaskListState>(entity);
	}
	
	private async Task UpdateAssignmentList(TaskListState entity, EditTaskListCommand request, CancellationToken cancellationToken)
	{
		IList<AssignmentState> assignmentListForDeletion = new List<AssignmentState>();
		var queryAssignmentForDeletion = Context.Assignment.Where(l => l.TaskListCode == request.Id).AsNoTracking();
		if (entity.AssignmentList?.Count > 0)
		{
			queryAssignmentForDeletion = queryAssignmentForDeletion.Where(l => !(entity.AssignmentList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		assignmentListForDeletion = await queryAssignmentForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var assignment in assignmentListForDeletion!)
		{
			Context.Entry(assignment).State = EntityState.Deleted;
		}
		if (entity.AssignmentList?.Count > 0)
		{
			foreach (var assignment in entity.AssignmentList.Where(l => !assignmentListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<AssignmentState>(x => x.Id == assignment.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(assignment).State = EntityState.Added;
				}
				else
				{
					Context.Entry(assignment).State = EntityState.Modified;
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
        RuleFor(x => x.TaskListCode).MustAsync(async (request, taskListCode, cancellation) => await _context.NotExists<TaskListState>(x => x.TaskListCode == taskListCode && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("TaskList with taskListCode {PropertyValue} already exists");
	
    }
}
