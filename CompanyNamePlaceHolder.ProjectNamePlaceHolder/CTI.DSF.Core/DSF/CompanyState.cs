using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.DSF.Core.DSF;

public record CompanyState : BaseEntity
{
	public string CompanyCode { get; init; } = "";
	public string CompanyName { get; init; } = "";
	public bool Active { get; init; }
	
	
	public IList<DepartmentState>? DepartmentList { get; set; }
	public IList<TaskCompanyAssignmentState>? TaskCompanyAssignmentList { get; set; }
	
}
