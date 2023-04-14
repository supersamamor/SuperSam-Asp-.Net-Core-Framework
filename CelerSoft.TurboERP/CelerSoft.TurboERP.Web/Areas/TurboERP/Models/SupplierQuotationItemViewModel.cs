using CelerSoft.Common.Web.Utility.Extensions;
using CelerSoft.TurboERP.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CelerSoft.TurboERP.Web.Areas.TurboERP.Models;

public record SupplierQuotationItemViewModel : BaseViewModel
{	
	[Display(Name = "Supplier Quotation")]
	[Required]
	
	public string SupplierQuotationId { get; init; } = "";
	public string?  ForeignKeySupplierQuotation { get; set; }
	[Display(Name = "Product")]
	[Required]
	
	public string ProductId { get; init; } = "";
	public string?  ForeignKeyProduct { get; set; }
	[Display(Name = "Quantity")]
	[Required]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal Quantity { get; init; }
	[Display(Name = "Amount")]
	[Required]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal Amount { get; init; }
	[Display(Name = "Remarks")]
	[StringLength(400, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Remarks { get; init; }
	
	public DateTime LastModifiedDate { get; set; }
	public SupplierQuotationViewModel? SupplierQuotation { get; init; }
	public ProductViewModel? Product { get; init; }
		
	public IList<PurchaseItemViewModel>? PurchaseItemList { get; set; }
	
}
