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

namespace CelerSoft.TurboERP.Application.Features.TurboERP.Inventory.Commands;

public record EditInventoryCommand : InventoryState, IRequest<Validation<Error, InventoryState>>;

public class EditInventoryCommandHandler : BaseCommandHandler<ApplicationContext, InventoryState, EditInventoryCommand>, IRequestHandler<EditInventoryCommand, Validation<Error, InventoryState>>
{
    public EditInventoryCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditInventoryCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, InventoryState>> Handle(EditInventoryCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await EditInventory(request, cancellationToken));


	public async Task<Validation<Error, InventoryState>> EditInventory(EditInventoryCommand request, CancellationToken cancellationToken)
	{
		var entity = await Context.Inventory.Where(l => l.Id == request.Id).SingleAsync(cancellationToken: cancellationToken);
		Mapper.Map(request, entity);
		await UpdateInventoryHistoryList(entity, request, cancellationToken);
		await UpdateOrderItemList(entity, request, cancellationToken);
		Context.Update(entity);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, InventoryState>(entity);
	}
	
	private async Task UpdateInventoryHistoryList(InventoryState entity, EditInventoryCommand request, CancellationToken cancellationToken)
	{
		IList<InventoryHistoryState> inventoryHistoryListForDeletion = new List<InventoryHistoryState>();
		var queryInventoryHistoryForDeletion = Context.InventoryHistory.Where(l => l.InventoryId == request.Id).AsNoTracking();
		if (entity.InventoryHistoryList?.Count > 0)
		{
			queryInventoryHistoryForDeletion = queryInventoryHistoryForDeletion.Where(l => !(entity.InventoryHistoryList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		inventoryHistoryListForDeletion = await queryInventoryHistoryForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var inventoryHistory in inventoryHistoryListForDeletion!)
		{
			Context.Entry(inventoryHistory).State = EntityState.Deleted;
		}
		if (entity.InventoryHistoryList?.Count > 0)
		{
			foreach (var inventoryHistory in entity.InventoryHistoryList.Where(l => !inventoryHistoryListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<InventoryHistoryState>(x => x.Id == inventoryHistory.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(inventoryHistory).State = EntityState.Added;
				}
				else
				{
					Context.Entry(inventoryHistory).State = EntityState.Modified;
				}
			}
		}
	}
	private async Task UpdateOrderItemList(InventoryState entity, EditInventoryCommand request, CancellationToken cancellationToken)
	{
		IList<OrderItemState> orderItemListForDeletion = new List<OrderItemState>();
		var queryOrderItemForDeletion = Context.OrderItem.Where(l => l.InventoryId == request.Id).AsNoTracking();
		if (entity.OrderItemList?.Count > 0)
		{
			queryOrderItemForDeletion = queryOrderItemForDeletion.Where(l => !(entity.OrderItemList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		orderItemListForDeletion = await queryOrderItemForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var orderItem in orderItemListForDeletion!)
		{
			Context.Entry(orderItem).State = EntityState.Deleted;
		}
		if (entity.OrderItemList?.Count > 0)
		{
			foreach (var orderItem in entity.OrderItemList.Where(l => !orderItemListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<OrderItemState>(x => x.Id == orderItem.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(orderItem).State = EntityState.Added;
				}
				else
				{
					Context.Entry(orderItem).State = EntityState.Modified;
				}
			}
		}
	}
	
}

public class EditInventoryCommandValidator : AbstractValidator<EditInventoryCommand>
{
    readonly ApplicationContext _context;

    public EditInventoryCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<InventoryState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Inventory with id {PropertyValue} does not exists");
        
    }
}
