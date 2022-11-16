using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.ELMS.Core.ELMS;

public record OfferingState : BaseEntity
{
	public string? ProjectID { get; init; }
	public string? OfferingHistoryID { get; init; }
	public string? Status { get; init; }
	public DateTime? CommencementDate { get; init; }
	public DateTime? TerminationDate { get; init; }
	public int? Year { get; init; }
	public int? Month { get; init; }
	public int? Day { get; init; }
	public int? SecMonths { get; init; }
	public int? ConstructionMonths { get; init; }
	public decimal? OtherChargesAircon { get; init; }
	public string? Concession { get; init; }
	public string? OffersheetRemarks { get; init; }
	public decimal? ConstructionCAMC { get; init; }
	public decimal? CommencementCAMC { get; init; }
	public decimal? BoardUp { get; init; }
	public string? UnitsInformation { get; init; }
	public string? ANType { get; init; }
	public string? LeadID { get; init; }
	public string? TenantContractNo { get; init; }
	public decimal? DocStamp { get; init; }
	public DateTime? AwardNoticeCreatedDate { get; init; }
	public string? AwardNoticeCreatedBy { get; init; }
	public DateTime? SignedOfferSheetDate { get; init; }
	public string? TagSignedOfferSheetBy { get; init; }
	public decimal? TotalUnitArea { get; init; }
	public bool IsPOS { get; init; }
	public string Location { get; init; } = "";
	public decimal? TotalSecurityDeposit { get; init; }
	public decimal? AnnualAdvertisingFee { get; init; }
	public decimal? CAMCConstructionTotalUnitArea { get; init; }
	public int? ConstructionCAMCMonths { get; init; }
	public decimal? ConstructionCAMCRate { get; init; }
	public bool HasBoardUpFee { get; init; }
	public int? SecurityDepositPayableWithinMonths { get; init; }
	public decimal? TotalConstructionBond { get; init; }
	public int? ConstructionPayableWithinMonths { get; init; }
	public decimal? TotalBasicFixedMonthlyRent { get; init; }
	public decimal? TotalMinimumMonthlyRent { get; init; }
	public decimal? TotalLotBudget { get; init; }
	public decimal? TotalPercentageRent { get; init; }
	public bool AutoComputeTotalConstructionBond { get; init; }
	public bool AutoComputeTotalSecurityDeposit { get; init; }
	public int? OfferSheetPerProjectCounter { get; init; }
	public DateTime? SignedAwardNoticeDate { get; init; }
	public string? TagSignedAwardNoticeBy { get; init; }
	public decimal? MinimumSalesQuota { get; init; }
	public string UnitType { get; init; } = "";
	public string? Provision { get; init; }
	public int? FitOutPeriod { get; init; }
	public DateTime? TurnOverDate { get; init; }
	public bool AutoComputeAnnualAdvertisingFee { get; init; }
	public DateTime? BookingDate { get; init; }
	public string? SignatoryName { get; init; }
	public string? SignatoryPosition { get; init; }
	public string? ANSignatoryName { get; init; }
	public string? ANSignatoryPosition { get; init; }
	public DateTime? LeaseContractCreatedDate { get; init; }
	public string? LeaseContractCreatedBy { get; init; }
	public string? TagSignedLeaseContractBy { get; init; }
	public DateTime? SignedLeaseContractDate { get; init; }
	public string? TagForReviewLeaseContractBy { get; init; }
	public DateTime? ForReviewLeaseContractDate { get; init; }
	public string? TagForFinalPrintLeaseContractBy { get; init; }
	public DateTime? ForFinalPrintLeaseContractDate { get; init; }
	public string? LeaseContractStatus { get; init; } = Constants.LeaseContractStatus.NoContract;
	public string? ANTermType { get; init; }
	public int? ContractTypeID { get; init; }
	public string? WitnessName { get; init; }
	public string? PermittedUse { get; init; }
	public string? ModifiedCategory { get; init; }
	public bool IsDisabledModifiedCategory { get; init; }
	public string? ContractNumber { get; init; }
	
	public ProjectState? Project { get; init; }
	public LeadState? Lead { get; init; }
	
	public IList<OfferingHistoryState>? OfferingHistoryList { get; set; }
	public IList<PreSelectedUnitState>? PreSelectedUnitList { get; set; }
	public IList<UnitOfferedState>? UnitOfferedList { get; set; }
	public IList<UnitOfferedHistoryState>? UnitOfferedHistoryList { get; set; }
	public IList<IFCATenantInformationState>? IFCATenantInformationList { get; set; }
	public string OfferSheetNo
	{
		get
		{
			int version = 1;
			if (this.OfferingHistoryList != null && this.OfferingHistoryList.Count > 0)
			{
				version = (int)(this.OfferingHistoryList!.Where(l => l.Id == this.OfferingHistoryID)!.FirstOrDefault()!.OfferingVersion!);
			}

			return this.Project?.IFCAProjectCode?.ToString() + "-" + this.Id.PadLeft(5, '0') + "-" + version.ToString().PadLeft(2, '0');
		}
	}
}
