using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.DSF.Core.DSF;

public record DepartmentState : BaseEntity
{
	public string CompanyCode { get; init; } = "";
	public string? DepartmentCode { get; init; }
	public string? DepartmentName { get; init; }
	
	public CompanyState? Company { get; init; }
	
	public IList<SectionState>? SectionList { get; set; }
	public IList<TaskListState>? TaskListList { get; set; }
	
}
