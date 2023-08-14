using CTI.Common.Web.Utility.Extensions;
using CTI.DSF.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.DSF.Web.Areas.DSF.Models;

public record CompanyViewModel : BaseViewModel
{	
	[Display(Name = "Company Code")]
	[Required]
	
	public string CompanyCode { get; init; } = "";
	[Display(Name = "Company Name")]
	[Required]
	
	public string CompanyName { get; init; } = "";
	
	public DateTime LastModifiedDate { get; set; }
		
	public IList<DepartmentViewModel>? DepartmentList { get; set; }
	
}
