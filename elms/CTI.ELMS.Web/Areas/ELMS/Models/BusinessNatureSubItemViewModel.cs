using CTI.Common.Web.Utility.Extensions;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.ELMS.Web.Areas.ELMS.Models;

public record BusinessNatureSubItemViewModel : BaseViewModel
{	
	[Display(Name = "Business Nature Sub Item Name")]
	
	public string? BusinessNatureSubItemName { get; init; }
	[Display(Name = "Business Nature")]
	
	public string? BusinessNatureID { get; init; }
	public string?  ForeignKeyBusinessNature { get; set; }
	[Display(Name = "Is Disabled")]
	public bool IsDisabled { get; init; }
	[Display(Name = "Business Nature Sub Item Code")]
	
	public string? BusinessNatureSubItemCode { get; init; }
	
	public DateTime LastModifiedDate { get; set; }
	public BusinessNatureViewModel? BusinessNature { get; init; }
		
	public IList<BusinessNatureCategoryViewModel>? BusinessNatureCategoryList { get; set; }
	public IList<LeadViewModel>? LeadList { get; set; }
	
}
