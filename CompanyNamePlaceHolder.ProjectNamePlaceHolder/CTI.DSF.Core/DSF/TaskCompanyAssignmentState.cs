using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.DSF.Core.DSF;

public record TaskCompanyAssignmentState : BaseEntity
{
	public string TaskMasterId { get; init; } = "";
	public string CompanyId { get; init; } = "";
	public string? DepartmentId { get; init; }
	public string? SectionId { get; init; }
	public string? TeamId { get; init; }
	
	public TaskMasterState? TaskMaster { get; init; }
	public CompanyState? Company { get; init; }
	public DepartmentState? Department { get; init; }
	public SectionState? Section { get; init; }
	public TeamState? Team { get; init; }
	
	public IList<TaskApproverState>? TaskApproverList { get; set; }
	public IList<AssignmentState>? AssignmentList { get; set; }
	public IList<DeliveryState>? DeliveryList { get; set; }
	
}
