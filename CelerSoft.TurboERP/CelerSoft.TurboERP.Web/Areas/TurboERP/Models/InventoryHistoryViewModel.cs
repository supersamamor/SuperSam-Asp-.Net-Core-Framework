using CelerSoft.Common.Web.Utility.Extensions;
using CelerSoft.TurboERP.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CelerSoft.TurboERP.Web.Areas.TurboERP.Models;

public record InventoryHistoryViewModel : BaseViewModel
{	
	[Display(Name = "Activity")]
	[Required]
	[StringLength(400, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Activity { get; init; } = "";
	[Display(Name = "Inventory")]
	
	public string? InventoryId { get; init; }
	public string?  ForeignKeyInventory { get; set; }
	[Display(Name = "Quantity")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? Quantity { get; init; }
	
	public DateTime LastModifiedDate { get; set; }
	public InventoryViewModel? Inventory { get; init; }
		
	
}
