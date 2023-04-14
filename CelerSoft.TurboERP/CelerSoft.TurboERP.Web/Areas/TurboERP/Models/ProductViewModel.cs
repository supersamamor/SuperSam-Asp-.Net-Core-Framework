using CelerSoft.Common.Web.Utility.Extensions;
using CelerSoft.TurboERP.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CelerSoft.TurboERP.Web.Areas.TurboERP.Models;

public record ProductViewModel : BaseViewModel
{	
	[Display(Name = "Item")]
	[Required]
	
	public string ItemId { get; init; } = "";
	public string?  ForeignKeyItem { get; set; }
	[Display(Name = "Brand")]
	
	public string? BrandId { get; init; }
	public string?  ForeignKeyBrand { get; set; }
	[Display(Name = "Model")]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Model { get; init; }
	[Display(Name = "Description")]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Description { get; init; }
	[Display(Name = "Minimum Quantity")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? MinimumQuantity { get; init; }
	[Display(Name = "Barcode Number")]
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? BarcodeNumber { get; init; } = Guid.NewGuid().ToString();

	public DateTime LastModifiedDate { get; set; }
	public ItemViewModel? Item { get; init; }
	public BrandViewModel? Brand { get; init; }
		
	public IList<ProductImageViewModel>? ProductImageList { get; set; }
	public IList<PurchaseRequisitionItemViewModel>? PurchaseRequisitionItemList { get; set; }
	public IList<SupplierQuotationItemViewModel>? SupplierQuotationItemList { get; set; }
	public IList<PurchaseItemViewModel>? PurchaseItemList { get; set; }
	public IList<InventoryViewModel>? InventoryList { get; set; }
	public string? ProductName { get; set; }
}
