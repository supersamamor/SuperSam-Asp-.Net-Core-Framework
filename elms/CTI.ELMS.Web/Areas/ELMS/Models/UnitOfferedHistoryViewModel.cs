using CTI.Common.Web.Utility.Extensions;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.ELMS.Web.Areas.ELMS.Models;

public record UnitOfferedHistoryViewModel : BaseViewModel
{	
	[Display(Name = "Offering")]
	
	public string? OfferingID { get; init; }
	public string?  ForeignKeyOffering { get; set; }
	[Display(Name = "Unit")]
	
	public string? UnitID { get; init; }
	public string?  ForeignKeyUnit { get; set; }
	[Display(Name = "Lot Budget")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? LotBudget { get; init; }
	[Display(Name = "Lot Area")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? LotArea { get; init; }
	[Display(Name = "Offering History")]
	
	public string? OfferingHistoryID { get; init; }
	public string?  ForeignKeyOfferingHistory { get; set; }
	[Display(Name = "Basic Fixed")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? BasicFixedMonthlyRent { get; init; }
	[Display(Name = "Percentage Rent")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? PercentageRent { get; init; }
	[Display(Name = "Minimum Monthly")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? MinimumMonthlyRent { get; init; }
	[Display(Name = "Annual Increment")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? AnnualIncrement { get; init; }
	[Display(Name = "Annual Increment")]
	[Required]
	
	public string AnnualIncrementInformation { get; init; } = "";
	[Display(Name = "Is Fixed")]
	public bool IsFixedMonthlyRent { get; init; }
	
	public DateTime LastModifiedDate { get; set; }
	public OfferingViewModel? Offering { get; init; }
	public UnitViewModel? Unit { get; init; }
	public OfferingHistoryViewModel? OfferingHistory { get; init; }
		
	public IList<AnnualIncrementHistoryViewModel>? AnnualIncrementHistoryList { get; set; }
	
}
