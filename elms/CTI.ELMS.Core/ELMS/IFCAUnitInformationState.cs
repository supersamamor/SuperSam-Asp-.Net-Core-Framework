using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.ELMS.Core.ELMS;

public record IFCAUnitInformationState : BaseEntity
{
	public string? UnitID { get; init; }
	public string? IFCATenantInformationID { get; init; }
	public decimal? RentalRate { get; init; }
	public decimal? BudgetedAmount { get; init; }
	public DateTime? StartDate { get; init; }
	public DateTime? EndDate { get; init; }
	public decimal? BasicFixedMonthlyRent { get; init; }
	
	public UnitState? Unit { get; init; }
	public IFCATenantInformationState? IFCATenantInformation { get; init; }
	
	
}
