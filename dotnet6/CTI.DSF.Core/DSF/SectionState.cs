using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.DSF.Core.DSF;

public record SectionState : BaseEntity
{
	public string DepartmentCode { get; init; } = "";
	public string SectionCode { get; init; } = "";
	public string SectionName { get; init; } = "";
	public bool Active { get; init; }
	
	public DepartmentState? Department { get; init; }
	
	public IList<TeamState>? TeamList { get; set; }
	public IList<TaskCompanyAssignmentState>? TaskCompanyAssignmentList { get; set; }
	
}
