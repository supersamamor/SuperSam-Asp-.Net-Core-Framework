using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.ELMS.Core.ELMS;

public record ProjectState : BaseEntity
{
	public string ProjectName { get; init; } = "";
	public string? DatabaseSource { get; init; }
	public string? EntityGroupId { get; init; }
	public string? IFCAProjectCode { get; init; }
	public string PayableTo { get; init; } = "";
	public string ProjectAddress { get; init; } = "";
	public string? Location { get; init; }
	public string ProjectNameANSection { get; init; } = "";
	public decimal? LandArea { get; init; }
	public decimal? GLA { get; init; }
	public bool IsDisabled { get; init; }
	public string SignatoryName { get; init; } = "";
	public string SignatoryPosition { get; init; } = "";
	public string ANSignatoryName { get; init; } = "";
	public string ANSignatoryPosition { get; init; } = "";
	public string ContractSignatory { get; init; } = "";
	public string ContractSignatoryPosition { get; init; } = "";
	public string ContractSignatoryProofOfIdentity { get; init; } = "";
	public string? ContractSignatoryWitness { get; init; }
	public string? ContractSignatoryWitnessPosition { get; init; }
	public bool OutsideFC { get; init; }
	public string? ProjectGreetingsSection { get; init; }
	public string? ProjectShortName { get; init; }
	public string? SignatureUpper { get; init; }
	public string? SignatureLower { get; init; }
	public bool HasAssociationDues { get; init; }
	
	public EntityGroupState? EntityGroup { get; init; }
	
	public IList<UserProjectAssignmentState>? UserProjectAssignmentList { get; set; }
	public IList<UnitState>? UnitList { get; set; }
	public IList<UnitBudgetState>? UnitBudgetList { get; set; }
	public IList<ActivityState>? ActivityList { get; set; }
	public IList<OfferingState>? OfferingList { get; set; }
	public IList<OfferingHistoryState>? OfferingHistoryList { get; set; }
	public IList<IFCATenantInformationState>? IFCATenantInformationList { get; set; }
	public IList<IFCAARLedgerState>? IFCAARLedgerList { get; set; }
	public IList<IFCAARAllocationState>? IFCAARAllocationList { get; set; }
	public IList<ReportTableCollectionDetailState>? ReportTableCollectionDetailList { get; set; }
	
}
