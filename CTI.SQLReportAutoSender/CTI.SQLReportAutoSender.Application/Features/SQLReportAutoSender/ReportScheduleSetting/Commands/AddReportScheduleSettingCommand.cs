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

public record AddReportScheduleSettingCommand : ReportScheduleSettingState, IRequest<Validation<Error, ReportScheduleSettingState>>;

public class AddReportScheduleSettingCommandHandler : BaseCommandHandler<ApplicationContext, ReportScheduleSettingState, AddReportScheduleSettingCommand>, IRequestHandler<AddReportScheduleSettingCommand, Validation<Error, ReportScheduleSettingState>>
{
	private readonly IdentityContext _identityContext;
    public AddReportScheduleSettingCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddReportScheduleSettingCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, ReportScheduleSettingState>> Handle(AddReportScheduleSettingCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddReportScheduleSettingCommandValidator : AbstractValidator<AddReportScheduleSettingCommand>
{
    readonly ApplicationContext _context;

    public AddReportScheduleSettingCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<ReportScheduleSettingState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ReportScheduleSetting with id {PropertyValue} already exists");
        
    }
}
