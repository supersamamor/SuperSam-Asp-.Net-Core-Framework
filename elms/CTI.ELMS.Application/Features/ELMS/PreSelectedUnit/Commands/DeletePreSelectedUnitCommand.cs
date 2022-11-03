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

namespace CTI.ELMS.Application.Features.ELMS.PreSelectedUnit.Commands;

public record DeletePreSelectedUnitCommand : BaseCommand, IRequest<Validation<Error, PreSelectedUnitState>>;

public class DeletePreSelectedUnitCommandHandler : BaseCommandHandler<ApplicationContext, PreSelectedUnitState, DeletePreSelectedUnitCommand>, IRequestHandler<DeletePreSelectedUnitCommand, Validation<Error, PreSelectedUnitState>>
{
    public DeletePreSelectedUnitCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeletePreSelectedUnitCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, PreSelectedUnitState>> Handle(DeletePreSelectedUnitCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeletePreSelectedUnitCommandValidator : AbstractValidator<DeletePreSelectedUnitCommand>
{
    readonly ApplicationContext _context;

    public DeletePreSelectedUnitCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<PreSelectedUnitState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("PreSelectedUnit with id {PropertyValue} does not exists");
    }
}
