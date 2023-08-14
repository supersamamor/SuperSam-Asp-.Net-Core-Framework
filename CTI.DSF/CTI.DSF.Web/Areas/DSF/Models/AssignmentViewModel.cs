using CTI.Common.Web.Utility.Extensions;
using CTI.DSF.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.DSF.Web.Areas.DSF.Models;

public record AssignmentViewModel : BaseViewModel
{	
	[Display(Name = "Assignment Code")]
	
	public string? AssignmentCode { get; init; }
	[Display(Name = "Task List Code")]
	
	public string? TaskListCode { get; init; }
	public string?  ForeignKeyTaskList { get; set; }
	[Display(Name = "Primary Assignee")]
	[Required]
	
	public string PrimaryAsignee { get; init; } = "";
	[Display(Name = "Alternate Assignee")]
	
	public string? AlternateAsignee { get; init; }
	[Display(Name = "Start Date")]
	[Required]
	public DateTime StartDate { get; init; } = DateTime.Now.Date;
	[Display(Name = "End Date")]
	[Required]
	public DateTime EndDate { get; init; } = DateTime.Now.Date;
	
	public DateTime LastModifiedDate { get; set; }
	public TaskListViewModel? TaskList { get; init; }
		
	
}
