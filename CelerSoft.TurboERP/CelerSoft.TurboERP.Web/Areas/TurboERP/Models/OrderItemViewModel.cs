using CelerSoft.Common.Web.Utility.Extensions;
using CelerSoft.TurboERP.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CelerSoft.TurboERP.Web.Areas.TurboERP.Models;

public record OrderItemViewModel : BaseViewModel
{	
	[Display(Name = "Amount")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? Amount { get; init; }
	[Display(Name = "Delivered By")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string DeliveredByFullName { get; init; } = "";
	[Display(Name = "Inventory")]
	
	public string? InventoryId { get; init; }
	public string?  ForeignKeyInventory { get; set; }
	[Display(Name = "Order By")]
	[Required]
	[StringLength(400, ErrorMessage = "{0} length can't be more than {1}.")]
	public string OrderByUserId { get; init; } = "";
	[Display(Name = "Order")]
	
	public string? OrderId { get; init; }
	public string?  ForeignKeyOrder { get; set; }
	[Display(Name = "Paid")]
	public bool Paid { get; init; }
	[Display(Name = "Quantity")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? Quantity { get; init; }
	[Display(Name = "Received By")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string ReceivedByFullName { get; init; } = "";
	[Display(Name = "Status")]
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Status { get; init; }
	[Display(Name = "Total Amount")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? TotalAmount { get; init; }
	
	public DateTime LastModifiedDate { get; set; }
	public InventoryViewModel? Inventory { get; init; }
	public OrderViewModel? Order { get; init; }
		
	
}
