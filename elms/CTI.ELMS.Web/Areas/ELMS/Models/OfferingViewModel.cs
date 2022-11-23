using CTI.Common.Web.Utility.Extensions;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.ELMS.Web.Areas.ELMS.Models;

public record OfferingViewModel : BaseViewModel
{
    [Display(Name = "Project")]
    [Required]
    public string ProjectID { get; init; } = "";
    public string? ForeignKeyProjectName { get; set; }
    public string? ForeignKeyProject { get; set; }
    [Display(Name = "Offering History")]
    public string? OfferingHistoryID { get; init; }
    [Display(Name = "Status")]
    [StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
    public string? Status { get; init; }
    [Display(Name = "Commencement Date")]
    [Required]
    public DateTime? CommencementDate { get; init; } = DateTime.Now.Date;
    [Display(Name = "Termination Date")]
    [Required]
    public DateTime? TerminationDate { get; set; } = DateTime.Now.Date;
    [Display(Name = "Year")]
    public int? Year { get; set; }
    [Display(Name = "Month")]
    public int? Month { get; set; }
    [Display(Name = "Day")]
    public int? Day { get; set; }
    [Display(Name = "Sec Months")]
    public int? SecMonths { get; init; } = 0;
    [Display(Name = "Construction Months")]
    public int? ConstructionMonths { get; init; } = 0;
    [Display(Name = "Other Charges / Aircon")]

    [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
    public decimal? OtherChargesAircon { get; init; } = 0;
    [Display(Name = "Concession")]

    public string? Concession { get; init; }
    [Display(Name = "Offersheet Remarks")]

    public string? OffersheetRemarks { get; init; }
    [Display(Name = "Construction CAMC")]

    [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
    public decimal? ConstructionCAMC { get; set; } = 0;
    [Display(Name = "Commencement CAMC")]

    [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
    public decimal? CommencementCAMC { get; init; } = 0;
    [Display(Name = "Units Information")]

    public string? UnitsInformation { get; init; }
    [Display(Name = "ANType")]
    [StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
    public string? ANType { get; init; }
    [Display(Name = "Lead")]

    public string? LeadID { get; init; }
    public string? ForeignKeyLead { get; set; }
    [Display(Name = "Tenant Contract No.")]
    [StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
    public string? TenantContractNo { get; init; }
    [Display(Name = "Doc Stamp")]

    [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
    public decimal? DocStamp { get; init; } = 0;
    [Display(Name = "Award Notice Created Date")]
    public DateTime? AwardNoticeCreatedDate { get; init; }
    [Display(Name = "Award Notice Created By")]
    [StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
    public string? AwardNoticeCreatedBy { get; init; }
    [Display(Name = "Signed Offersheet Date")]
    public DateTime? SignedOfferSheetDate { get; init; }
    [Display(Name = "Tag Signed Offersheet By")]

    public string? TagSignedOfferSheetBy { get; init; }
    [Display(Name = "Is POS")]
    public bool IsPOS { get; init; }
    [Display(Name = "Total Security Deposit")]

    [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
    public decimal? TotalSecurityDeposit { get; set; } = 0;
    [Display(Name = "Annual Advertising Fee")]

    [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
    public decimal? AnnualAdvertisingFee { get; set; } = 0;
    [Display(Name = "CAMC Construction Total Unit Area")]

    [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
    public decimal? CAMCConstructionTotalUnitArea { get; set; } = 0;
    [Display(Name = "Construction CAMC Months")]
    public int? ConstructionCAMCMonths { get; init; } = 0;
    [Display(Name = "Construction CAMC Rate")]

    [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
    public decimal? ConstructionCAMCRate { get; init; } = 0;
    [Display(Name = "Has Board Up Fee")]
    public bool HasBoardUpFee { get; init; }
    [Display(Name = "Security Deposit Payable Within Months")]
    public int? SecurityDepositPayableWithinMonths { get; init; } = 0;
    [Display(Name = "Total Construction Bond")]

    [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
    public decimal? TotalConstructionBond { get; set; } = 0;
    [Display(Name = "Construction Payable Within Months")]
    public int? ConstructionPayableWithinMonths { get; init; } = 0;
    [Display(Name = "Total Basic Fixed Monthly Rent")]

    [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
    public decimal? TotalBasicFixedMonthlyRent { get; set; } = 0;
    [Display(Name = "Total Minimum Monthly Rent")]

    [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
    public decimal? TotalMinimumMonthlyRent { get; init; } = 0;
    [Display(Name = "Total Lot Budget")]

    [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
    public decimal? TotalLotBudget { get; init; } = 0;
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
    public DateTime? SignedAwardNoticeDate { get; init; }
    [Display(Name = "Tag Signed Award Notice By")]

    public string? TagSignedAwardNoticeBy { get; init; }
    [Display(Name = "Minimum Sales")]

    [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
    public decimal? MinimumSalesQuota { get; init; } = 0;

    [Display(Name = "Provision")]
    public string? Provision { get; init; }
    [Range(2, int.MaxValue, ErrorMessage = "{0} shall be more than 1 day")]
    [Display(Name = "Fit-Out Period")]
    [Required]
    public int? FitOutPeriod { get; set; } = 0;
    [Display(Name = "Turn Over Date")]
    public DateTime? TurnOverDate { get; set; } = DateTime.Now.Date;
    [Display(Name = "Auto Compute Annual Advertising Fee")]
    public bool AutoComputeAnnualAdvertisingFee { get; init; }
    [Display(Name = "Booking Date")]
    public DateTime? BookingDate { get; init; }
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
    public DateTime? LeaseContractCreatedDate { get; init; }
    [Display(Name = "Lease Contract Created By")]
    [StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
    public string? LeaseContractCreatedBy { get; init; }
    [Display(Name = "Tag Signed Lease Contract By")]

    public string? TagSignedLeaseContractBy { get; init; }
    [Display(Name = "Signed Lease Contract Date")]
    public DateTime? SignedLeaseContractDate { get; init; }
    [Display(Name = "Tag For Review Lease Contract By")]

    public string? TagForReviewLeaseContractBy { get; init; }
    [Display(Name = "For Review Lease Contract Date")]
    public DateTime? ForReviewLeaseContractDate { get; init; }
    [Display(Name = "Tag For Final Print Lease Contract By")]

    public string? TagForFinalPrintLeaseContractBy { get; init; }
    [Display(Name = "For Final Print Lease Contract Date")]
    public DateTime? ForFinalPrintLeaseContractDate { get; init; }
    [Display(Name = "Lease Contract")]
    public string? LeaseContractStatus { get; init; }
    [Display(Name = "AN Term Type")]
    public string? ANTermType { get; init; }
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
    [Display(Name = "OS Reference No")]
    public string? OfferSheetNo { get; init; } = "";
    public decimal AreaDivider
    {
        get
        {
            return (this.CAMCConstructionTotalUnitArea == null || this.CAMCConstructionTotalUnitArea == 0 ? 1 : (decimal)this.CAMCConstructionTotalUnitArea);
        }
    }
    public string TotalRentalRateString
    {
        get
        {
            var FixedRent = (this.TotalBasicFixedMonthlyRent == null || this.TotalBasicFixedMonthlyRent == 0 ? 1 : (decimal)this.TotalBasicFixedMonthlyRent) / this.AreaDivider;
            var PercentRent = (this.TotalPercentageRent == null || this.TotalPercentageRent == 0 ? 1 : (decimal)this.TotalPercentageRent) / this.AreaDivider;
            var MinimumRent = (this.TotalMinimumMonthlyRent == null || this.TotalMinimumMonthlyRent == 0 ? 1 : (decimal)this.TotalMinimumMonthlyRent) / this.AreaDivider;
            return String.Format("{0:n0}", FixedRent) + "/" + (PercentRent).ToString("0.##") + "%/" + String.Format("{0:n0}", MinimumRent);
        }
    }
    public string TotalLotBudgetString
    {
        get
        {
            return String.Format("{0:n0}", (this.TotalLotBudget / this.AreaDivider));
        }
    }
}
