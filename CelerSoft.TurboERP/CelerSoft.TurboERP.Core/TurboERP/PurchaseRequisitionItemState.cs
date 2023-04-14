using CelerSoft.Common.Core.Base.Models;
using System.ComponentModel;

namespace CelerSoft.TurboERP.Core.TurboERP;

public record PurchaseRequisitionItemState : BaseEntity
{
	public string PurchaseRequisitionId { get; init; } = "";
	public string ProductId { get; init; } = "";
	public decimal Quantity { get; init; }
	public string? Remarks { get; init; }
	
	public PurchaseRequisitionState? PurchaseRequisition { get; init; }
	public ProductState? Product { get; init; }
	
	
}
