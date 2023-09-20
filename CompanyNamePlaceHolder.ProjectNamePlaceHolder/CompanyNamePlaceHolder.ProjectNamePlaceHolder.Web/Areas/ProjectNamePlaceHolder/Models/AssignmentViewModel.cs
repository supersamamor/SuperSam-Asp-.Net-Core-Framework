using CompanyNamePlaceHolder.Common.Web.Utility.Extensions;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.ProjectNamePlaceHolder.Models;

public record AssignmentViewModel : BaseViewModel
{	
	[Display(Name = "Assignment Code")]
	[Required]
	
	public string AssignmentCode { get; init; } = "";
	[Display(Name = "Task List Code")]
	[Required]
	
	public string TaskListCode { get; init; } = "";
	public string?  ReferenceFieldTaskListCode { get; set; }
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
