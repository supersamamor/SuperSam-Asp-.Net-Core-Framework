using CompanyPL.Common.Core.Base.Models;
using System.ComponentModel;

namespace CompanyPL.ProjectPL.Application.DTOs;

public record EmployeeListDto : BaseDto
{
	public DateTime? DateSample { get; init; }
	public string RadioButtonSample { get; init; } = "";
	public decimal? DecimalSample { get; init; }
	public string IntegerSample { get; init; } = "";
	public string SampleParentId { get; init; } = "";
	public string EmployeeCode { get; init; } = "";
	public string MiddleName { get; init; } = "";
	public string FirstName { get; init; } = "";
	public string LastName { get; init; } = "";
	public string DateTimeSample { get; init; } = "";
	public string BooleanSample { get; init; } = "";
	
	public string StatusBadge { get; set; } = "";
}
