using CompanyPL.Common.Core.Base.Models;
using System.ComponentModel;

namespace CompanyPL.ProjectPL.Core.ProjectPL;

public record ContactInformationState : BaseEntity
{
	public string EmployeeId { get; init; } = "";
	public string ContactDetails { get; init; } = "";
	
	public EmployeeState? Employee { get; init; }
	
	
}
