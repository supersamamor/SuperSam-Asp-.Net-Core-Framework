using CompanyPL.Common.Web.Utility.Extensions;
using CompanyPL.ProjectPL.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CompanyPL.ProjectPL.Web.Areas.ProjectPL.Models;

public record HealthDeclarationViewModel : BaseViewModel
{	
	[Display(Name = "Employee")]
	[Required]
	
	public string EmployeeId { get; init; } = "";
	public string?  ReferenceFieldEmployeeId { get; set; }
	[Display(Name = "Vaccinated")]
	[Required]
	public bool? IsVaccinated { get; init; }
	[Display(Name = "Vaccine")]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Vaccine { get; init; }
	
	public DateTime LastModifiedDate { get; set; }
	public EmployeeViewModel? Employee { get; init; }
		
	
}
