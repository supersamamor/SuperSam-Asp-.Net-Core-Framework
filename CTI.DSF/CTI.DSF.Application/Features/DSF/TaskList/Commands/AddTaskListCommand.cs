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
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
        entity.GenerateTaskListCode();
        UpdateAssignmentList(entity);
        UpdateChildTaskList(entity);
        _ = await Context.AddAsync(entity, cancellationToken);
        await AddApprovers(entity.Id, cancellationToken);
        _ = await Context.SaveChangesAsync(cancellationToken);
        return Success<Error, TaskListState>(entity);
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
    private void UpdateChildTaskList(TaskListState entity)
    {
        if (entity.ChildTaskList?.Count > 0)
        {
            foreach (var assignment in entity.ChildTaskList!)
            {
                assignment.SetInformationFromParent(entity);
                Context.Entry(assignment).State = EntityState.Added;
            }
        }
    }
    private async Task AddApprovers(string taskListId, CancellationToken cancellationToken)
    {
        var approverList = await Context.ApproverAssignment.Include(l => l.ApproverSetup).Where(l => l.ApproverSetup.TableName == ApprovalModule.TaskList).AsNoTracking().ToListAsync(cancellationToken);
        if (approverList.Count > 0)
        {
            var approvalRecord = new ApprovalRecordState()
            {
                ApproverSetupId = approverList.FirstOrDefault()!.ApproverSetupId,
                DataId = taskListId,
                ApprovalList = new List<ApprovalState>()
            };
            foreach (var approverItem in approverList)
            {
                if (approverItem.ApproverType == ApproverTypes.User)
                {
                    var approval = new ApprovalState()
                    {
                        Sequence = approverItem.Sequence,
                        ApproverUserId = approverItem.ApproverUserId!,
                    };
                    if (approverList.FirstOrDefault()!.ApproverSetup.ApprovalType != ApprovalTypes.InSequence)
                    {
                        approval.EmailSendingStatus = SendingStatus.Pending;
                    }
                    approvalRecord.ApprovalList.Add(approval);
                }
                else if (approverItem.ApproverType == ApproverTypes.Role)
                {
                    var userListWithRole = await (from a in _identityContext.Users
                                                  join b in _identityContext.UserRoles on a.Id equals b.UserId
                                                  join c in _identityContext.Roles on b.RoleId equals c.Id
                                                  where c.Id == approverItem.ApproverRoleId
                                                  select a.Id).AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
                    foreach (var userId in userListWithRole)
                    {
                        var approval = new ApprovalState()
                        {
                            Sequence = approverItem.Sequence,
                            ApproverUserId = userId,
                        };
                        if (approverList.FirstOrDefault()!.ApproverSetup.ApprovalType != ApprovalTypes.InSequence)
                        {
                            approval.EmailSendingStatus = SendingStatus.Pending;
                        }
                        approvalRecord.ApprovalList.Add(approval);
                    }
                }
            }
            await Context.AddAsync(approvalRecord, cancellationToken);
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
