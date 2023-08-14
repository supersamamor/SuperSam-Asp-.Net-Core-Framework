using CTI.Common.Web.Utility.Extensions;
using CTI.DSF.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.DSF.Web.Areas.DSF.Models;

public record TeamViewModel : BaseViewModel
{	
	[Display(Name = "Section Code")]
	
	public string? SectionCode { get; init; }
	public string?  ForeignKeySection { get; set; }
	[Display(Name = "Team Code")]
	[Required]
	
	public string TeamCode { get; init; } = "";
	[Display(Name = "Team Name")]
	[Required]
	
	public string TeamName { get; init; } = "";
	
	public DateTime LastModifiedDate { get; set; }
	public SectionViewModel? Section { get; init; }
		
	
}
