using CelerSoft.Common.Web.Utility.Extensions;
using CelerSoft.TurboERP.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CelerSoft.TurboERP.Web.Areas.TurboERP.Models;

public record SupplierQuotationViewModel : BaseViewModel
{	
	[Display(Name = "Purchase Requisition")]
	[Required]
	
	public string PurchaseRequisitionId { get; init; } = "";
	public string?  ForeignKeyPurchaseRequisition { get; set; }
	[Display(Name = "Supplier")]
	[Required]
	
	public string SupplierId { get; init; } = "";
	public string?  ForeignKeySupplier { get; set; }
	[Display(Name = "Canvasser")]
	[Required]
	[StringLength(500, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Canvasser { get; init; } = "";
	[Display(Name = "Status")]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Status { get; init; }
	
	public DateTime LastModifiedDate { get; set; }
	public PurchaseRequisitionViewModel? PurchaseRequisition { get; init; }
	public SupplierViewModel? Supplier { get; init; }
		
	public IList<SupplierQuotationItemViewModel>? SupplierQuotationItemList { get; set; }
	public IList<PurchaseViewModel>? PurchaseList { get; set; }
	
}
