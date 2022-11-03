using CTI.Common.Web.Utility.Extensions;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.ELMS.Web.Areas.ELMS.Models;

public record BusinessNatureCategoryViewModel : BaseViewModel
{	
	[Display(Name = "Business Nature Category Code")]
	
	public string? BusinessNatureCategoryCode { get; init; }
	[Display(Name = "Business Nature Category Name")]
	
	public string? BusinessNatureCategoryName { get; init; }
	[Display(Name = "Business Nature Sub Item")]
	
	public string? BusinessNatureSubItemID { get; init; }
	public string?  ForeignKeyBusinessNatureSubItem { get; set; }
	[Display(Name = "Is Disabled")]
	public bool IsDisabled { get; init; }
	
	public DateTime LastModifiedDate { get; set; }
	public BusinessNatureSubItemViewModel? BusinessNatureSubItem { get; init; }
		
	public IList<LeadViewModel>? LeadList { get; set; }
	
}
