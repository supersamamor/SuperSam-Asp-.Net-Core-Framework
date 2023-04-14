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

namespace CelerSoft.TurboERP.Application.Features.TurboERP.Purchase.Commands;

public record AddPurchaseCommand : PurchaseState, IRequest<Validation<Error, PurchaseState>>;

public class AddPurchaseCommandHandler : BaseCommandHandler<ApplicationContext, PurchaseState, AddPurchaseCommand>, IRequestHandler<AddPurchaseCommand, Validation<Error, PurchaseState>>
{
	private readonly IdentityContext _identityContext;
    public AddPurchaseCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddPurchaseCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, PurchaseState>> Handle(AddPurchaseCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddPurchaseCommandValidator : AbstractValidator<AddPurchaseCommand>
{
    readonly ApplicationContext _context;

    public AddPurchaseCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<PurchaseState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Purchase with id {PropertyValue} already exists");
        RuleFor(x => x.Code).MustAsync(async (code, cancellation) => await _context.NotExists<PurchaseState>(x => x.Code == code, cancellationToken: cancellation)).WithMessage("Purchase with code {PropertyValue} already exists");
	
    }
}
