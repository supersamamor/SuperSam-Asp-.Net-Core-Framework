using CTI.Common.Web.Utility.Extensions;
using CTI.DSF.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.DSF.Web.Areas.DSF.Models;

public record TeamViewModel : BaseViewModel
{	
	[Display(Name = "Section Code")]
	[Required]
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string SectionCode { get; init; } = "";
	public string?  ReferenceFieldSectionCode { get; set; }
	[Display(Name = "Team Code")]
	[Required]
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string TeamCode { get; init; } = "";
	[Display(Name = "Team Name")]
	[Required]
	
	public string TeamName { get; init; } = "";
	[Display(Name = "Active")]
	public bool Active { get; init; }
	
	public DateTime LastModifiedDate { get; set; }
	public SectionViewModel? Section { get; init; }
		
	public IList<TaskCompanyAssignmentViewModel>? TaskCompanyAssignmentList { get; set; }
	
}
