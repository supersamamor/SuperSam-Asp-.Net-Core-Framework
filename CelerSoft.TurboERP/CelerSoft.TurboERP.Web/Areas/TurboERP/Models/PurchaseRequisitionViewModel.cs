using CelerSoft.Common.Web.Utility.Extensions;
using CelerSoft.TurboERP.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CelerSoft.TurboERP.Web.Areas.TurboERP.Models;

public record PurchaseRequisitionViewModel : BaseViewModel
{	
	[Display(Name = "Date Required")]
	[Required]
	public DateTime DateRequired { get; init; } = DateTime.Now.Date;
	[Display(Name = "Purpose")]
	[StringLength(500, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Purpose { get; init; }
	[Display(Name = "Remarks")]
	[StringLength(500, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Remarks { get; init; }
	[Display(Name = "Status")]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Status { get; init; }
	
	public DateTime LastModifiedDate { get; set; }
		
	public IList<PurchaseRequisitionItemViewModel>? PurchaseRequisitionItemList { get; set; }
	public IList<SupplierQuotationViewModel>? SupplierQuotationList { get; set; }
	public IList<PurchaseViewModel>? PurchaseList { get; set; }
	
}
