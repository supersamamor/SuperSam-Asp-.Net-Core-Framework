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

public record EditSupplierQuotationItemCommand : SupplierQuotationItemState, IRequest<Validation<Error, SupplierQuotationItemState>>;

public class EditSupplierQuotationItemCommandHandler : BaseCommandHandler<ApplicationContext, SupplierQuotationItemState, EditSupplierQuotationItemCommand>, IRequestHandler<EditSupplierQuotationItemCommand, Validation<Error, SupplierQuotationItemState>>
{
    public EditSupplierQuotationItemCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditSupplierQuotationItemCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, SupplierQuotationItemState>> Handle(EditSupplierQuotationItemCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await EditSupplierQuotationItem(request, cancellationToken));


	public async Task<Validation<Error, SupplierQuotationItemState>> EditSupplierQuotationItem(EditSupplierQuotationItemCommand request, CancellationToken cancellationToken)
	{
		var entity = await Context.SupplierQuotationItem.Where(l => l.Id == request.Id).SingleAsync(cancellationToken: cancellationToken);
		Mapper.Map(request, entity);
		await UpdatePurchaseItemList(entity, request, cancellationToken);
		Context.Update(entity);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, SupplierQuotationItemState>(entity);
	}
	
	private async Task UpdatePurchaseItemList(SupplierQuotationItemState entity, EditSupplierQuotationItemCommand request, CancellationToken cancellationToken)
	{
		IList<PurchaseItemState> purchaseItemListForDeletion = new List<PurchaseItemState>();
		var queryPurchaseItemForDeletion = Context.PurchaseItem.Where(l => l.SupplierQuotationItemId == request.Id).AsNoTracking();
		if (entity.PurchaseItemList?.Count > 0)
		{
			queryPurchaseItemForDeletion = queryPurchaseItemForDeletion.Where(l => !(entity.PurchaseItemList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		purchaseItemListForDeletion = await queryPurchaseItemForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var purchaseItem in purchaseItemListForDeletion!)
		{
			Context.Entry(purchaseItem).State = EntityState.Deleted;
		}
		if (entity.PurchaseItemList?.Count > 0)
		{
			foreach (var purchaseItem in entity.PurchaseItemList.Where(l => !purchaseItemListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<PurchaseItemState>(x => x.Id == purchaseItem.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(purchaseItem).State = EntityState.Added;
				}
				else
				{
					Context.Entry(purchaseItem).State = EntityState.Modified;
				}
			}
		}
	}
	
}

public class EditSupplierQuotationItemCommandValidator : AbstractValidator<EditSupplierQuotationItemCommand>
{
    readonly ApplicationContext _context;

    public EditSupplierQuotationItemCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<SupplierQuotationItemState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("SupplierQuotationItem with id {PropertyValue} does not exists");
        
    }
}
