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

public record AddScheduleParameterCommand : ScheduleParameterState, IRequest<Validation<Error, ScheduleParameterState>>;

public class AddScheduleParameterCommandHandler : BaseCommandHandler<ApplicationContext, ScheduleParameterState, AddScheduleParameterCommand>, IRequestHandler<AddScheduleParameterCommand, Validation<Error, ScheduleParameterState>>
{
	private readonly IdentityContext _identityContext;
    public AddScheduleParameterCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddScheduleParameterCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    public async Task<Validation<Error, ScheduleParameterState>> Handle(AddScheduleParameterCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await AddScheduleParameter(request, cancellationToken));


	public async Task<Validation<Error, ScheduleParameterState>> AddScheduleParameter(AddScheduleParameterCommand request, CancellationToken cancellationToken)
	{
		ScheduleParameterState entity = Mapper.Map<ScheduleParameterState>(request);
		UpdateScheduleFrequencyParameterSetupList(entity);
		UpdateReportScheduleSettingList(entity);
		_ = await Context.AddAsync(entity, cancellationToken);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, ScheduleParameterState>(entity);
	}
	
	private void UpdateScheduleFrequencyParameterSetupList(ScheduleParameterState entity)
	{
		if (entity.ScheduleFrequencyParameterSetupList?.Count > 0)
		{
			foreach (var scheduleFrequencyParameterSetup in entity.ScheduleFrequencyParameterSetupList!)
			{
				Context.Entry(scheduleFrequencyParameterSetup).State = EntityState.Added;
			}
		}
	}
	private void UpdateReportScheduleSettingList(ScheduleParameterState entity)
	{
		if (entity.ReportScheduleSettingList?.Count > 0)
		{
			foreach (var reportScheduleSetting in entity.ReportScheduleSettingList!)
			{
				Context.Entry(reportScheduleSetting).State = EntityState.Added;
			}
		}
	}
	
	
}

public class AddScheduleParameterCommandValidator : AbstractValidator<AddScheduleParameterCommand>
{
    readonly ApplicationContext _context;

    public AddScheduleParameterCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<ScheduleParameterState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ScheduleParameter with id {PropertyValue} already exists");
        RuleFor(x => x.Description).MustAsync(async (description, cancellation) => await _context.NotExists<ScheduleParameterState>(x => x.Description == description, cancellationToken: cancellation)).WithMessage("ScheduleParameter with description {PropertyValue} already exists");
	
    }
}
