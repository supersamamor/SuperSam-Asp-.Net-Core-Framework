using CelerSoft.Common.Core.Base.Models;
using System.ComponentModel;

namespace CelerSoft.TurboERP.Core.TurboERP;

public record SupplierQuotationItemState : BaseEntity
{
	public string SupplierQuotationId { get; init; } = "";
	public string ProductId { get; init; } = "";
	public decimal Quantity { get; init; }
	public decimal Amount { get; init; }
	public string? Remarks { get; init; }
	
	public SupplierQuotationState? SupplierQuotation { get; init; }
	public ProductState? Product { get; init; }
	
	public IList<PurchaseItemState>? PurchaseItemList { get; set; }
	
}
