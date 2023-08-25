using CTI.Common.Web.Utility.Extensions;
using CTI.DSF.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.DSF.Web.Areas.DSF.Models;

public record TaskApproverViewModel : BaseViewModel
{	
	[Display(Name = "Approver")]
	[Required]
	[StringLength(36, ErrorMessage = "{0} length can't be more than {1}.")]
	public string ApproverUserId { get; init; } = "";
	[Display(Name = "Task List")]
	[Required]
	
	public string TaskListId { get; init; } = "";
	public string?  ReferenceFieldTaskListId { get; set; }
	[Display(Name = "Approver Type")]
	[Required]
	
	public string ApproverType { get; init; } = "";
	[Display(Name = "Is Primary")]
	public bool IsPrimary { get; init; }
	[Display(Name = "Sequence")]
	[Required]
	public int Sequence { get; init; }
	
	public DateTime LastModifiedDate { get; set; }
	public TaskListViewModel? TaskList { get; init; }
		
	
}
