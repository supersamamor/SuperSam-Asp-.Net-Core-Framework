using CTI.Common.Web.Utility.Extensions;
using CTI.DSF.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.DSF.Web.Areas.DSF.Models;

public record CompanyViewModel : BaseViewModel
{	
	[Display(Name = "Company Code")]
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? CompanyCode { get; init; }
	[Display(Name = "Company Name")]
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? CompanyName { get; init; }
	
	public DateTime LastModifiedDate { get; set; }
		
	public IList<DepartmentViewModel>? DepartmentList { get; set; }
	public IList<TaskListViewModel>? TaskListList { get; set; }
	
}
