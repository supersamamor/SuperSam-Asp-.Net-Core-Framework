using CTI.Common.Web.Utility.Extensions;
using CTI.DSF.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.DSF.Web.Areas.DSF.Models;

public record SectionViewModel : BaseViewModel
{	
	[Display(Name = "Department Code")]
	
	public string? DepartmentCode { get; init; }
	public string?  ForeignKeyDepartment { get; set; }
	[Display(Name = "Section Code")]
	[Required]
	
	public string SectionCode { get; init; } = "";
	[Display(Name = "Section Name")]
	[Required]
	
	public string SectionName { get; init; } = "";
	
	public DateTime LastModifiedDate { get; set; }
	public DepartmentViewModel? Department { get; init; }
		
	public IList<TeamViewModel>? TeamList { get; set; }
	
}
