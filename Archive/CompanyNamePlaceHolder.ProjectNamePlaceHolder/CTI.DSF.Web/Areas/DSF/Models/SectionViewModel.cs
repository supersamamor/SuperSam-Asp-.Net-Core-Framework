using CTI.Common.Web.Utility.Extensions;
using CTI.DSF.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.DSF.Web.Areas.DSF.Models;

public record SectionViewModel : BaseViewModel
{	
	[Display(Name = "Department Code")]
	[Required]
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string DepartmentCode { get; init; } = "";
	public string?  ReferenceFieldDepartmentCode { get; set; }
	[Display(Name = "Section Code")]
	[Required]
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string SectionCode { get; init; } = "";
	[Display(Name = "Section Name")]
	[Required]
	
	public string SectionName { get; init; } = "";
	[Display(Name = "Active")]
	public bool Active { get; init; }
	
	public DateTime LastModifiedDate { get; set; }
	public DepartmentViewModel? Department { get; init; }
		
	public IList<TeamViewModel>? TeamList { get; set; }
	public IList<TaskCompanyAssignmentViewModel>? TaskCompanyAssignmentList { get; set; }
	
}
