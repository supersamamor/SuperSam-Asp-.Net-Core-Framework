using CompanyPL.Common.Core.Base.Models;
using System.ComponentModel;

namespace CompanyPL.ProjectPL.Core.ProjectPL;

public record EmployeeState : BaseEntity
{
	public string EmployeeCode { get; init; } = "";
	public string FirstName { get; init; } = "";
	public string MiddleName { get; init; } = "";
	public string LastName { get; init; } = "";
	
	
	public IList<ContactInformationState>? ContactInformationList { get; set; }
	public IList<HealthDeclarationState>? HealthDeclarationList { get; set; }
	
}
