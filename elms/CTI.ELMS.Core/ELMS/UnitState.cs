using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.ELMS.Core.ELMS;

public record UnitState : BaseEntity
{
	public string? UnitNo { get; init; }
	public string? ProjectID { get; init; }
	public decimal? LotBudget { get; init; }
	public decimal? LotArea { get; init; }
	public DateTime AvailabilityDate { get; init; }
	public DateTime CommencementDate { get; init; }
	public string CurrentTenantContractNo { get; init; } = "";
	
	public ProjectState? Project { get; init; }
	
	public IList<UnitBudgetState>? UnitBudgetList { get; set; }
	public IList<UnitActivityState>? UnitActivityList { get; set; }
	public IList<PreSelectedUnitState>? PreSelectedUnitList { get; set; }
	public IList<UnitOfferedState>? UnitOfferedList { get; set; }
	public IList<UnitOfferedHistoryState>? UnitOfferedHistoryList { get; set; }
	public IList<IFCAUnitInformationState>? IFCAUnitInformationList { get; set; }
	
}
