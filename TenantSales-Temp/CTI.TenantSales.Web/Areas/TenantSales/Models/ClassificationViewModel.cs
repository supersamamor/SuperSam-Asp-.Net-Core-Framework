using CTI.Common.Web.Utility.Extensions;
using CTI.TenantSales.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.TenantSales.Web.Areas.TenantSales.Models;

public record ClassificationViewModel : BaseViewModel
{	
	[Display(Name = "Theme")]
	[Required]
	
	public string ThemeId { get; init; } = "";
	public string?  ForeignKeyTheme { get; set; }
	[Display(Name = "Name")]
	[Required]
	[StringLength(80, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Name { get; init; } = "";
	[Display(Name = "Code")]
	[Required]
	[StringLength(15, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Code { get; init; } = "";
	
	public DateTime LastModifiedDate { get; set; }
	public ThemeViewModel? Theme { get; init; }
		
	public IList<CategoryViewModel>? CategoryList { get; set; }
	
}
