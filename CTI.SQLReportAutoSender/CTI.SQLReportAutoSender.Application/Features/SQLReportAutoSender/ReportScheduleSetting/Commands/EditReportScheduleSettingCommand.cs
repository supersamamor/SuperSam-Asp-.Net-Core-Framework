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

namespace CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.ReportScheduleSetting.Commands;

public record EditReportScheduleSettingCommand : ReportScheduleSettingState, IRequest<Validation<Error, ReportScheduleSettingState>>;

public class EditReportScheduleSettingCommandHandler : BaseCommandHandler<ApplicationContext, ReportScheduleSettingState, EditReportScheduleSettingCommand>, IRequestHandler<EditReportScheduleSettingCommand, Validation<Error, ReportScheduleSettingState>>
{
    public EditReportScheduleSettingCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditReportScheduleSettingCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, ReportScheduleSettingState>> Handle(EditReportScheduleSettingCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditReportScheduleSettingCommandValidator : AbstractValidator<EditReportScheduleSettingCommand>
{
    readonly ApplicationContext _context;

    public EditReportScheduleSettingCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ReportScheduleSettingState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ReportScheduleSetting with id {PropertyValue} does not exists");
        
    }
}
