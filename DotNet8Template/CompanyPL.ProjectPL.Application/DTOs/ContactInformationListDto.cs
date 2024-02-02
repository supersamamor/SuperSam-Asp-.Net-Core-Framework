using CompanyPL.Common.Core.Base.Models;
using System.ComponentModel;

namespace CompanyPL.ProjectPL.Application.DTOs;

public record ContactInformationListDto : BaseDto
{
	public string EmployeeId { get; init; } = "";
	public string ContactDetails { get; init; } = "";
	
	
}
