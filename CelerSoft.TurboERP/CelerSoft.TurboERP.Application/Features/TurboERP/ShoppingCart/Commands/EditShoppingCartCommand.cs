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

public record EditShoppingCartCommand : ShoppingCartState, IRequest<Validation<Error, ShoppingCartState>>;

public class EditShoppingCartCommandHandler : BaseCommandHandler<ApplicationContext, ShoppingCartState, EditShoppingCartCommand>, IRequestHandler<EditShoppingCartCommand, Validation<Error, ShoppingCartState>>
{
    public EditShoppingCartCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditShoppingCartCommand> validator) : base(context, mapper, validator)
    {
    }

    
public async Task<Validation<Error, ShoppingCartState>> Handle(EditShoppingCartCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Edit(request, cancellationToken));
	
	
}

public class EditShoppingCartCommandValidator : AbstractValidator<EditShoppingCartCommand>
{
    readonly ApplicationContext _context;

    public EditShoppingCartCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ShoppingCartState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ShoppingCart with id {PropertyValue} does not exists");
        
    }
}
