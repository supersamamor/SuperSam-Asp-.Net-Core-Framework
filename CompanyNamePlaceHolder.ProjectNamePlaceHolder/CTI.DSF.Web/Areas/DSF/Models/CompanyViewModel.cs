using CTI.Common.Web.Utility.Extensions;
using CTI.DSF.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.DSF.Web.Areas.DSF.Models;

public record CompanyViewModel : BaseViewModel
{	
	[Display(Name = "Company Code")]
	[Required]
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string CompanyCode { get; init; } = "";
	[Display(Name = "Company Name")]
	[Required]
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string CompanyName { get; init; } = "";
	[Display(Name = "Active")]
	public bool Active { get; init; }
	
	public DateTime LastModifiedDate { get; set; }
		
	public IList<DepartmentViewModel>? DepartmentList { get; set; }
	public IList<TaskCompanyAssignmentViewModel>? TaskCompanyAssignmentList { get; set; }
	
}
