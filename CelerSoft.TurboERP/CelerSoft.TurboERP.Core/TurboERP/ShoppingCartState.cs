using CelerSoft.Common.Core.Base.Models;
using System.ComponentModel;

namespace CelerSoft.TurboERP.Core.TurboERP;

public record ShoppingCartState : BaseEntity
{
	public decimal? Amount { get; init; }
	public string? InventoryId { get; init; }
	public bool IsCheckOut { get; init; }
	public decimal? Quantity { get; init; }
	public string ShopperUsername { get; init; } = "";
	public decimal? TotalAmount { get; init; }
	
	public InventoryState? Inventory { get; init; }
	
	
}
