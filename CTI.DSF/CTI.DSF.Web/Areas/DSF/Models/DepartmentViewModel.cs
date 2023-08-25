using CTI.Common.Web.Utility.Extensions;
using CTI.DSF.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.DSF.Web.Areas.DSF.Models;

public record DepartmentViewModel : BaseViewModel
{	
	[Display(Name = "Company Code")]
	[Required]
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string CompanyCode { get; init; } = "";
	public string?  ReferenceFieldCompanyCode { get; set; }
	[Display(Name = "Department Code")]
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? DepartmentCode { get; init; }
	[Display(Name = "Department Name")]
	
	public string? DepartmentName { get; init; }
	
	public DateTime LastModifiedDate { get; set; }
	public CompanyViewModel? Company { get; init; }
		
	public IList<SectionViewModel>? SectionList { get; set; }
	public IList<TaskListViewModel>? TaskListList { get; set; }
	
}
