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

namespace CelerSoft.TurboERP.Application.Features.TurboERP.PurchaseRequisition.Commands;

public record EditPurchaseRequisitionCommand : PurchaseRequisitionState, IRequest<Validation<Error, PurchaseRequisitionState>>;

public class EditPurchaseRequisitionCommandHandler : BaseCommandHandler<ApplicationContext, PurchaseRequisitionState, EditPurchaseRequisitionCommand>, IRequestHandler<EditPurchaseRequisitionCommand, Validation<Error, PurchaseRequisitionState>>
{
    public EditPurchaseRequisitionCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditPurchaseRequisitionCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, PurchaseRequisitionState>> Handle(EditPurchaseRequisitionCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await EditPurchaseRequisition(request, cancellationToken));


	public async Task<Validation<Error, PurchaseRequisitionState>> EditPurchaseRequisition(EditPurchaseRequisitionCommand request, CancellationToken cancellationToken)
	{
		var entity = await Context.PurchaseRequisition.Where(l => l.Id == request.Id).SingleAsync(cancellationToken: cancellationToken);
		Mapper.Map(request, entity);
		await UpdatePurchaseRequisitionItemList(entity, request, cancellationToken);
		Context.Update(entity);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, PurchaseRequisitionState>(entity);
	}
	
	private async Task UpdatePurchaseRequisitionItemList(PurchaseRequisitionState entity, EditPurchaseRequisitionCommand request, CancellationToken cancellationToken)
	{
		IList<PurchaseRequisitionItemState> purchaseRequisitionItemListForDeletion = new List<PurchaseRequisitionItemState>();
		var queryPurchaseRequisitionItemForDeletion = Context.PurchaseRequisitionItem.Where(l => l.PurchaseRequisitionId == request.Id).AsNoTracking();
		if (entity.PurchaseRequisitionItemList?.Count > 0)
		{
			queryPurchaseRequisitionItemForDeletion = queryPurchaseRequisitionItemForDeletion.Where(l => !(entity.PurchaseRequisitionItemList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		purchaseRequisitionItemListForDeletion = await queryPurchaseRequisitionItemForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var purchaseRequisitionItem in purchaseRequisitionItemListForDeletion!)
		{
			Context.Entry(purchaseRequisitionItem).State = EntityState.Deleted;
		}
		if (entity.PurchaseRequisitionItemList?.Count > 0)
		{
			foreach (var purchaseRequisitionItem in entity.PurchaseRequisitionItemList.Where(l => !purchaseRequisitionItemListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<PurchaseRequisitionItemState>(x => x.Id == purchaseRequisitionItem.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(purchaseRequisitionItem).State = EntityState.Added;
				}
				else
				{
					Context.Entry(purchaseRequisitionItem).State = EntityState.Modified;
				}
			}
		}
	}
	
}

public class EditPurchaseRequisitionCommandValidator : AbstractValidator<EditPurchaseRequisitionCommand>
{
    readonly ApplicationContext _context;

    public EditPurchaseRequisitionCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<PurchaseRequisitionState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("PurchaseRequisition with id {PropertyValue} does not exists");
        
    }
}
