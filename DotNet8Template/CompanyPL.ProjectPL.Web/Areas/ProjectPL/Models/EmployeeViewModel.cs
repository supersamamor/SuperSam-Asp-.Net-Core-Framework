using CompanyPL.Common.Web.Utility.Extensions;
using CompanyPL.ProjectPL.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CompanyPL.ProjectPL.Web.Areas.ProjectPL.Models;

public record EmployeeViewModel : BaseViewModel
{	
	[Display(Name = "Employee Code")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string EmployeeCode { get; init; } = "";
	[Display(Name = "First Name")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string FirstName { get; init; } = "";
	[Display(Name = "Middle Name")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string MiddleName { get; init; } = "";
	[Display(Name = "Last Name")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string LastName { get; init; } = "";
	
	public DateTime LastModifiedDate { get; set; }
		
	public IList<ContactInformationViewModel>? ContactInformationList { get; set; }
	public IList<HealthDeclarationViewModel>? HealthDeclarationList { get; set; }
	
}
