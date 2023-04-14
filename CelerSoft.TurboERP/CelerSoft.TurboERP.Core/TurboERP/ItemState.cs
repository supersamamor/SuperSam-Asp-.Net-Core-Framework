using CelerSoft.Common.Core.Base.Models;
using System.ComponentModel;

namespace CelerSoft.TurboERP.Core.TurboERP;

public record ItemState : BaseEntity
{
	public string ItemTypeId { get; init; } = "";
	public string? Code { get; init; }
	public string Name { get; init; } = "";
	public string UnitId { get; init; } = "";
	public decimal? AveragePrice { get; init; }
	public decimal? LastPurchasedPrice { get; init; }
	
	public ItemTypeState? ItemType { get; init; }
	public UnitState? Unit { get; init; }
	
	public IList<ProductState>? ProductList { get; set; }
	
}
