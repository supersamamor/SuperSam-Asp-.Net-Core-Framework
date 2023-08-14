using CTI.Common.Web.Utility.Extensions;
using CTI.DSF.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using CTI.DSF.Core.DSF;
using System.ComponentModel.DataAnnotations;

namespace CTI.DSF.Web.Areas.DSF.Models;

public record TaskListViewModel : BaseViewModel
{	
	[Display(Name = "Task List Code")]
	public string? TaskListCode { get; init; }

	[Display(Name = "Task Description")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string TaskDescription { get; init; } = "";

    [Display(Name = "Sub Task")]
    public string? SubTask { get; init; }

    [Display(Name = "Task Classification ")]
	[Required]
    public string TaskClassification { get; init; } = "";
	[Display(Name = "Task Frequency")]
	[Required]
	
	public string TaskFrequency { get; init; } = "";
	[Display(Name = "Task Due Day")]	
	public int? TaskDueDay { get; init; }
	[Display(Name = "Target Due Date")]
	public DateTime? TargetDueDate { get; init; } 

    [Display(Name = "Entity")]
    [Required]
    public string? Company { get; init; }

    [Display(Name = "Department")]
    [Required]
    public string? Department { get; init; }

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
    public string? ParentTaskId { get; init; }
    public IList<AssignmentViewModel>? AssignmentList { get; set; }
    public IList<TaskListViewModel>? ChildTaskList { get; set; }
}
