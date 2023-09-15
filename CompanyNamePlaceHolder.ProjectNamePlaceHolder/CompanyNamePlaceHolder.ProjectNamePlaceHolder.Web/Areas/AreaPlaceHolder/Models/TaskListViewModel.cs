using CompanyNamePlaceHolder.Common.Web.Utility.Extensions;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.AreaPlaceHolder.Models;

public record TaskListViewModel : BaseViewModel
{	
	[Display(Name = "Task List Code")]
	[Required]
	
	public string TaskListCode { get; init; } = "";
	[Display(Name = "Task Description")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string TaskDescription { get; init; } = "";
	[Display(Name = "Task Type")]
	[Required]
	
	public string TaskType { get; init; } = "";
	[Display(Name = "Task Frequency")]
	[Required]
	
	public string TaskFrequency { get; init; } = "";
	[Display(Name = "Task Due Day")]
	[Required]
	public int TaskDueDay { get; init; }
	[Display(Name = "Target Due Date")]
	[Required]
	public DateTime TargetDueDate { get; init; } = DateTime.Now.Date;
	[Display(Name = "Primary Endorser")]
	
	public string? PrimaryEndorser { get; init; }
	[Display(Name = "Primary Approver")]
	[Required]
	
	public string PrimaryApprover { get; init; } = "";
	[Display(Name = "Alternate Endorser")]
	
	public string? AlternateEndorser { get; init; }
	[Display(Name = "Alternate Approver")]
	[Required]
	
	public string AlternateApprover { get; init; } = "";
	
	public DateTime LastModifiedDate { get; set; }
		
	public IList<AssignmentViewModel>? AssignmentList { get; set; }
	
}
