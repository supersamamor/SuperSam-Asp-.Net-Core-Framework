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
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CelerSoft.TurboERP.Application.Features.TurboERP.ShoppingCart.Commands;

public record AddShoppingCartCommand : ShoppingCartState, IRequest<Validation<Error, ShoppingCartState>>;

public class AddShoppingCartCommandHandler : BaseCommandHandler<ApplicationContext, ShoppingCartState, AddShoppingCartCommand>, IRequestHandler<AddShoppingCartCommand, Validation<Error, ShoppingCartState>>
{
	private readonly IdentityContext _identityContext;
    public AddShoppingCartCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddShoppingCartCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, ShoppingCartState>> Handle(AddShoppingCartCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddShoppingCartCommandValidator : AbstractValidator<AddShoppingCartCommand>
{
    readonly ApplicationContext _context;

    public AddShoppingCartCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<ShoppingCartState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ShoppingCart with id {PropertyValue} already exists");
        
    }
}
