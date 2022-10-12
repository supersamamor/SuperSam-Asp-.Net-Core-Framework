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

namespace CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.ScheduleFrequency.Commands;

public record DeleteScheduleFrequencyCommand : BaseCommand, IRequest<Validation<Error, ScheduleFrequencyState>>;

public class DeleteScheduleFrequencyCommandHandler : BaseCommandHandler<ApplicationContext, ScheduleFrequencyState, DeleteScheduleFrequencyCommand>, IRequestHandler<DeleteScheduleFrequencyCommand, Validation<Error, ScheduleFrequencyState>>
{
    public DeleteScheduleFrequencyCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteScheduleFrequencyCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ScheduleFrequencyState>> Handle(DeleteScheduleFrequencyCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteScheduleFrequencyCommandValidator : AbstractValidator<DeleteScheduleFrequencyCommand>
{
    readonly ApplicationContext _context;

    public DeleteScheduleFrequencyCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ScheduleFrequencyState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ScheduleFrequency with id {PropertyValue} does not exists");
    }
}
