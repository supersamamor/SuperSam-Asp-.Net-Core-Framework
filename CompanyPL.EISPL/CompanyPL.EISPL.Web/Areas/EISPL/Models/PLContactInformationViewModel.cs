using CompanyPL.Common.Web.Utility.Extensions;
using CompanyPL.EISPL.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CompanyPL.EISPL.Web.Areas.EISPL.Models;

public record PLContactInformationViewModel : BaseViewModel
{	
	[Display(Name = "PL Contact Details")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string PLContactDetails { get; init; } = "";
	[Display(Name = "PL Employee")]
	[Required]
	
	public string PLEmployeeId { get; init; } = "";
	public string?  ForeignKeyPLEmployee { get; set; }
	
	public DateTime LastModifiedDate { get; set; }
	public PLEmployeeViewModel? PLEmployee { get; init; }
		
	
}
