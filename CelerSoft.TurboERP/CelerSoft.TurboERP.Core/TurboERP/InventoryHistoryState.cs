using CelerSoft.Common.Core.Base.Models;
using System.ComponentModel;

namespace CelerSoft.TurboERP.Core.TurboERP;

public record InventoryHistoryState : BaseEntity
{
	public string Activity { get; init; } = "";
	public string? InventoryId { get; init; }
	public decimal? Quantity { get; init; }
	
	public InventoryState? Inventory { get; init; }
	
	
}
