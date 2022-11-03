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

namespace CTI.ELMS.Application.Features.ELMS.UnitGroup.Commands;

public record AddUnitGroupCommand : UnitGroupState, IRequest<Validation<Error, UnitGroupState>>;

public class AddUnitGroupCommandHandler : BaseCommandHandler<ApplicationContext, UnitGroupState, AddUnitGroupCommand>, IRequestHandler<AddUnitGroupCommand, Validation<Error, UnitGroupState>>
{
	private readonly IdentityContext _identityContext;
    public AddUnitGroupCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddUnitGroupCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, UnitGroupState>> Handle(AddUnitGroupCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddUnitGroupCommandValidator : AbstractValidator<AddUnitGroupCommand>
{
    readonly ApplicationContext _context;

    public AddUnitGroupCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<UnitGroupState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("UnitGroup with id {PropertyValue} already exists");
        
    }
}
