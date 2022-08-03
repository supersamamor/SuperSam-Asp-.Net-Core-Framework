using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.TenantSales.Core.TenantSales;

public record ProjectState : BaseEntity
{
	public string CompanyId { get; init; } = "";
	public string Name { get; init; } = "";
	public string Code { get; init; } = "";
	public string? PayableTo { get; init; }
	public string? ProjectAddress { get; init; }
	public string? Location { get; init; }
	public string? ProjectNameANSection { get; init; }
	public decimal LandArea { get; init; }
	public decimal GLA { get; init; }
	public bool IsDisabled { get; init; }
	public string? SignatoryName { get; init; }
	public string? SignatoryPosition { get; init; }
	public string? ANSignatoryName { get; init; }
	public string? ANSignatoryPosition { get; init; }
	public string? ContractSignatory { get; init; }
	public string? ContractSignatoryPosition { get; init; }
	public string? ContractSignatoryProofOfIdentity { get; init; }
	public string? ContractSignatoryWitness { get; init; }
	public string? ContractSignatoryWitnessPosition { get; init; }
	public bool OutsideFC { get; init; }
	public string? ProjectGreetingsSection { get; init; }
	public string? ProjectShortName { get; init; }
	public string? SignatureUpper { get; init; }
	public string? SignatureLower { get; init; }
	public bool HasAssociationDues { get; init; }
	public string? SalesUploadFolder { get; init; }
	public bool EnableMeterReadingApp { get; init; }
	public string? CurrencyCode { get; init; }
	public int? CurrencyRate { get; init; }
	public int? GasCutOff { get; init; }
	public int? PowerCutOff { get; init; }
	public int? WaterCutOff { get; init; }
	
	public CompanyState? Company { get; init; }
	
	public IList<TenantState>? TenantList { get; set; }
	public IList<ProjectBusinessUnitState>? ProjectBusinessUnitList { get; set; }
	public IList<LevelState>? LevelList { get; set; }
	public IList<RevalidateState>? RevalidateList { get; set; }
	
}
