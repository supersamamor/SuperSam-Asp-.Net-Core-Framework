using CelerSoft.Common.Web.Utility.Extensions;
using CelerSoft.TurboERP.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CelerSoft.TurboERP.Web.Areas.TurboERP.Models;

public record ShoppingCartViewModel : BaseViewModel
{	
	[Display(Name = "Amount")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? Amount { get; init; }
	[Display(Name = "Inventory")]
	
	public string? InventoryId { get; init; }
	public string?  ForeignKeyInventory { get; set; }
	[Display(Name = "Is Check Out")]
	public bool IsCheckOut { get; init; }
	[Display(Name = "Quantity")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? Quantity { get; init; }
	[Display(Name = "Shopper Username")]
	[Required]
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string ShopperUsername { get; init; } = "";
	[Display(Name = "Total Amount")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? TotalAmount { get; init; }
	
	public DateTime LastModifiedDate { get; set; }
	public InventoryViewModel? Inventory { get; init; }
		
	
}
