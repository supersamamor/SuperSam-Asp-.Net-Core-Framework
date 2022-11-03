using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;

namespace CTI.ELMS.Application.Features.ELMS.UnitOfferedHistory.Commands;

public record DeleteUnitOfferedHistoryCommand : BaseCommand, IRequest<Validation<Error, UnitOfferedHistoryState>>;

public class DeleteUnitOfferedHistoryCommandHandler : BaseCommandHandler<ApplicationContext, UnitOfferedHistoryState, DeleteUnitOfferedHistoryCommand>, IRequestHandler<DeleteUnitOfferedHistoryCommand, Validation<Error, UnitOfferedHistoryState>>
{
    public DeleteUnitOfferedHistoryCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteUnitOfferedHistoryCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, UnitOfferedHistoryState>> Handle(DeleteUnitOfferedHistoryCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteUnitOfferedHistoryCommandValidator : AbstractValidator<DeleteUnitOfferedHistoryCommand>
{
    readonly ApplicationContext _context;

    public DeleteUnitOfferedHistoryCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<UnitOfferedHistoryState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("UnitOfferedHistory with id {PropertyValue} does not exists");
    }
}
