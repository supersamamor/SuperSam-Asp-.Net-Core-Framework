using CTI.Common.Web.Utility.Extensions;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.ELMS.Web.Areas.ELMS.Models;

public record OfferingHistoryViewModel : BaseViewModel
{	
	[Display(Name = "Offering")]
	
	public string? OfferingID { get; init; }
	public string?  ForeignKeyOffering { get; set; }
	[Display(Name = "Status")]
	[StringLength(35, ErrorMessage = "{0} length can't be more than {1}.")]
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
	[Display(Name = "Costruction CAMC")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? ConstructionCAMC { get; init; }
	[Display(Name = "Commencement CAMC")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? CommencementCAMC { get; init; }
	[Display(Name = "Concession")]
	
	public string? Concession { get; init; }
	[Display(Name = "Offersheet Remarks")]
	
	public string? OffersheetRemarks { get; init; }
	[Display(Name = "Units Information")]
	[Required]
	
	public string UnitsInformation { get; init; } = "";
	[Display(Name = "AN Type")]
	[StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? ANType { get; init; }
	[Display(Name = "Lead")]
	
	public string? LeadID { get; init; }
	public string?  ForeignKeyLead { get; set; }
	[Display(Name = "Project")]
	
	public string? ProjectID { get; init; }
	public string?  ForeignKeyProject { get; set; }
	[Display(Name = "Total Unit Area")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? TotalUnitArea { get; init; }
	[Display(Name = "Total Security Deposit")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? TotalSecurityDeposit { get; init; }
	[Display(Name = "Annual Advertising Fee")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? AnnualAdvertisingFee { get; init; }
	[Display(Name = "CAMC Construction Total")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? CAMCConstructionTotalUnitArea { get; init; }
	[Display(Name = "Construction CAMC Months")]
	public int? ConstructionCAMCMonths { get; init; }
	[Display(Name = "Construction CAMC Rate")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? ConstructionCAMCRate { get; init; }
	[Display(Name = "Has Board Fee")]
	public bool HasBoardUpFee { get; init; }
	[Display(Name = "Security Deposit Payable Within Months")]
	public int? SecurityDepositPayableWithinMonths { get; init; }
	[Display(Name = "Total Construction Bond")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? TotalConstructionBond { get; init; }
	[Display(Name = "Construction Payable Within Months")]
	public int? ConstructionPayableWithinMonths { get; init; }
	[Display(Name = "Is POS")]
	public bool IsPOS { get; init; }
	[Display(Name = "Location")]
	[Required]
	
	public string Location { get; init; } = "";
	[Display(Name = "Offering Version")]
	public int? OfferingVersion { get; init; }
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
	[Display(Name = "Minimum Sales Quota")]
	
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
	[Required]
	public DateTime TurnOverDate { get; init; } = DateTime.Now.Date;
	[Display(Name = "Auto Compute Annual Advertising Fee")]
	public bool AutoComputeAnnualAdvertisingFee { get; init; }
	[Display(Name = "Booking Date")]
	[Required]
	public DateTime BookingDate { get; init; } = DateTime.Now.Date;
	
	public DateTime LastModifiedDate { get; set; }
	public OfferingViewModel? Offering { get; init; }
	public LeadViewModel? Lead { get; init; }
	public ProjectViewModel? Project { get; init; }
		
	public IList<UnitOfferedHistoryViewModel>? UnitOfferedHistoryList { get; set; }
	public IList<UnitGroupViewModel>? UnitGroupList { get; set; }
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
