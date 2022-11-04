using CTI.Common.Web.Utility.Extensions;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.ELMS.Web.Areas.ELMS.Models;

public record BusinessNatureViewModel : BaseViewModel
{	
	[Display(Name = "Business Nature Name")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string BusinessNatureName { get; init; } = "";
	[Display(Name = "Business Nature Code")]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? BusinessNatureCode { get; init; }
	[Display(Name = "Is Disabled")]
	public bool IsDisabled { get; init; }
	
	public DateTime LastModifiedDate { get; set; }
		
	public IList<BusinessNatureSubItemViewModel>? BusinessNatureSubItemList { get; set; }
	public IList<LeadViewModel>? LeadList { get; set; }
	
}
