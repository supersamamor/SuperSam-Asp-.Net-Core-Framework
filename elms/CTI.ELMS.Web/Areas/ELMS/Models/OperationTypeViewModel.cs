using CTI.Common.Web.Utility.Extensions;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.ELMS.Web.Areas.ELMS.Models;

public record OperationTypeViewModel : BaseViewModel
{	
	[Display(Name = "Operation Type")]
	[Required]
	
	public string OperationTypeName { get; init; } = "";
	
	public DateTime LastModifiedDate { get; set; }
		
	public IList<LeadViewModel>? LeadList { get; set; }
	
}
