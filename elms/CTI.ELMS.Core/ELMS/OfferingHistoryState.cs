using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.ELMS.Core.ELMS;

public record OfferingHistoryState : BaseEntity
{
	public string? OfferingID { get; init; }
	public string? Status { get; init; }
	public DateTime? CommencementDate { get; init; }
	public DateTime? TerminationDate { get; init; }
	public int? Year { get; init; }
	public int? Month { get; init; }
	public int? Day { get; init; }
	public int? SecMonths { get; init; }
	public int? ConstructionMonths { get; init; }
	public decimal? OtherChargesAircon { get; init; }
	public decimal? ConstructionCAMC { get; init; }
	public decimal? CommencementCAMC { get; init; }
	public string? Concession { get; init; }
	public string? OffersheetRemarks { get; init; }
	public string? UnitsInformation { get; init; }
	public string? ANType { get; init; }
	public string? LeadID { get; init; }
	public string? ProjectID { get; init; }
	public decimal? TotalUnitArea { get; init; }
	public decimal? TotalSecurityDeposit { get; init; }
	public decimal? AnnualAdvertisingFee { get; init; }
	public decimal? CAMCConstructionTotalUnitArea { get; init; }
	public int? ConstructionCAMCMonths { get; init; }
	public decimal? ConstructionCAMCRate { get; init; }
	public bool HasBoardUpFee { get; init; }
	public int? SecurityDepositPayableWithinMonths { get; init; }
	public decimal? TotalConstructionBond { get; init; }
	public int? ConstructionPayableWithinMonths { get; init; }
	public bool IsPOS { get; init; }
	public string Location { get; init; } = "";
	public int? OfferingVersion { get; private set; }
	public decimal? TotalBasicFixedMonthlyRent { get; init; }
	public decimal? TotalMinimumMonthlyRent { get; init; }
	public decimal? TotalLotBudget { get; init; }
	public decimal? TotalPercentageRent { get; init; }
	public bool AutoComputeTotalConstructionBond { get; init; }
	public bool AutoComputeTotalSecurityDeposit { get; init; }
	public decimal? MinimumSalesQuota { get; init; }
	public string UnitType { get; init; } = "";
	public string? Provision { get; init; }
	public int? FitOutPeriod { get; init; }
	public DateTime? TurnOverDate { get; init; }
	public bool AutoComputeAnnualAdvertisingFee { get; init; }
	public DateTime? BookingDate { get; init; }	
	public OfferingState? Offering { get; init; }
	public LeadState? Lead { get; init; }
	public ProjectState? Project { get; init; }	
	public IList<UnitOfferedHistoryState>? UnitOfferedHistoryList { get; set; }
	public IList<UnitGroupState>? UnitGroupList { get; set; }
	public void SetOfferingVersion(int offeringVersion)
	{
		this.OfferingVersion = offeringVersion;
	}
	public string OfferSheetNo
	{
		get
		{		
			return this.Project?.IFCAProjectCode?.ToString() + "-" + this.Offering?.OfferSheetId + "-" + this.OfferingVersion?.ToString().PadLeft(2, '0');
		}
	}
}
