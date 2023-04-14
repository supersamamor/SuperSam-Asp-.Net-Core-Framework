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

namespace CelerSoft.TurboERP.Application.Features.TurboERP.SupplierQuotationItem.Commands;

public record AddSupplierQuotationItemCommand : SupplierQuotationItemState, IRequest<Validation<Error, SupplierQuotationItemState>>;

public class AddSupplierQuotationItemCommandHandler : BaseCommandHandler<ApplicationContext, SupplierQuotationItemState, AddSupplierQuotationItemCommand>, IRequestHandler<AddSupplierQuotationItemCommand, Validation<Error, SupplierQuotationItemState>>
{
	private readonly IdentityContext _identityContext;
    public AddSupplierQuotationItemCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddSupplierQuotationItemCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    public async Task<Validation<Error, SupplierQuotationItemState>> Handle(AddSupplierQuotationItemCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await AddSupplierQuotationItem(request, cancellationToken));


	public async Task<Validation<Error, SupplierQuotationItemState>> AddSupplierQuotationItem(AddSupplierQuotationItemCommand request, CancellationToken cancellationToken)
	{
		SupplierQuotationItemState entity = Mapper.Map<SupplierQuotationItemState>(request);
		UpdatePurchaseItemList(entity);
		_ = await Context.AddAsync(entity, cancellationToken);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, SupplierQuotationItemState>(entity);
	}
	
	private void UpdatePurchaseItemList(SupplierQuotationItemState entity)
	{
		if (entity.PurchaseItemList?.Count > 0)
		{
			foreach (var purchaseItem in entity.PurchaseItemList!)
			{
				Context.Entry(purchaseItem).State = EntityState.Added;
			}
		}
	}
	
	
}

public class AddSupplierQuotationItemCommandValidator : AbstractValidator<AddSupplierQuotationItemCommand>
{
    readonly ApplicationContext _context;

    public AddSupplierQuotationItemCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<SupplierQuotationItemState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("SupplierQuotationItem with id {PropertyValue} already exists");
        
    }
}
