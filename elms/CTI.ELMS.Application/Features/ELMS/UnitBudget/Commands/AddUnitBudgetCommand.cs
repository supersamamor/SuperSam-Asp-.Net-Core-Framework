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
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CTI.ELMS.Application.Features.ELMS.UnitBudget.Commands;

public record AddUnitBudgetCommand : UnitBudgetState, IRequest<Validation<Error, UnitBudgetState>>;

public class AddUnitBudgetCommandHandler : BaseCommandHandler<ApplicationContext, UnitBudgetState, AddUnitBudgetCommand>, IRequestHandler<AddUnitBudgetCommand, Validation<Error, UnitBudgetState>>
{
	private readonly IdentityContext _identityContext;
    public AddUnitBudgetCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddUnitBudgetCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, UnitBudgetState>> Handle(AddUnitBudgetCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddUnitBudgetCommandValidator : AbstractValidator<AddUnitBudgetCommand>
{
    readonly ApplicationContext _context;

    public AddUnitBudgetCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<UnitBudgetState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("UnitBudget with id {PropertyValue} already exists");
        
    }
}
