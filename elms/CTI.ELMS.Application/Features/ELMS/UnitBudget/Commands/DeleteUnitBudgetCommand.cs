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

namespace CTI.ELMS.Application.Features.ELMS.UnitBudget.Commands;

public record DeleteUnitBudgetCommand : BaseCommand, IRequest<Validation<Error, UnitBudgetState>>;

public class DeleteUnitBudgetCommandHandler : BaseCommandHandler<ApplicationContext, UnitBudgetState, DeleteUnitBudgetCommand>, IRequestHandler<DeleteUnitBudgetCommand, Validation<Error, UnitBudgetState>>
{
    public DeleteUnitBudgetCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteUnitBudgetCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, UnitBudgetState>> Handle(DeleteUnitBudgetCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteUnitBudgetCommandValidator : AbstractValidator<DeleteUnitBudgetCommand>
{
    readonly ApplicationContext _context;

    public DeleteUnitBudgetCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<UnitBudgetState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("UnitBudget with id {PropertyValue} does not exists");
    }
}
