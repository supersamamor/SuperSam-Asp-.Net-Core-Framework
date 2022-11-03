using CTI.Common.Web.Utility.Extensions;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.ELMS.Web.Areas.ELMS.Models;

public record UserProjectAssignmentViewModel : BaseViewModel
{	
	[Display(Name = "User Id")]
	
	public string? UserId { get; init; }
	[Display(Name = "Project")]
	
	public string? ProjectID { get; init; }
	public string?  ForeignKeyProject { get; set; }
	
	public DateTime LastModifiedDate { get; set; }
	public ProjectViewModel? Project { get; init; }
		
	
}
