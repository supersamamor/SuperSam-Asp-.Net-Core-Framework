using CTI.Common.Web.Utility.Extensions;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.ELMS.Web.Areas.ELMS.Models;

public record LeadSourceViewModel : BaseViewModel
{	
	[Display(Name = "Lead Source")]
	[Required]
	
	public string LeadSourceName { get; init; } = "";
	
	public DateTime LastModifiedDate { get; set; }
		
	public IList<LeadViewModel>? LeadList { get; set; }
	
}
