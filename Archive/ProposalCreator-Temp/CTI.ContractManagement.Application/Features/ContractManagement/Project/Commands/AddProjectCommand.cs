using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.ContractManagement.Core.ContractManagement;
using CTI.ContractManagement.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.ContractManagement.Application.Features.ContractManagement.Project.Commands;

public record AddProjectCommand : ProjectState, IRequest<Validation<Error, ProjectState>>;

public class AddProjectCommandHandler : BaseCommandHandler<ApplicationContext, ProjectState, AddProjectCommand>, IRequestHandler<AddProjectCommand, Validation<Error, ProjectState>>
{
	private readonly IdentityContext _identityContext;
    public AddProjectCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddProjectCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    public async Task<Validation<Error, ProjectState>> Handle(AddProjectCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await AddProject(request, cancellationToken));


	public async Task<Validation<Error, ProjectState>> AddProject(AddProjectCommand request, CancellationToken cancellationToken)
	{
		ProjectState entity = Mapper.Map<ProjectState>(request);
		UpdateProjectDeliverableList(entity);
		UpdateProjectMilestoneList(entity);
		UpdateProjectPackageList(entity);
		_ = await Context.AddAsync(entity, cancellationToken);
		await AddApprovers(entity.Id, cancellationToken);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, ProjectState>(entity);
	}
	
	private void UpdateProjectDeliverableList(ProjectState entity)
	{
		if (entity.ProjectDeliverableList?.Count > 0)
		{
			foreach (var projectDeliverable in entity.ProjectDeliverableList!)
			{
				Context.Entry(projectDeliverable).State = EntityState.Added;
			}
		}
	}
	private void UpdateProjectMilestoneList(ProjectState entity)
	{
		if (entity.ProjectMilestoneList?.Count > 0)
		{
			foreach (var projectMilestone in entity.ProjectMilestoneList!)
			{
				Context.Entry(projectMilestone).State = EntityState.Added;
			}
		}
	}
	private void UpdateProjectPackageList(ProjectState entity)
	{
		if (entity.ProjectPackageList?.Count > 0)
		{
			foreach (var projectPackage in entity.ProjectPackageList!)
			{
				Context.Entry(projectPackage).State = EntityState.Added;
			}
		}
	}
	
	private async Task AddApprovers(string projectId, CancellationToken cancellationToken)
	{
		var approverList = await Context.ApproverAssignment.Include(l=>l.ApproverSetup).Where(l => l.ApproverSetup.TableName == ApprovalModule.Project).AsNoTracking().ToListAsync(cancellationToken);
		if (approverList.Count > 0)
		{
			var approvalRecord = new ApprovalRecordState()
			{
				ApproverSetupId = approverList.FirstOrDefault()!.ApproverSetupId,
				DataId = projectId,
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

public class AddProjectCommandValidator : AbstractValidator<AddProjectCommand>
{
    readonly ApplicationContext _context;

    public AddProjectCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<ProjectState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Project with id {PropertyValue} already exists");
        RuleFor(x => x.DocumentCode).MustAsync(async (documentCode, cancellation) => await _context.NotExists<ProjectState>(x => x.DocumentCode == documentCode, cancellationToken: cancellation)).WithMessage("Project with documentCode {PropertyValue} already exists");
	
    }
}
