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

namespace CTI.ELMS.Application.Features.ELMS.UnitGroup.Commands;

public record DeleteUnitGroupCommand : BaseCommand, IRequest<Validation<Error, UnitGroupState>>;

public class DeleteUnitGroupCommandHandler : BaseCommandHandler<ApplicationContext, UnitGroupState, DeleteUnitGroupCommand>, IRequestHandler<DeleteUnitGroupCommand, Validation<Error, UnitGroupState>>
{
    public DeleteUnitGroupCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteUnitGroupCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, UnitGroupState>> Handle(DeleteUnitGroupCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteUnitGroupCommandValidator : AbstractValidator<DeleteUnitGroupCommand>
{
    readonly ApplicationContext _context;

    public DeleteUnitGroupCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<UnitGroupState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("UnitGroup with id {PropertyValue} does not exists");
    }
}
