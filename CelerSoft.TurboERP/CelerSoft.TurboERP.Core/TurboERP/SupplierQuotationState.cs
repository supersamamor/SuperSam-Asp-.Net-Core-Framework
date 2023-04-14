using CelerSoft.Common.Core.Base.Models;
using System.ComponentModel;

namespace CelerSoft.TurboERP.Core.TurboERP;

public record SupplierQuotationState : BaseEntity
{
	public string PurchaseRequisitionId { get; init; } = "";
	public string SupplierId { get; init; } = "";
	public string Canvasser { get; init; } = "";
	public string? Status { get; init; }
	
	public PurchaseRequisitionState? PurchaseRequisition { get; init; }
	public SupplierState? Supplier { get; init; }
	
	public IList<SupplierQuotationItemState>? SupplierQuotationItemList { get; set; }
	public IList<PurchaseState>? PurchaseList { get; set; }
	
}
