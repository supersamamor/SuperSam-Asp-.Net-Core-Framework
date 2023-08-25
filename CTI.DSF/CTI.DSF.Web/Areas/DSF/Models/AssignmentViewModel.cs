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
	[Display(Name = "Task List Id")]
	[Required]
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string TaskListId { get; init; } = "";
	public string?  ReferenceFieldTaskListId { get; set; }
	[Display(Name = "Primary Assignee")]
	[StringLength(36, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? PrimaryAssignee { get; init; }
	[Display(Name = "Alternate Assignee")]
	[Required]
	[StringLength(36, ErrorMessage = "{0} length can't be more than {1}.")]
	public string AlternateAssignee { get; init; } = "";
	[Display(Name = "Start Date")]
	public DateTime? StartDate { get; init; } = DateTime.Now.Date;
	[Display(Name = "End Date")]
	public DateTime? EndDate { get; init; } = DateTime.Now.Date;
	
	public DateTime LastModifiedDate { get; set; }
	public TaskListViewModel? TaskList { get; init; }
		
	public IList<DeliveryViewModel>? DeliveryList { get; set; }
	
}
