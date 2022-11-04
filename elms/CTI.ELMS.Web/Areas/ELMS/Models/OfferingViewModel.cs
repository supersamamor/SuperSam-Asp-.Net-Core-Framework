using CTI.Common.Web.Utility.Extensions;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.ELMS.Web.Areas.ELMS.Models;

public record OfferingViewModel : BaseViewModel
{	
	[Display(Name = "Project")]
	
	public string? ProjectID { get; init; }
	public string?  ForeignKeyProject { get; set; }
	[Display(Name = "Offering History")]
	public int? OfferingHistoryID { get; init; }
	[Display(Name = "Status")]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Status { get; init; }
	[Display(Name = "Commencement Date")]
	public DateTime? CommencementDate { get; init; } = DateTime.Now.Date;
	[Display(Name = "Termination Date")]
	public DateTime? TerminationDate { get; init; } = DateTime.Now.Date;
	[Display(Name = "Year")]
	public int? Year { get; init; }
	[Display(Name = "Month")]
	public int? Month { get; init; }
	[Display(Name = "Day")]
	public int? Day { get; init; }
	[Display(Name = "Sec Months")]
	public int? SecMonths { get; init; }
	[Display(Name = "Construction Months")]
	public int? ConstructionMonths { get; init; }
	[Display(Name = "Other Charges / Aircon")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? OtherChargesAircon { get; init; }
	[Display(Name = "Concession")]
	
	public string? Concession { get; init; }
	[Display(Name = "Offersheet Remarks")]
	
	public string? OffersheetRemarks { get; init; }
	[Display(Name = "Construction CAMC")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? ConstructionCAMC { get; init; }
	[Display(Name = "Commencement CAMC")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? CommencementCAMC { get; init; }
	[Display(Name = "Board Up")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? BoardUp { get; init; }
	[Display(Name = "Units Information")]
	
	public string? UnitsInformation { get; init; }
	[Display(Name = "ANType")]
	[StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? ANType { get; init; }
	[Display(Name = "Lead")]
	
	public string? LeadID { get; init; }
	public string?  ForeignKeyLead { get; set; }
	[Display(Name = "Tenant Contract No.")]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? TenantContractNo { get; init; }
	[Display(Name = "Doc Stamp")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? DocStamp { get; init; }
	[Display(Name = "Award Notice Created Date")]
	public DateTime? AwardNoticeCreatedDate { get; init; } = DateTime.Now.Date;
	[Display(Name = "Award Notice Created By")]
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? AwardNoticeCreatedBy { get; init; }
	[Display(Name = "Signed Offersheet Date")]
	public DateTime? SignedOfferSheetDate { get; init; } = DateTime.Now.Date;
	[Display(Name = "Tag Signed Offersheet By")]
	
	public string? TagSignedOfferSheetBy { get; init; }
	[Display(Name = "Total Unit Area")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? TotalUnitArea { get; init; }
	[Display(Name = "Is POS")]
	public bool IsPOS { get; init; }
	[Display(Name = "Location")]
	[Required]
	
	public string Location { get; init; } = "";
	[Display(Name = "Total Security Deposit")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? TotalSecurityDeposit { get; init; }
	[Display(Name = "Annual Advertising Fee")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? AnnualAdvertisingFee { get; init; }
	[Display(Name = "CAMC Construction Total Unit Area")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? CAMCConstructionTotalUnitArea { get; init; }
	[Display(Name = "Construction CAMC Months")]
	public int? ConstructionCAMCMonths { get; init; }
	[Display(Name = "Construction CAMC Rate")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? ConstructionCAMCRate { get; init; }
	[Display(Name = "Has Board Up Fee")]
	public bool HasBoardUpFee { get; init; }
	[Display(Name = "Security Deposit Payable Within Months")]
	public int? SecurityDepositPayableWithinMonths { get; init; }
	[Display(Name = "Total Construction Bond")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? TotalConstructionBond { get; init; }
	[Display(Name = "Construction Payable Within Months")]
	public int? ConstructionPayableWithinMonths { get; init; }
	[Display(Name = "Total Basic Fixed Monthly Rent")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? TotalBasicFixedMonthlyRent { get; init; }
	[Display(Name = "Total Minimum Monthly Rent")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? TotalMinimumMonthlyRent { get; init; }
	[Display(Name = "Total Lot Budget")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? TotalLotBudget { get; init; }
	[Display(Name = "Total Percentage Rent")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? TotalPercentageRent { get; init; }
	[Display(Name = "Auto-compute Total Construction Bond")]
	public bool AutoComputeTotalConstructionBond { get; init; }
	[Display(Name = "Auto-compute Total Security Deposit")]
	public bool AutoComputeTotalSecurityDeposit { get; init; }
	[Display(Name = "Offer Sheet Per Project Counter")]
	public int? OfferSheetPerProjectCounter { get; init; }
	[Display(Name = "Signed Award Notice Date")]
	public DateTime? SignedAwardNoticeDate { get; init; } = DateTime.Now.Date;
	[Display(Name = "Tag Signed Award Notice By")]
	
	public string? TagSignedAwardNoticeBy { get; init; }
	[Display(Name = "Minimum Sales")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? MinimumSalesQuota { get; init; }
	[Display(Name = "Unit Type")]
	[Required]
	
	public string UnitType { get; init; } = "";
	[Display(Name = "Provision")]
	
	public string? Provision { get; init; }
	[Display(Name = "Fit-Out Period")]
	public int? FitOutPeriod { get; init; }
	[Display(Name = "Turn Over Date")]
	public DateTime? TurnOverDate { get; init; } = DateTime.Now.Date;
	[Display(Name = "Auto Compute Annual Advertising Fee")]
	public bool AutoComputeAnnualAdvertisingFee { get; init; }
	[Display(Name = "Booking Date")]
	public DateTime? BookingDate { get; init; } = DateTime.Now.Date;
	[Display(Name = "Signatory Name")]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? SignatoryName { get; init; }
	[Display(Name = "Signatory Position")]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? SignatoryPosition { get; init; }
	[Display(Name = "AN Signatory Name")]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? ANSignatoryName { get; init; }
	[Display(Name = "AN Signatory Position")]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? ANSignatoryPosition { get; init; }
	[Display(Name = "Lease Contract Created Date")]
	public DateTime? LeaseContractCreatedDate { get; init; } = DateTime.Now.Date;
	[Display(Name = "Lease Contract Created By")]
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? LeaseContractCreatedBy { get; init; }
	[Display(Name = "Tag Signed Lease Contract By")]
	
	public string? TagSignedLeaseContractBy { get; init; }
	[Display(Name = "Signed Lease Contract Date")]
	public DateTime? SignedLeaseContractDate { get; init; } = DateTime.Now.Date;
	[Display(Name = "Tag For Review Lease Contract By")]
	
	public string? TagForReviewLeaseContractBy { get; init; }
	[Display(Name = "For Review Lease Contract Date")]
	public DateTime? ForReviewLeaseContractDate { get; init; } = DateTime.Now.Date;
	[Display(Name = "Tag For Final Print Lease Contract By")]
	
	public string? TagForFinalPrintLeaseContractBy { get; init; }
	[Display(Name = "For Final Print Lease Contract Date")]
	public DateTime? ForFinalPrintLeaseContractDate { get; init; } = DateTime.Now.Date;
	[Display(Name = "Lease Contract")]
	public int? LeaseContractStatus { get; init; }
	[Display(Name = "AN Term Type")]
	public int? ANTermTypeID { get; init; }
	[Display(Name = "Contract Type")]
	public int? ContractTypeID { get; init; }
	[Display(Name = "Witness Name")]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? WitnessName { get; init; }
	[Display(Name = "Permitted Use")]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? PermittedUse { get; init; }
	[Display(Name = "Modified Category")]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? ModifiedCategory { get; init; }
	[Display(Name = "Is Disabled Modified Category")]
	public bool IsDisabledModifiedCategory { get; init; }
	[Display(Name = "Contract Number")]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? ContractNumber { get; init; }
	
	public DateTime LastModifiedDate { get; set; }
	public ProjectViewModel? Project { get; init; }
	public LeadViewModel? Lead { get; init; }
		
	public IList<OfferingHistoryViewModel>? OfferingHistoryList { get; set; }
	public IList<PreSelectedUnitViewModel>? PreSelectedUnitList { get; set; }
	public IList<UnitOfferedViewModel>? UnitOfferedList { get; set; }
	public IList<UnitOfferedHistoryViewModel>? UnitOfferedHistoryList { get; set; }
	public IList<IFCATenantInformationViewModel>? IFCATenantInformationList { get; set; }
	
}
