using CompanyPL.Common.Core.Base.Models;
using System.ComponentModel;

namespace CompanyPL.ProjectPL.Core.ProjectPL;

public record SampleParentState : BaseEntity
{
	public string Name { get; init; } = "";
	
	
	public IList<EmployeeState>? EmployeeList { get; set; }
	
}
