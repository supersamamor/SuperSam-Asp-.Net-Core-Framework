using CTI.Common.Web.Utility.Extensions;
using CTI.DSF.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.DSF.Web.Areas.DSF.Models;

public record TaskCompanyAssignmentViewModel : BaseViewModel
{	
	[Display(Name = "Task Master")]
	[Required]
	
	public string TaskMasterId { get; init; } = "";
	public string?  ReferenceFieldTaskMasterId { get; set; }
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
	public TaskMasterViewModel? TaskMaster { get; init; }
	public CompanyViewModel? Company { get; init; }
	public DepartmentViewModel? Department { get; init; }
	public SectionViewModel? Section { get; init; }
	public TeamViewModel? Team { get; init; }
		
	public IList<TaskApproverViewModel>? TaskApproverList { get; set; }
	public IList<AssignmentViewModel>? AssignmentList { get; set; }
	public IList<DeliveryViewModel>? DeliveryList { get; set; }
	
}
