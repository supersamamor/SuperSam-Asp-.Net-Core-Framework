using CelerSoft.Common.Web.Utility.Extensions;
using CelerSoft.TurboERP.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CelerSoft.TurboERP.Web.Areas.TurboERP.Models;

public record InventoryViewModel : BaseViewModel
{	
	[Display(Name = "Purchase Item")]
	[Required]
	
	public string PurchaseItemId { get; init; } = "";
	public string?  ForeignKeyPurchaseItem { get; set; }
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
	[Display(Name = "Delivered By")]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? DeliveredByFullName { get; init; }
	[Display(Name = "Received By")]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? ReceivedByFullName { get; init; }
	[Display(Name = "Delivered Date")]
	public DateTime? DeliveredDate { get; init; } = DateTime.Now.Date;
	[Display(Name = "Received Date")]
	public DateTime? ReceivedDate { get; init; } = DateTime.Now.Date;
	[Display(Name = "Sold By")]
	[StringLength(400, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? SellByUsername { get; init; }
	
	public DateTime LastModifiedDate { get; set; }
	public PurchaseItemViewModel? PurchaseItem { get; init; }
	public ProductViewModel? Product { get; init; }
		
	public IList<InventoryHistoryViewModel>? InventoryHistoryList { get; set; }
	public IList<ShoppingCartViewModel>? ShoppingCartList { get; set; }
	public IList<OrderItemViewModel>? OrderItemList { get; set; }
	
}
