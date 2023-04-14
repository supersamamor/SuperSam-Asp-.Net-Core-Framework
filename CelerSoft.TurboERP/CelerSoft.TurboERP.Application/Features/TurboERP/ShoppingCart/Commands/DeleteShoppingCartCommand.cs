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

namespace CelerSoft.TurboERP.Application.Features.TurboERP.ShoppingCart.Commands;

public record DeleteShoppingCartCommand : BaseCommand, IRequest<Validation<Error, ShoppingCartState>>;

public class DeleteShoppingCartCommandHandler : BaseCommandHandler<ApplicationContext, ShoppingCartState, DeleteShoppingCartCommand>, IRequestHandler<DeleteShoppingCartCommand, Validation<Error, ShoppingCartState>>
{
    public DeleteShoppingCartCommandHandler(ApplicationContext context,
                                       IMapper mapper,
                                       CompositeValidator<DeleteShoppingCartCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, ShoppingCartState>> Handle(DeleteShoppingCartCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await Delete(request.Id, cancellationToken));
}


public class DeleteShoppingCartCommandValidator : AbstractValidator<DeleteShoppingCartCommand>
{
    readonly ApplicationContext _context;

    public DeleteShoppingCartCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<ShoppingCartState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("ShoppingCart with id {PropertyValue} does not exists");
    }
}
