using CelerSoft.Common.Core.Base.Models;
using System.ComponentModel;

namespace CelerSoft.TurboERP.Core.TurboERP;

public record PurchaseState : BaseEntity
{
	public string? Code { get; init; }
	public string NotedByUsername { get; init; } = "";
	public string? PurchaseRequisitionId { get; init; }
	public string? SupplierQuotationId { get; init; }
	public string ReferenceInvoiceNumber { get; init; } = "";
	
	public PurchaseRequisitionState? PurchaseRequisition { get; init; }
	public SupplierQuotationState? SupplierQuotation { get; init; }
	
	
}
