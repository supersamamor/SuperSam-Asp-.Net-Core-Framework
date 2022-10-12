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

namespace CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.ScheduleParameter.Commands;

public record DeleteScheduleParameterCommand : BaseCommand, IRequest<Validation<Error, ScheduleParameterState>>;

public class DeleteScheduleParameterCommandHandler : BaseCommandHandler<ApplicationContext, ScheduleParameterState, DeleteScheduleParameterCommand>, IRequestHandler<DeleteScheduleParameterCommand, Validation<Error, ScheduleParameterState>>
{
    public DeleteScheduleParameterCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteScheduleParameterCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ScheduleParameterState>> Handle(DeleteScheduleParameterCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteScheduleParameterCommandValidator : AbstractValidator<DeleteScheduleParameterCommand>
{
    readonly ApplicationContext _context;

    public DeleteScheduleParameterCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ScheduleParameterState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ScheduleParameter with id {PropertyValue} does not exists");
    }
}
