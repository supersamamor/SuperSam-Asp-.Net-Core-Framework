using CompanyPL.Common.Core.Base.Models;
using System.ComponentModel;

namespace CompanyPL.ProjectPL.Core.ProjectPL;

public record EmployeeState : BaseEntity
{
	public DateTime? DateSample { get; init; }
	public bool? RadioButtonSample { get; init; }
	public decimal? DecimalSample { get; init; }
	public int? IntegerSample { get; init; }
	public string SampleParentId { get; init; } = "";
	public string EmployeeCode { get; init; } = "";
	public string MiddleName { get; init; } = "";
	public string FirstName { get; init; } = "";
	public string LastName { get; init; } = "";
	public DateTime? DateTimeSample { get; init; }
	public bool BooleanSample { get; init; }
	
	public SampleParentState? SampleParent { get; init; }
	
	public IList<ContactInformationState>? ContactInformationList { get; set; }
	public IList<HealthDeclarationState>? HealthDeclarationList { get; set; }
	
}
