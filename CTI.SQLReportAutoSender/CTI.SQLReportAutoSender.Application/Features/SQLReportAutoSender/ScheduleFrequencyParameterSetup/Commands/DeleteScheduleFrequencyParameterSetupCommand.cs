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

namespace CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.ScheduleFrequencyParameterSetup.Commands;

public record DeleteScheduleFrequencyParameterSetupCommand : BaseCommand, IRequest<Validation<Error, ScheduleFrequencyParameterSetupState>>;

public class DeleteScheduleFrequencyParameterSetupCommandHandler : BaseCommandHandler<ApplicationContext, ScheduleFrequencyParameterSetupState, DeleteScheduleFrequencyParameterSetupCommand>, IRequestHandler<DeleteScheduleFrequencyParameterSetupCommand, Validation<Error, ScheduleFrequencyParameterSetupState>>
{
    public DeleteScheduleFrequencyParameterSetupCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteScheduleFrequencyParameterSetupCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ScheduleFrequencyParameterSetupState>> Handle(DeleteScheduleFrequencyParameterSetupCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteScheduleFrequencyParameterSetupCommandValidator : AbstractValidator<DeleteScheduleFrequencyParameterSetupCommand>
{
    readonly ApplicationContext _context;

    public DeleteScheduleFrequencyParameterSetupCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ScheduleFrequencyParameterSetupState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ScheduleFrequencyParameterSetup with id {PropertyValue} does not exists");
    }
}
