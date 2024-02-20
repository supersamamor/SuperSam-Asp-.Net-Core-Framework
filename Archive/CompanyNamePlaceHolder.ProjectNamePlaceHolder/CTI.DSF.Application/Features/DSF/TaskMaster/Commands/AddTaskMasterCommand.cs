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

public record AddTaskMasterCommand : TaskMasterState, IRequest<Validation<Error, TaskMasterState>>;

public class AddTaskMasterCommandHandler : BaseCommandHandler<ApplicationContext, TaskMasterState, AddTaskMasterCommand>, IRequestHandler<AddTaskMasterCommand, Validation<Error, TaskMasterState>>
{
	private readonly IdentityContext _identityContext;
    public AddTaskMasterCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddTaskMasterCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    public async Task<Validation<Error, TaskMasterState>> Handle(AddTaskMasterCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await AddTaskMaster(request, cancellationToken));


	public async Task<Validation<Error, TaskMasterState>> AddTaskMaster(AddTaskMasterCommand request, CancellationToken cancellationToken)
	{
		TaskMasterState entity = Mapper.Map<TaskMasterState>(request);
		UpdateTaskCompanyAssignmentList(entity);
		UpdateTaskTagList(entity);
		_ = await Context.AddAsync(entity, cancellationToken);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, TaskMasterState>(entity);
	}
	
	private void UpdateTaskCompanyAssignmentList(TaskMasterState entity)
	{
		if (entity.TaskCompanyAssignmentList?.Count > 0)
		{
			foreach (var taskCompanyAssignment in entity.TaskCompanyAssignmentList!)
			{
				Context.Entry(taskCompanyAssignment).State = EntityState.Added;
			}
		}
	}
	private void UpdateTaskTagList(TaskMasterState entity)
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

public class AddTaskMasterCommandValidator : AbstractValidator<AddTaskMasterCommand>
{
    readonly ApplicationContext _context;

    public AddTaskMasterCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<TaskMasterState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("TaskMaster with id {PropertyValue} already exists");
        
    }
}
