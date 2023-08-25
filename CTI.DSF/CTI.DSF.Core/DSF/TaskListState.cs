using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.DSF.Core.DSF;

public record TaskListState : BaseEntity
{
	public int? TaskListNo { get; init; }
	public string? TaskDescription { get; init; }
	public string? TaskClassification { get; init; }
	public string? TaskFrequency { get; init; }
	public int? TaskDueDay { get; init; }
	public DateTime? TargetDueDate { get; init; }
	public string HolidayTag { get; init; } = "";
	public string CompanyId { get; init; } = "";
	public string? DepartmentId { get; init; }
	public string? SectionId { get; init; }
	public string? TeamId { get; init; }
	
	public CompanyState? Company { get; init; }
	public DepartmentState? Department { get; init; }
	public SectionState? Section { get; init; }
	public TeamState? Team { get; init; }
	
	public IList<TaskApproverState>? TaskApproverList { get; set; }
	public IList<TaskTagState>? TaskTagList { get; set; }
	public IList<AssignmentState>? AssignmentList { get; set; }
	
}
