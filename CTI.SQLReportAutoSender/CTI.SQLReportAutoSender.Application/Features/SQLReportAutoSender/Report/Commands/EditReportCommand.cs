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

public record EditReportCommand : ReportState, IRequest<Validation<Error, ReportState>>;

public class EditReportCommandHandler : BaseCommandHandler<ApplicationContext, ReportState, EditReportCommand>, IRequestHandler<EditReportCommand, Validation<Error, ReportState>>
{
    private readonly IdentityContext _identityContext;
    public EditReportCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditReportCommand> validator,
                                     IdentityContext identityContext) : base(context, mapper, validator)
    {
        _identityContext = identityContext;
    }

    public async Task<Validation<Error, ReportState>> Handle(EditReportCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await EditReport(request, cancellationToken));


    public async Task<Validation<Error, ReportState>> EditReport(EditReportCommand request, CancellationToken cancellationToken)
    {
        var entity = await Context.Report.Where(l => l.Id == request.Id).SingleAsync(cancellationToken: cancellationToken);
        Mapper.Map(request, entity);
        await UpdateReportDetailList(entity, request, cancellationToken);
        await UpdateMailSettingList(entity, request, cancellationToken);
        await UpdateMailRecipientList(entity, request, cancellationToken);
        await UpdateReportScheduleSettingList(entity, request, cancellationToken);
        await UpdateCustomScheduleList(entity, request, cancellationToken);
        Context.Update(entity);
        await RefreshApprovers(request.Id, cancellationToken);
        _ = await Context.SaveChangesAsync(cancellationToken);
        return Success<Error, ReportState>(entity);
    }

    private async Task UpdateReportDetailList(ReportState entity, EditReportCommand request, CancellationToken cancellationToken)
    {
        IList<ReportDetailState> reportDetailListForDeletion = new List<ReportDetailState>();
        var queryReportDetailForDeletion = Context.ReportDetail.Where(l => l.ReportId == request.Id).AsNoTracking();
        if (entity.ReportDetailList?.Count > 0)
        {
            queryReportDetailForDeletion = queryReportDetailForDeletion.Where(l => !(entity.ReportDetailList.Select(l => l.Id).ToList().Contains(l.Id)));
        }
        reportDetailListForDeletion = await queryReportDetailForDeletion.ToListAsync(cancellationToken: cancellationToken);
        foreach (var reportDetail in reportDetailListForDeletion!)
        {
            Context.Entry(reportDetail).State = EntityState.Deleted;
        }
        if (entity.ReportDetailList?.Count > 0)
        {
            foreach (var reportDetail in entity.ReportDetailList.Where(l => !reportDetailListForDeletion.Select(l => l.Id).Contains(l.Id)))
            {
                if (await Context.NotExists<ReportDetailState>(x => x.Id == reportDetail.Id, cancellationToken: cancellationToken))
                {
                    Context.Entry(reportDetail).State = EntityState.Added;
                }
                else
                {
                    Context.Entry(reportDetail).State = EntityState.Modified;
                }
            }
        }
    }
    private async Task UpdateMailSettingList(ReportState entity, EditReportCommand request, CancellationToken cancellationToken)
    {
        IList<MailSettingState> mailSettingListForDeletion = new List<MailSettingState>();
        var queryMailSettingForDeletion = Context.MailSetting.Where(l => l.ReportId == request.Id).AsNoTracking();
        if (entity.MailSettingList?.Count > 0)
        {
            queryMailSettingForDeletion = queryMailSettingForDeletion.Where(l => !(entity.MailSettingList.Select(l => l.Id).ToList().Contains(l.Id)));
        }
        mailSettingListForDeletion = await queryMailSettingForDeletion.ToListAsync(cancellationToken: cancellationToken);
        foreach (var mailSetting in mailSettingListForDeletion!)
        {
            Context.Entry(mailSetting).State = EntityState.Deleted;
        }
        if (entity.MailSettingList?.Count > 0)
        {
            foreach (var mailSetting in entity.MailSettingList.Where(l => !mailSettingListForDeletion.Select(l => l.Id).Contains(l.Id)))
            {
                if (await Context.NotExists<MailSettingState>(x => x.Id == mailSetting.Id, cancellationToken: cancellationToken))
                {
                    Context.Entry(mailSetting).State = EntityState.Added;
                }
                else
                {
                    Context.Entry(mailSetting).State = EntityState.Modified;
                }
            }
        }
    }
    private async Task UpdateMailRecipientList(ReportState entity, EditReportCommand request, CancellationToken cancellationToken)
    {
        IList<MailRecipientState> mailRecipientListForDeletion = new List<MailRecipientState>();
        var queryMailRecipientForDeletion = Context.MailRecipient.Where(l => l.ReportId == request.Id).AsNoTracking();
        if (entity.MailRecipientList?.Count > 0)
        {
            queryMailRecipientForDeletion = queryMailRecipientForDeletion.Where(l => !(entity.MailRecipientList.Select(l => l.Id).ToList().Contains(l.Id)));
        }
        mailRecipientListForDeletion = await queryMailRecipientForDeletion.ToListAsync(cancellationToken: cancellationToken);
        foreach (var mailRecipient in mailRecipientListForDeletion!)
        {
            Context.Entry(mailRecipient).State = EntityState.Deleted;
        }
        if (entity.MailRecipientList?.Count > 0)
        {
            foreach (var mailRecipient in entity.MailRecipientList.Where(l => !mailRecipientListForDeletion.Select(l => l.Id).Contains(l.Id)))
            {
                if (await Context.NotExists<MailRecipientState>(x => x.Id == mailRecipient.Id, cancellationToken: cancellationToken))
                {
                    Context.Entry(mailRecipient).State = EntityState.Added;
                }
                else
                {
                    Context.Entry(mailRecipient).State = EntityState.Modified;
                }
            }
        }
    }
    private async Task UpdateReportScheduleSettingList(ReportState entity, EditReportCommand request, CancellationToken cancellationToken)
    {
        IList<ReportScheduleSettingState> reportScheduleSettingListForDeletion = new List<ReportScheduleSettingState>();
        var queryReportScheduleSettingForDeletion = Context.ReportScheduleSetting.Where(l => l.ReportId == request.Id).AsNoTracking();
        if (entity.ReportScheduleSettingList?.Count > 0)
        {
            queryReportScheduleSettingForDeletion = queryReportScheduleSettingForDeletion.Where(l => !(entity.ReportScheduleSettingList.Select(l => l.Id).ToList().Contains(l.Id)));
        }
        reportScheduleSettingListForDeletion = await queryReportScheduleSettingForDeletion.ToListAsync(cancellationToken: cancellationToken);
        foreach (var reportScheduleSetting in reportScheduleSettingListForDeletion!)
        {
            Context.Entry(reportScheduleSetting).State = EntityState.Deleted;
        }
        if (entity.ReportScheduleSettingList?.Count > 0)
        {
            foreach (var reportScheduleSetting in entity.ReportScheduleSettingList.Where(l => !reportScheduleSettingListForDeletion.Select(l => l.Id).Contains(l.Id)))
            {
                if (await Context.NotExists<ReportScheduleSettingState>(x => x.Id == reportScheduleSetting.Id, cancellationToken: cancellationToken))
                {
                    Context.Entry(reportScheduleSetting).State = EntityState.Added;
                }
                else
                {
                    Context.Entry(reportScheduleSetting).State = EntityState.Modified;
                }
            }
        }
    }
    private async Task UpdateCustomScheduleList(ReportState entity, EditReportCommand request, CancellationToken cancellationToken)
    {
        IList<CustomScheduleState> customScheduleListForDeletion = new List<CustomScheduleState>();
        var queryCustomScheduleForDeletion = Context.CustomSchedule.Where(l => l.ReportId == request.Id).AsNoTracking();
        if (entity.CustomScheduleList?.Count > 0)
        {
            queryCustomScheduleForDeletion = queryCustomScheduleForDeletion.Where(l => !(entity.CustomScheduleList.Select(l => l.Id).ToList().Contains(l.Id)));
        }
        customScheduleListForDeletion = await queryCustomScheduleForDeletion.ToListAsync(cancellationToken: cancellationToken);
        foreach (var customSchedule in customScheduleListForDeletion!)
        {
            Context.Entry(customSchedule).State = EntityState.Deleted;
        }
        if (entity.CustomScheduleList?.Count > 0)
        {
            foreach (var customSchedule in entity.CustomScheduleList.Where(l => !customScheduleListForDeletion.Select(l => l.Id).Contains(l.Id)))
            {
                if (await Context.NotExists<CustomScheduleState>(x => x.Id == customSchedule.Id, cancellationToken: cancellationToken))
                {
                    Context.Entry(customSchedule).State = EntityState.Added;
                }
                else
                {
                    Context.Entry(customSchedule).State = EntityState.Modified;
                }
            }
        }
    }
    private async Task RefreshApprovers(string reportId, CancellationToken cancellationToken)
    {
        var approvalRecordListToDelete = await Context.ApprovalRecord.Include(l => l.ApprovalList).Where(l => l.DataId == reportId).AsNoTracking().ToListAsync(cancellationToken);
        foreach (var approvalRecord in approvalRecordListToDelete)
        {
            foreach (var approval in approvalRecord?.ApprovalList!)
            {
                Context.Entry(approval).State = EntityState.Deleted;
            }
            Context.Entry(approvalRecord).State = EntityState.Deleted;
        }
        var approverList = await Context.ApproverAssignment.Include(l => l.ApproverSetup).Where(l => l.ApproverSetup.TableName == ApprovalModule.Report).AsNoTracking().ToListAsync(cancellationToken);
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

public class EditReportCommandValidator : AbstractValidator<EditReportCommand>
{
    readonly ApplicationContext _context;

    public EditReportCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ReportState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Report with id {PropertyValue} does not exists");

        RuleForEach(x => x.CustomScheduleList)
                         .Must((model, submodel) => model.CustomScheduleList!.Count(xsub => xsub.DateTimeSchedule == submodel.DateTimeSchedule) <= 1)
                         .WithMessage((model, submodel) => $"The report schedule date `{submodel.DateTimeSchedule}` has duplicates from report `{model.Description}`.");


        RuleFor(x => x.MailRecipientList).NotEmpty().WithMessage("There must be atleast one recipient");
        RuleFor(x => x.MailRecipientList!.Count).GreaterThan(0).WithMessage("There must be atleast one recipient");
    }
}
