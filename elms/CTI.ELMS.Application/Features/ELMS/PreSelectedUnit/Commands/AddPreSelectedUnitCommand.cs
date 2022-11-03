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

namespace CTI.ELMS.Application.Features.ELMS.PreSelectedUnit.Commands;

public record AddPreSelectedUnitCommand : PreSelectedUnitState, IRequest<Validation<Error, PreSelectedUnitState>>;

public class AddPreSelectedUnitCommandHandler : BaseCommandHandler<ApplicationContext, PreSelectedUnitState, AddPreSelectedUnitCommand>, IRequestHandler<AddPreSelectedUnitCommand, Validation<Error, PreSelectedUnitState>>
{
	private readonly IdentityContext _identityContext;
    public AddPreSelectedUnitCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddPreSelectedUnitCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, PreSelectedUnitState>> Handle(AddPreSelectedUnitCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddPreSelectedUnitCommandValidator : AbstractValidator<AddPreSelectedUnitCommand>
{
    readonly ApplicationContext _context;

    public AddPreSelectedUnitCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<PreSelectedUnitState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("PreSelectedUnit with id {PropertyValue} already exists");
        
    }
}
