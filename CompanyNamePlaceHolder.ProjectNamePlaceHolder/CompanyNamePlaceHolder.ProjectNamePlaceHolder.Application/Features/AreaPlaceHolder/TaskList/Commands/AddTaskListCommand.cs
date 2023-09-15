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
		UpdateAssignmentList(entity);
		_ = await Context.AddAsync(entity, cancellationToken);
		await Helpers.ApprovalHelper.AddApprovers(Context, _identityContext, ApprovalModule.TaskList, entity.Id, cancellationToken);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, TaskListState>(entity);
	}
	
	private void UpdateAssignmentList(TaskListState entity)
	{
		if (entity.AssignmentList?.Count > 0)
		{
			foreach (var assignment in entity.AssignmentList!)
			{
				Context.Entry(assignment).State = EntityState.Added;
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
        RuleFor(x => x.TaskListCode).MustAsync(async (taskListCode, cancellation) => await _context.NotExists<TaskListState>(x => x.TaskListCode == taskListCode, cancellationToken: cancellation)).WithMessage("TaskList with taskListCode {PropertyValue} already exists");
	
    }
}
