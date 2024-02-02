using CompanyPL.Common.Web.Utility.Extensions;
using CompanyPL.ProjectPL.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CompanyPL.ProjectPL.Web.Areas.ProjectPL.Models;

public record SampleParentViewModel : BaseViewModel
{	
	[Display(Name = "Name")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Name { get; init; } = "";
	
	public DateTime LastModifiedDate { get; set; }
		
	public IList<EmployeeViewModel>? EmployeeList { get; set; }
	
}
