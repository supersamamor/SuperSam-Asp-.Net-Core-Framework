using CompanyPL.Common.Core.Base.Models;
using System.ComponentModel;

namespace CompanyPL.ProjectPL.Application.DTOs;

public record EmployeeListDto : BaseDto
{
	public DateTime? DateSample { get; init; }
	public string DateSampleFormatted { get { return this.DateSample == null ? "" : this.DateSample!.Value.ToString("MMM dd, yyyy"); } }
	public bool? RadioButtonSample { get; init; }
	public string RadioButtonSampleFormatted { get { return this.RadioButtonSample == true ? "Yes" : "No"; } }
	public decimal? DecimalSample { get; init; }
	public string DecimalSampleFormatted { get { return this.DecimalSample == null ? "" : this.DecimalSample!.Value.ToString("##,##.00"); } }
	public int? IntegerSample { get; init; }
	public string IntegerSampleFormatted { get { return this.IntegerSample == null ? "" : this.IntegerSample!.Value.ToString("##,##"); } }
	public string? SampleParentTestF { get; init; } = "";
	public string EmployeeCode { get; init; } = ""; 
	public string MiddleName { get; init; } = ""; 
	public string FirstName { get; init; } = ""; 
	public string LastName { get; init; } = ""; 
	public DateTime? DateTimeSample { get; init; }
	public string DateTimeSampleFormatted { get { return this.DateTimeSample == null ? "" : this.DateTimeSample!.Value.ToString("MMM dd, yyyy HH:mm"); } }
	public bool BooleanSample { get; init; }
	public string BooleanSampleFormatted { get { return this.BooleanSample == true ? "Yes" : "No"; } }
	
	public string StatusBadge { get; set; } = "";
}
