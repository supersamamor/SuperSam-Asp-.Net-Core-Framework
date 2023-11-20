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

namespace CTI.DSF.Application.Features.DSF.TaskMaster.Commands;

public record EditTaskMasterCommand : TaskMasterState, IRequest<Validation<Error, TaskMasterState>>;

public class EditTaskMasterCommandHandler : BaseCommandHandler<ApplicationContext, TaskMasterState, EditTaskMasterCommand>, IRequestHandler<EditTaskMasterCommand, Validation<Error, TaskMasterState>>
{
    public EditTaskMasterCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditTaskMasterCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, TaskMasterState>> Handle(EditTaskMasterCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await EditTaskMaster(request, cancellationToken));


	public async Task<Validation<Error, TaskMasterState>> EditTaskMaster(EditTaskMasterCommand request, CancellationToken cancellationToken)
	{
		var entity = await Context.TaskMaster.Where(l => l.Id == request.Id).SingleAsync(cancellationToken: cancellationToken);
		Mapper.Map(request, entity);
		await UpdateTaskCompanyAssignmentList(entity, request, cancellationToken);
		await UpdateTaskTagList(entity, request, cancellationToken);
		Context.Update(entity);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, TaskMasterState>(entity);
	}
	
	private async Task UpdateTaskCompanyAssignmentList(TaskMasterState entity, EditTaskMasterCommand request, CancellationToken cancellationToken)
	{
		IList<TaskCompanyAssignmentState> taskCompanyAssignmentListForDeletion = new List<TaskCompanyAssignmentState>();
		var queryTaskCompanyAssignmentForDeletion = Context.TaskCompanyAssignment.Where(l => l.TaskMasterId == request.Id).AsNoTracking();
		if (entity.TaskCompanyAssignmentList?.Count > 0)
		{
			queryTaskCompanyAssignmentForDeletion = queryTaskCompanyAssignmentForDeletion.Where(l => !(entity.TaskCompanyAssignmentList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		taskCompanyAssignmentListForDeletion = await queryTaskCompanyAssignmentForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var taskCompanyAssignment in taskCompanyAssignmentListForDeletion!)
		{
			Context.Entry(taskCompanyAssignment).State = EntityState.Deleted;
		}
		if (entity.TaskCompanyAssignmentList?.Count > 0)
		{
			foreach (var taskCompanyAssignment in entity.TaskCompanyAssignmentList.Where(l => !taskCompanyAssignmentListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<TaskCompanyAssignmentState>(x => x.Id == taskCompanyAssignment.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(taskCompanyAssignment).State = EntityState.Added;
				}
				else
				{
					Context.Entry(taskCompanyAssignment).State = EntityState.Modified;
				}
			}
		}
	}
	private async Task UpdateTaskTagList(TaskMasterState entity, EditTaskMasterCommand request, CancellationToken cancellationToken)
	{
		IList<TaskTagState> taskTagListForDeletion = new List<TaskTagState>();
		var queryTaskTagForDeletion = Context.TaskTag.Where(l => l.TaskMasterId == request.Id).AsNoTracking();
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

public class EditTaskMasterCommandValidator : AbstractValidator<EditTaskMasterCommand>
{
    readonly ApplicationContext _context;

    public EditTaskMasterCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<TaskMasterState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("TaskMaster with id {PropertyValue} does not exists");
        
    }
}
