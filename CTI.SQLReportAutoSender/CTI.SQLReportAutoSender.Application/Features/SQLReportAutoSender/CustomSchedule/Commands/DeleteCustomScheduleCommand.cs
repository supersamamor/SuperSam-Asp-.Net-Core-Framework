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

namespace CTI.SQLReportAutoSender.Application.Features.SQLReportAutoSender.CustomSchedule.Commands;

public record DeleteCustomScheduleCommand : BaseCommand, IRequest<Validation<Error, CustomScheduleState>>;

public class DeleteCustomScheduleCommandHandler : BaseCommandHandler<ApplicationContext, CustomScheduleState, DeleteCustomScheduleCommand>, IRequestHandler<DeleteCustomScheduleCommand, Validation<Error, CustomScheduleState>>
{
    public DeleteCustomScheduleCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteCustomScheduleCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, CustomScheduleState>> Handle(DeleteCustomScheduleCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteCustomScheduleCommandValidator : AbstractValidator<DeleteCustomScheduleCommand>
{
    readonly ApplicationContext _context;

    public DeleteCustomScheduleCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<CustomScheduleState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("CustomSchedule with id {PropertyValue} does not exists");
    }
}
