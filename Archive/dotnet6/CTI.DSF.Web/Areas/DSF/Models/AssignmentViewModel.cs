using CTI.Common.Web.Utility.Extensions;
using CTI.DSF.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.DSF.Web.Areas.DSF.Models;

public record AssignmentViewModel : BaseViewModel
{	
	[Display(Name = "Assignment Code")]
	[Required]
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string AssignmentCode { get; init; } = "";
	[Display(Name = "Task / Company Assignment")]
	[Required]
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string TaskCompanyAssignmentId { get; init; } = "";
	public string?  ReferenceFieldTaskCompanyAssignmentId { get; set; }
	[Display(Name = "Primary Assignee")]
	[Required]
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string PrimaryAssignee { get; init; } = "";
	[Display(Name = "Alternate Assignee")]
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? AlternateAssignee { get; init; }
	[Display(Name = "Start Date")]
	[Required]
	public DateTime StartDate { get; init; } = DateTime.Now.Date;
	[Display(Name = "End Date")]
	[Required]
	public DateTime EndDate { get; init; } = DateTime.Now.Date;
	
	public DateTime LastModifiedDate { get; set; }
	public TaskCompanyAssignmentViewModel? TaskCompanyAssignment { get; init; }
		
	public IList<DeliveryViewModel>? DeliveryList { get; set; }
	
}
