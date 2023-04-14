using CelerSoft.Common.Web.Utility.Extensions;
using CelerSoft.TurboERP.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CelerSoft.TurboERP.Web.Areas.TurboERP.Models;

public record ItemViewModel : BaseViewModel
{	
	[Display(Name = "Item Type")]
	[Required]
	
	public string ItemTypeId { get; init; } = "";
	public string?  ForeignKeyItemType { get; set; }
	[Display(Name = "Code")]
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Code { get; init; }
	[Display(Name = "Name")]
	[Required]
	[StringLength(500, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Name { get; init; } = "";
	[Display(Name = "Unit")]
	[Required]
	
	public string UnitId { get; init; } = "";
	public string?  ForeignKeyUnit { get; set; }
	[Display(Name = "Average Price")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? AveragePrice { get; init; }
	[Display(Name = "Last Purchased Price")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? LastPurchasedPrice { get; init; }
	
	public DateTime LastModifiedDate { get; set; }
	public ItemTypeViewModel? ItemType { get; init; }
	public UnitViewModel? Unit { get; init; }
		
	public IList<ProductViewModel>? ProductList { get; set; }
	
}
