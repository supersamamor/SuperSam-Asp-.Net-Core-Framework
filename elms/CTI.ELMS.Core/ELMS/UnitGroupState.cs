using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.ELMS.Core.ELMS;

public record UnitGroupState : BaseEntity
{
	public string UnitsInformation { get; init; } = "";
	public decimal? LotArea { get; init; }
	public decimal? BasicFixedMonthlyRent { get; init; }
	public string? OfferingHistoryID { get; init; }
	public int? UnitOfferedHistoryID { get; init; }
	public bool IsFixedMonthlyRent { get; init; }
	public string AreaTypeDescription { get; init; } = "";
	
	public OfferingHistoryState? OfferingHistory { get; init; }
	
	
}
