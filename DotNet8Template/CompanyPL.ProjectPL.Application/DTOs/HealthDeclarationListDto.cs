using CompanyPL.Common.Core.Base.Models;
using System.ComponentModel;

namespace CompanyPL.ProjectPL.Application.DTOs;

public record HealthDeclarationListDto : BaseDto
{
	public string EmployeeId { get; init; } = "";
	public bool? IsVaccinated { get; init; }
	public string IsVaccinatedFormatted { get { return this.IsVaccinated == true ? "Yes" : "No"; } }
	public string? Vaccine { get; init; } 
	
	
}
