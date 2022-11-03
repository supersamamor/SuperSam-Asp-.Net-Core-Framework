using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.ELMS.Core.ELMS;

public record PreSelectedUnitState : BaseEntity
{
	public string? OfferingID { get; init; }
	public string? UnitID { get; init; }
	public decimal? LotBudget { get; init; }
	public decimal? LotArea { get; init; }
	
	public OfferingState? Offering { get; init; }
	public UnitState? Unit { get; init; }
	
	
}
