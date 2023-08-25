using CTI.Common.Web.Utility.Extensions;
using CTI.DSF.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.DSF.Web.Areas.DSF.Models;

public record TaskListViewModel : BaseViewModel
{	
	[Display(Name = "Task No")]
	public int? TaskListNo { get; init; }
	[Display(Name = "Task Description")]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? TaskDescription { get; init; }
	[Display(Name = "Task Classification")]
	
	public string? TaskClassification { get; init; }
	[Display(Name = "Task Frequency")]
	
	public string? TaskFrequency { get; init; }
	[Display(Name = "Task Due Day")]
	public int? TaskDueDay { get; init; }
	[Display(Name = "Target Due Date")]
	public DateTime? TargetDueDate { get; init; } = DateTime.Now.Date;
	[Display(Name = "Holiday Tag")]
	[Required]
	
	public string HolidayTag { get; init; } = "";
	[Display(Name = "Company")]
	[Required]
	
	public string CompanyId { get; init; } = "";
	public string?  ReferenceFieldCompanyId { get; set; }
	[Display(Name = "Department")]
	
	public string? DepartmentId { get; init; }
	public string?  ReferenceFieldDepartmentId { get; set; }
	[Display(Name = "Section")]
	
	public string? SectionId { get; init; }
	public string?  ReferenceFieldSectionId { get; set; }
	[Display(Name = "Team")]
	
	public string? TeamId { get; init; }
	public string?  ReferenceFieldTeamId { get; set; }
	
	public DateTime LastModifiedDate { get; set; }
	public CompanyViewModel? Company { get; init; }
	public DepartmentViewModel? Department { get; init; }
	public SectionViewModel? Section { get; init; }
	public TeamViewModel? Team { get; init; }
		
	public IList<TaskApproverViewModel>? TaskApproverList { get; set; }
	public IList<TaskTagViewModel>? TaskTagList { get; set; }
	public IList<AssignmentViewModel>? AssignmentList { get; set; }
	
}
