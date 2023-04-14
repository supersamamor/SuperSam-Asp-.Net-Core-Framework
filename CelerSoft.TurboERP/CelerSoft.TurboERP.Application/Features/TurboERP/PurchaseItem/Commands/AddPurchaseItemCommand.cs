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

namespace CelerSoft.TurboERP.Application.Features.TurboERP.PurchaseItem.Commands;

public record AddPurchaseItemCommand : PurchaseItemState, IRequest<Validation<Error, PurchaseItemState>>;

public class AddPurchaseItemCommandHandler : BaseCommandHandler<ApplicationContext, PurchaseItemState, AddPurchaseItemCommand>, IRequestHandler<AddPurchaseItemCommand, Validation<Error, PurchaseItemState>>
{
	private readonly IdentityContext _identityContext;
    public AddPurchaseItemCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddPurchaseItemCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    
public async Task<Validation<Error, PurchaseItemState>> Handle(AddPurchaseItemCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await Add(request, cancellationToken));
	
	
	
}

public class AddPurchaseItemCommandValidator : AbstractValidator<AddPurchaseItemCommand>
{
    readonly ApplicationContext _context;

    public AddPurchaseItemCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<PurchaseItemState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("PurchaseItem with id {PropertyValue} already exists");
        
    }
}
