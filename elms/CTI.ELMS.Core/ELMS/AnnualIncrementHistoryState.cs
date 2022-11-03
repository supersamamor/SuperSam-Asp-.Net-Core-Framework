using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.ELMS.Core.ELMS;

public record AnnualIncrementHistoryState : BaseEntity
{
	public string? UnitOfferedHistoryID { get; init; }
	public int? Year { get; init; }
	public decimal? BasicFixedMonthlyRent { get; init; }
	public decimal? PercentageRent { get; init; }
	public decimal? MinimumMonthlyRent { get; init; }
	public int? ContractGroupingCount { get; init; }
	
	public UnitOfferedHistoryState? UnitOfferedHistory { get; init; }
	
	
}
