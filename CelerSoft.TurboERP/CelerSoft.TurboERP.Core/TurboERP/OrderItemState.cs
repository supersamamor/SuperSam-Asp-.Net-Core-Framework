using CelerSoft.Common.Core.Base.Models;
using System.ComponentModel;

namespace CelerSoft.TurboERP.Core.TurboERP;

public record OrderItemState : BaseEntity
{
	public decimal? Amount { get; init; }
	public string DeliveredByFullName { get; init; } = "";
	public string? InventoryId { get; init; }
	public string OrderByUserId { get; init; } = "";
	public string? OrderId { get; init; }
	public bool Paid { get; init; }
	public decimal? Quantity { get; init; }
	public string ReceivedByFullName { get; init; } = "";
	public string? Status { get; init; }
	public decimal? TotalAmount { get; init; }
	
	public InventoryState? Inventory { get; init; }
	public OrderState? Order { get; init; }
	
	
}
