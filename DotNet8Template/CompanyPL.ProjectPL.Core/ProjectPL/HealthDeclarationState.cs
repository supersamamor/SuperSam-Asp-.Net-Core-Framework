using CompanyPL.Common.Core.Base.Models;
using System.ComponentModel;

namespace CompanyPL.ProjectPL.Core.ProjectPL;

public record HealthDeclarationState : BaseEntity
{
	public string EmployeeId { get; init; } = "";
	public bool? IsVaccinated { get; init; }
	public string? Vaccine { get; init; }
	
	public EmployeeState? Employee { get; init; }
	
	
}
