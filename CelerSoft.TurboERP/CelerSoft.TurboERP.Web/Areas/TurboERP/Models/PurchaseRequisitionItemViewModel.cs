using CelerSoft.Common.Web.Utility.Extensions;
using CelerSoft.TurboERP.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CelerSoft.TurboERP.Web.Areas.TurboERP.Models;

public record PurchaseRequisitionItemViewModel : BaseViewModel
{	
	[Display(Name = "Purchase Requisition")]
	[Required]
	
	public string PurchaseRequisitionId { get; init; } = "";
	public string?  ForeignKeyPurchaseRequisition { get; set; }
	[Display(Name = "Product")]
	[Required]
	
	public string ProductId { get; init; } = "";
	public string?  ForeignKeyProduct { get; set; }
	[Display(Name = "Quantity")]
	[Required]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal Quantity { get; init; }
	[Display(Name = "Remarks")]
	[StringLength(400, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Remarks { get; init; }
	
	public DateTime LastModifiedDate { get; set; }
	public PurchaseRequisitionViewModel? PurchaseRequisition { get; init; }
	public ProductViewModel? Product { get; init; }
		
	
}
