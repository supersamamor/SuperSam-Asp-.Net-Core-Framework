using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.ELMS.Core.ELMS;

public record UnitOfferedHistoryState : BaseEntity
{
	public string? OfferingID { get; init; }
	public string? UnitID { get; init; }
	public decimal? LotBudget { get; init; }
	public decimal? LotArea { get; init; }
	public string? OfferingHistoryID { get; set; }
	public decimal? BasicFixedMonthlyRent { get; init; }
	public decimal? PercentageRent { get; init; }
	public decimal? MinimumMonthlyRent { get; init; }
	public decimal? AnnualIncrement { get; init; }
	public string? AnnualIncrementInformation { get; init; } = "";
	public bool IsFixedMonthlyRent { get; init; }
	
	public OfferingState? Offering { get; init; }
	public UnitState? Unit { get; init; }
	public OfferingHistoryState? OfferingHistory { get; init; }
	
	public IList<AnnualIncrementHistoryState>? AnnualIncrementHistoryList { get; set; }
	
}
