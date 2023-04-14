using CelerSoft.Common.Core.Base.Models;
using System.ComponentModel;

namespace CelerSoft.TurboERP.Core.TurboERP;

public record PurchaseRequisitionState : BaseEntity
{
	public DateTime DateRequired { get; init; }
	public string? Purpose { get; init; }
	public string? Remarks { get; init; }
	public string? Status { get; init; }
	
	
	public IList<PurchaseRequisitionItemState>? PurchaseRequisitionItemList { get; set; }
	public IList<SupplierQuotationState>? SupplierQuotationList { get; set; }
	public IList<PurchaseState>? PurchaseList { get; set; }
	
}
