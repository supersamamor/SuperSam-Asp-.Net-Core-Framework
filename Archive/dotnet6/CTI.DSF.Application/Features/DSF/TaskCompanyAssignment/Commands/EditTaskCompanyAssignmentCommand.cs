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

public record EditTaskCompanyAssignmentCommand : TaskCompanyAssignmentState, IRequest<Validation<Error, TaskCompanyAssignmentState>>;

public class EditTaskCompanyAssignmentCommandHandler : BaseCommandHandler<ApplicationContext, TaskCompanyAssignmentState, EditTaskCompanyAssignmentCommand>, IRequestHandler<EditTaskCompanyAssignmentCommand, Validation<Error, TaskCompanyAssignmentState>>
{
    public EditTaskCompanyAssignmentCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditTaskCompanyAssignmentCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, TaskCompanyAssignmentState>> Handle(EditTaskCompanyAssignmentCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await EditTaskCompanyAssignment(request, cancellationToken));


	public async Task<Validation<Error, TaskCompanyAssignmentState>> EditTaskCompanyAssignment(EditTaskCompanyAssignmentCommand request, CancellationToken cancellationToken)
	{
		var entity = await Context.TaskCompanyAssignment.Where(l => l.Id == request.Id).SingleAsync(cancellationToken: cancellationToken);
		Mapper.Map(request, entity);
		await UpdateTaskApproverList(entity, request, cancellationToken);
		Context.Update(entity);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, TaskCompanyAssignmentState>(entity);
	}
	
	private async Task UpdateTaskApproverList(TaskCompanyAssignmentState entity, EditTaskCompanyAssignmentCommand request, CancellationToken cancellationToken)
	{
		IList<TaskApproverState> taskApproverListForDeletion = new List<TaskApproverState>();
		var queryTaskApproverForDeletion = Context.TaskApprover.Where(l => l.TaskCompanyAssignmentId == request.Id).AsNoTracking();
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
	
}

public class EditTaskCompanyAssignmentCommandValidator : AbstractValidator<EditTaskCompanyAssignmentCommand>
{
    readonly ApplicationContext _context;

    public EditTaskCompanyAssignmentCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<TaskCompanyAssignmentState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("TaskCompanyAssignment with id {PropertyValue} does not exists");
        
    }
}
