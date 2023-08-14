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
                    UpdateAssignmentList(entity);
                    assignment.GenerateAssignmentCode();
                    Context.Entry(assignment).State = EntityState.Added;
                }
                else
                {
                    Context.Entry(assignment).State = EntityState.Modified;
                }
            }
        }
    }
    private void UpdateAssignmentList(TaskListState entity)
    {
        if (entity.AssignmentList?.Count > 0)
        {
            foreach (var assignment in entity.AssignmentList!)
            {
                UpdateDeliverables(assignment, entity.TaskDescription, entity.TaskFrequency);
                assignment.GenerateAssignmentCode();
                Context.Entry(assignment).State = EntityState.Added;
            }
        }
    }
    private void UpdateDeliverables(AssignmentState assignment, string taskDescription, string frequency)
    {
       
            var dueDateList = Helpers.DeliverableDateHelper.GenerateDatesOfDeliverablesDueDate(assignment.StartDate, assignment.EndDate, frequency);
            foreach (var dueDate in dueDateList)
            {
                var delivery = new DeliveryState()
                {
                    DueDate = dueDate,
                    AssignmentCode = assignment.AssignmentCode ?? "",
                    TaskDescription = taskDescription,
                    Status = "New", //Todo: Replace with constants				
                };
                delivery.GenerateDeliveryCode();
                Context.Entry(delivery).State = EntityState.Added;
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
