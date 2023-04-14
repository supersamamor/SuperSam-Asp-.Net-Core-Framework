using CelerSoft.Common.Core.Base.Models;
using System.ComponentModel;

namespace CelerSoft.TurboERP.Core.TurboERP;

public record InventoryState : BaseEntity
{
	public string PurchaseItemId { get; init; } = "";
	public string ProductId { get; init; } = "";
	public decimal Quantity { get; init; }
	public decimal Amount { get; init; }
	public string? DeliveredByFullName { get; init; }
	public string? ReceivedByFullName { get; init; }
	public DateTime? DeliveredDate { get; init; }
	public DateTime? ReceivedDate { get; init; }
	public string? SellByUsername { get; init; }
	
	public PurchaseItemState? PurchaseItem { get; init; }
	public ProductState? Product { get; init; }
	
	public IList<InventoryHistoryState>? InventoryHistoryList { get; set; }
	public IList<ShoppingCartState>? ShoppingCartList { get; set; }
	public IList<OrderItemState>? OrderItemList { get; set; }
	
}
