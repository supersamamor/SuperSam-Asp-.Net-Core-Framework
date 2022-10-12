using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.SQLReportAutoSender.Core.SQLReportAutoSender;
using CTI.SQLReportAutoSender.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.Report.Commands;

public record AddReportCommand : ReportState, IRequest<Validation<Error, ReportState>>;

public class AddReportCommandHandler : BaseCommandHandler<ApplicationContext, ReportState, AddReportCommand>, IRequestHandler<AddReportCommand, Validation<Error, ReportState>>
{
	private readonly IdentityContext _identityContext;
    public AddReportCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddReportCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    public async Task<Validation<Error, ReportState>> Handle(AddReportCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await AddReport(request, cancellationToken));


	public async Task<Validation<Error, ReportState>> AddReport(AddReportCommand request, CancellationToken cancellationToken)
	{
		ReportState entity = Mapper.Map<ReportState>(request);
		UpdateReportDetailList(entity);
		UpdateMailSettingList(entity);
		UpdateMailRecipientList(entity);
		UpdateReportScheduleSettingList(entity);
		UpdateCustomScheduleList(entity);
		_ = await Context.AddAsync(entity, cancellationToken);
		await AddApprovers(entity.Id, cancellationToken);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, ReportState>(entity);
	}
	
	private void UpdateReportDetailList(ReportState entity)
	{
		if (entity.ReportDetailList?.Count > 0)
		{
			foreach (var reportDetail in entity.ReportDetailList!)
			{
				Context.Entry(reportDetail).State = EntityState.Added;
			}
		}
	}
	private void UpdateMailSettingList(ReportState entity)
	{
		if (entity.MailSettingList?.Count > 0)
		{
			foreach (var mailSetting in entity.MailSettingList!)
			{
				Context.Entry(mailSetting).State = EntityState.Added;
			}
		}
	}
	private void UpdateMailRecipientList(ReportState entity)
	{
		if (entity.MailRecipientList?.Count > 0)
		{
			foreach (var mailRecipient in entity.MailRecipientList!)
			{
				Context.Entry(mailRecipient).State = EntityState.Added;
			}
		}
	}
	private void UpdateReportScheduleSettingList(ReportState entity)
	{
		if (entity.ReportScheduleSettingList?.Count > 0)
		{
			foreach (var reportScheduleSetting in entity.ReportScheduleSettingList!)
			{
				Context.Entry(reportScheduleSetting).State = EntityState.Added;
			}
		}
	}
	private void UpdateCustomScheduleList(ReportState entity)
	{
		if (entity.CustomScheduleList?.Count > 0)
		{
			foreach (var customSchedule in entity.CustomScheduleList!)
			{
				Context.Entry(customSchedule).State = EntityState.Added;
			}
		}
	}
	
	private async Task AddApprovers(string reportId, CancellationToken cancellationToken)
	{
		var approverList = await Context.ApproverAssignment.Include(l=>l.ApproverSetup).Where(l => l.ApproverSetup.TableName == ApprovalModule.Report).AsNoTracking().ToListAsync(cancellationToken);
		if (approverList.Count > 0)
		{
			var approvalRecord = new ApprovalRecordState()
			{
				ApproverSetupId = approverList.FirstOrDefault()!.ApproverSetupId,
				DataId = reportId,
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

public class AddReportCommandValidator : AbstractValidator<AddReportCommand>
{
    readonly ApplicationContext _context;

    public AddReportCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<ReportState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Report with id {PropertyValue} already exists");
        
    }
}
