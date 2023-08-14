using CTI.Common.Web.Utility.Extensions;
using CTI.DSF.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.DSF.Web.Areas.DSF.Models;

public record DepartmentViewModel : BaseViewModel
{	
	[Display(Name = "Company Code")]
	
	public string? CompanyCode { get; init; }
	public string?  ForeignKeyCompany { get; set; }
	[Display(Name = "Department Code")]
	[Required]
	
	public string DepartmentCode { get; init; } = "";
	[Display(Name = "Department Name")]
	[Required]
	
	public string DepartmentName { get; init; } = "";
	
	public DateTime LastModifiedDate { get; set; }
	public CompanyViewModel? Company { get; init; }
		
	public IList<SectionViewModel>? SectionList { get; set; }
	
}
