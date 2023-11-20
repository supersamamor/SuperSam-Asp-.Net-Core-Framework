using CTI.Common.Web.Utility.Extensions;
using CTI.DSF.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.DSF.Web.Areas.DSF.Models;

public record TaskMasterViewModel : BaseViewModel
{	
	[Display(Name = "Task No")]
	[Required]
	public int TaskNo { get; init; }
	[Display(Name = "Task Description")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string TaskDescription { get; init; } = "";
	[Display(Name = "Task Classification")]
	[Required]
	
	public string TaskClassification { get; init; } = "";
	[Display(Name = "Task Frequency")]
	[Required]
	
	public string TaskFrequency { get; init; } = "";
	[Display(Name = "Task Due Day")]
	public int? TaskDueDay { get; init; }
	[Display(Name = "Target Due Date")]
	public DateTime? TargetDueDate { get; init; } = DateTime.Now.Date;
	[Display(Name = "Holiday Tag")]
	[Required]
	
	public string HolidayTag { get; init; } = "";
	[Display(Name = "Active")]
	public bool Active { get; init; }
	
	public DateTime LastModifiedDate { get; set; }
		
	public IList<TaskCompanyAssignmentViewModel>? TaskCompanyAssignmentList { get; set; }
	public IList<TaskTagViewModel>? TaskTagList { get; set; }
	
}
