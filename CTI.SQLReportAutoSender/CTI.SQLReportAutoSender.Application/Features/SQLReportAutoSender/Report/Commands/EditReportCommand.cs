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
    public EditReportCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditReportCommand> validator) : base(context, mapper, validator)
    {
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
	
}

public class EditReportCommandValidator : AbstractValidator<EditReportCommand>
{
    readonly ApplicationContext _context;

    public EditReportCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ReportState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Report with id {PropertyValue} does not exists");
        
    }
}
