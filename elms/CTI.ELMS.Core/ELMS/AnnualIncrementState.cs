using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.ELMS.Core.ELMS;

public record AnnualIncrementState : BaseEntity
{
	public string? UnitOfferedID { get; init; }
	public int? Year { get; init; }
	public decimal? BasicFixedMonthlyRent { get; init; }
	public decimal? PercentageRent { get; init; }
	public decimal? MinimumMonthlyRent { get; init; }
	
	public UnitOfferedState? UnitOffered { get; init; }
	
	
}
