using AutoMapper;
using CelerSoft.Common.Core.Commands;
using CelerSoft.Common.Data;
using CelerSoft.Common.Utility.Validators;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;

namespace CelerSoft.TurboERP.Application.Features.TurboERP.Unit.Commands;

public record DeleteUnitCommand : BaseCommand, IRequest<Validation<Error, UnitState>>;

public class DeleteUnitCommandHandler : BaseCommandHandler<ApplicationContext, UnitState, DeleteUnitCommand>, IRequestHandler<DeleteUnitCommand, Validation<Error, UnitState>>
{
    public DeleteUnitCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteUnitCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, UnitState>> Handle(DeleteUnitCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteUnitCommandValidator : AbstractValidator<DeleteUnitCommand>
{
    readonly ApplicationContext _context;

    public DeleteUnitCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<UnitState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Unit with id {PropertyValue} does not exists");
    }
}
