using CelerSoft.Common.Web.Utility.Extensions;
using CelerSoft.TurboERP.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CelerSoft.TurboERP.Web.Areas.TurboERP.Models;

public record PurchaseViewModel : BaseViewModel
{	
	[Display(Name = "Code")]
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Code { get; init; }
	[Display(Name = "Noted By")]
	[Required]
	[StringLength(400, ErrorMessage = "{0} length can't be more than {1}.")]
	public string NotedByUsername { get; init; } = "";
	[Display(Name = "Purchase Requisition")]
	
	public string? PurchaseRequisitionId { get; init; }
	public string?  ForeignKeyPurchaseRequisition { get; set; }
	[Display(Name = "Supplier Quotation")]
	
	public string? SupplierQuotationId { get; init; }
	public string?  ForeignKeySupplierQuotation { get; set; }
	[Display(Name = "Reference Invoice Number")]
	[Required]
	[StringLength(400, ErrorMessage = "{0} length can't be more than {1}.")]
	public string ReferenceInvoiceNumber { get; init; } = "";
	
	public DateTime LastModifiedDate { get; set; }
	public PurchaseRequisitionViewModel? PurchaseRequisition { get; init; }
	public SupplierQuotationViewModel? SupplierQuotation { get; init; }
		
	
}
