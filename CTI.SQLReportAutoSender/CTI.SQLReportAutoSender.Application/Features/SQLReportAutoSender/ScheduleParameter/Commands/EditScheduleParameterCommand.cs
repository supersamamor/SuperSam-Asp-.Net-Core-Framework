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

namespace CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.ScheduleParameter.Commands;

public record EditScheduleParameterCommand : ScheduleParameterState, IRequest<Validation<Error, ScheduleParameterState>>;

public class EditScheduleParameterCommandHandler : BaseCommandHandler<ApplicationContext, ScheduleParameterState, EditScheduleParameterCommand>, IRequestHandler<EditScheduleParameterCommand, Validation<Error, ScheduleParameterState>>
{
    public EditScheduleParameterCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditScheduleParameterCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ScheduleParameterState>> Handle(EditScheduleParameterCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await EditScheduleParameter(request, cancellationToken));


	public async Task<Validation<Error, ScheduleParameterState>> EditScheduleParameter(EditScheduleParameterCommand request, CancellationToken cancellationToken)
	{
		var entity = await Context.ScheduleParameter.Where(l => l.Id == request.Id).SingleAsync(cancellationToken: cancellationToken);
		Mapper.Map(request, entity);
		await UpdateScheduleFrequencyParameterSetupList(entity, request, cancellationToken);
		await UpdateReportScheduleSettingList(entity, request, cancellationToken);
		Context.Update(entity);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, ScheduleParameterState>(entity);
	}
	
	private async Task UpdateScheduleFrequencyParameterSetupList(ScheduleParameterState entity, EditScheduleParameterCommand request, CancellationToken cancellationToken)
	{
		IList<ScheduleFrequencyParameterSetupState> scheduleFrequencyParameterSetupListForDeletion = new List<ScheduleFrequencyParameterSetupState>();
		var queryScheduleFrequencyParameterSetupForDeletion = Context.ScheduleFrequencyParameterSetup.Where(l => l.ScheduleParameterId == request.Id).AsNoTracking();
		if (entity.ScheduleFrequencyParameterSetupList?.Count > 0)
		{
			queryScheduleFrequencyParameterSetupForDeletion = queryScheduleFrequencyParameterSetupForDeletion.Where(l => !(entity.ScheduleFrequencyParameterSetupList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		scheduleFrequencyParameterSetupListForDeletion = await queryScheduleFrequencyParameterSetupForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var scheduleFrequencyParameterSetup in scheduleFrequencyParameterSetupListForDeletion!)
		{
			Context.Entry(scheduleFrequencyParameterSetup).State = EntityState.Deleted;
		}
		if (entity.ScheduleFrequencyParameterSetupList?.Count > 0)
		{
			foreach (var scheduleFrequencyParameterSetup in entity.ScheduleFrequencyParameterSetupList.Where(l => !scheduleFrequencyParameterSetupListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<ScheduleFrequencyParameterSetupState>(x => x.Id == scheduleFrequencyParameterSetup.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(scheduleFrequencyParameterSetup).State = EntityState.Added;
				}
				else
				{
					Context.Entry(scheduleFrequencyParameterSetup).State = EntityState.Modified;
				}
			}
		}
	}
	private async Task UpdateReportScheduleSettingList(ScheduleParameterState entity, EditScheduleParameterCommand request, CancellationToken cancellationToken)
	{
		IList<ReportScheduleSettingState> reportScheduleSettingListForDeletion = new List<ReportScheduleSettingState>();
		var queryReportScheduleSettingForDeletion = Context.ReportScheduleSetting.Where(l => l.ScheduleParameterId == request.Id).AsNoTracking();
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
	
}

public class EditScheduleParameterCommandValidator : AbstractValidator<EditScheduleParameterCommand>
{
    readonly ApplicationContext _context;

    public EditScheduleParameterCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ScheduleParameterState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ScheduleParameter with id {PropertyValue} does not exists");
        RuleFor(x => x.Description).MustAsync(async (request, description, cancellation) => await _context.NotExists<ScheduleParameterState>(x => x.Description == description && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("ScheduleParameter with description {PropertyValue} already exists");
	
    }
}
