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

namespace CTI.ELMS.Application.Features.ELMS.UnitOffered.Commands;

public record DeleteUnitOfferedCommand : BaseCommand, IRequest<Validation<Error, UnitOfferedState>>;

public class DeleteUnitOfferedCommandHandler : BaseCommandHandler<ApplicationContext, UnitOfferedState, DeleteUnitOfferedCommand>, IRequestHandler<DeleteUnitOfferedCommand, Validation<Error, UnitOfferedState>>
{
    public DeleteUnitOfferedCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteUnitOfferedCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, UnitOfferedState>> Handle(DeleteUnitOfferedCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteUnitOfferedCommandValidator : AbstractValidator<DeleteUnitOfferedCommand>
{
    readonly ApplicationContext _context;

    public DeleteUnitOfferedCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<UnitOfferedState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("UnitOffered with id {PropertyValue} does not exists");
    }
}
