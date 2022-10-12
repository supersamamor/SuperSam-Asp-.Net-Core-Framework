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

namespace CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.ReportScheduleSetting.Commands;

public record DeleteReportScheduleSettingCommand : BaseCommand, IRequest<Validation<Error, ReportScheduleSettingState>>;

public class DeleteReportScheduleSettingCommandHandler : BaseCommandHandler<ApplicationContext, ReportScheduleSettingState, DeleteReportScheduleSettingCommand>, IRequestHandler<DeleteReportScheduleSettingCommand, Validation<Error, ReportScheduleSettingState>>
{
    public DeleteReportScheduleSettingCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteReportScheduleSettingCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ReportScheduleSettingState>> Handle(DeleteReportScheduleSettingCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteReportScheduleSettingCommandValidator : AbstractValidator<DeleteReportScheduleSettingCommand>
{
    readonly ApplicationContext _context;

    public DeleteReportScheduleSettingCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ReportScheduleSettingState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ReportScheduleSetting with id {PropertyValue} does not exists");
    }
}
