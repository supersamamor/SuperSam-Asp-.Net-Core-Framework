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

namespace CTI.DSF.Application.Features.DSF.Department.Commands;

public record EditDepartmentCommand : DepartmentState, IRequest<Validation<Error, DepartmentState>>;

public class EditDepartmentCommandHandler : BaseCommandHandler<ApplicationContext, DepartmentState, EditDepartmentCommand>, IRequestHandler<EditDepartmentCommand, Validation<Error, DepartmentState>>
{
    public EditDepartmentCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditDepartmentCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, DepartmentState>> Handle(EditDepartmentCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await EditDepartment(request, cancellationToken));


	public async Task<Validation<Error, DepartmentState>> EditDepartment(EditDepartmentCommand request, CancellationToken cancellationToken)
	{
		var entity = await Context.Department.Where(l => l.Id == request.Id).SingleAsync(cancellationToken: cancellationToken);
		Mapper.Map(request, entity);
		await UpdateTaskCompanyAssignmentList(entity, request, cancellationToken);
		Context.Update(entity);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, DepartmentState>(entity);
	}
	
	private async Task UpdateTaskCompanyAssignmentList(DepartmentState entity, EditDepartmentCommand request, CancellationToken cancellationToken)
	{
		IList<TaskCompanyAssignmentState> taskCompanyAssignmentListForDeletion = new List<TaskCompanyAssignmentState>();
		var queryTaskCompanyAssignmentForDeletion = Context.TaskCompanyAssignment.Where(l => l.DepartmentId == request.Id).AsNoTracking();
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
	
}

public class EditDepartmentCommandValidator : AbstractValidator<EditDepartmentCommand>
{
    readonly ApplicationContext _context;

    public EditDepartmentCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<DepartmentState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Department with id {PropertyValue} does not exists");
        
    }
}
