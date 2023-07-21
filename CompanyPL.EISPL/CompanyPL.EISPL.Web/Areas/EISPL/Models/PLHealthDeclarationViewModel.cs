using CompanyPL.Common.Web.Utility.Extensions;
using CompanyPL.EISPL.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CompanyPL.EISPL.Web.Areas.EISPL.Models;

public record PLHealthDeclarationViewModel : BaseViewModel
{	
	[Display(Name = "PL Vaccine")]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? PLVaccine { get; init; }
	[Display(Name = "PL Vaccinated")]
	[Required]
	public bool? PLIsVaccinated { get; init; }
	[Display(Name = "PL Employee")]
	[Required]
	
	public string PLEmployeeId { get; init; } = "";
	public string?  ForeignKeyPLEmployee { get; set; }
	
	public DateTime LastModifiedDate { get; set; }
	public PLEmployeeViewModel? PLEmployee { get; init; }
		
	
}
