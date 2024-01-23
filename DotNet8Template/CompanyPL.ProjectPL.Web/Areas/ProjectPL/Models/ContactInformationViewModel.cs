using CompanyPL.Common.Web.Utility.Extensions;
using CompanyPL.ProjectPL.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CompanyPL.ProjectPL.Web.Areas.ProjectPL.Models;

public record ContactInformationViewModel : BaseViewModel
{	
	[Display(Name = "Employee")]
	[Required]
	
	public string EmployeeId { get; init; } = "";
	public string?  ReferenceFieldEmployeeId { get; set; }
	[Display(Name = "Contact Details")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string ContactDetails { get; init; } = "";
	
	public DateTime LastModifiedDate { get; set; }
	public EmployeeViewModel? Employee { get; init; }
		
	
}
