using CelerSoft.Common.Web.Utility.Extensions;
using CelerSoft.TurboERP.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CelerSoft.TurboERP.Web.Areas.TurboERP.Models;

public record PurchaseItemViewModel : BaseViewModel
{	
	[Display(Name = "Amount")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? Amount { get; init; }
	[Display(Name = "Product")]
	
	public string? ProductId { get; init; }
	public string?  ForeignKeyProduct { get; set; }
	[Display(Name = "Quantity")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? Quantity { get; init; }
	[Display(Name = "Remarks")]
	[StringLength(400, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Remarks { get; init; }
	[Display(Name = "Supplier Quotation Item")]
	
	public string? SupplierQuotationItemId { get; init; }
	public string?  ForeignKeySupplierQuotationItem { get; set; }
	
	public DateTime LastModifiedDate { get; set; }
	public ProductViewModel? Product { get; init; }
	public SupplierQuotationItemViewModel? SupplierQuotationItem { get; init; }
		
	public IList<InventoryViewModel>? InventoryList { get; set; }
	
}
