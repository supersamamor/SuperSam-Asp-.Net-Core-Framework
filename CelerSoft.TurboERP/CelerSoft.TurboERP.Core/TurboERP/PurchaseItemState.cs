using CelerSoft.Common.Core.Base.Models;
using System.ComponentModel;

namespace CelerSoft.TurboERP.Core.TurboERP;

public record PurchaseItemState : BaseEntity
{
	public decimal? Amount { get; init; }
	public string? ProductId { get; init; }
	public decimal? Quantity { get; init; }
	public string? Remarks { get; init; }
	public string? SupplierQuotationItemId { get; init; }
	
	public ProductState? Product { get; init; }
	public SupplierQuotationItemState? SupplierQuotationItem { get; init; }
	
	public IList<InventoryState>? InventoryList { get; set; }
	
}
