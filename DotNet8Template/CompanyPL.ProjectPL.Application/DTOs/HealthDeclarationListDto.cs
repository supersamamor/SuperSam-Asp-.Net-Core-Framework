using CompanyPL.Common.Core.Base.Models;
using System.ComponentModel;

namespace CompanyPL.ProjectPL.Application.DTOs;

public record HealthDeclarationListDto : BaseDto
{
	public string EmployeeId { get; init; } = "";
	public string IsVaccinated { get; init; } = "";
	public string Vaccine { get; init; } = "";
	
	
}
