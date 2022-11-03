using CTI.Common.Web.Utility.Extensions;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.ELMS.Web.Areas.ELMS.Models;

public record AnnualIncrementHistoryViewModel : BaseViewModel
{	
	[Display(Name = "Unit Offered")]
	
	public string? UnitOfferedHistoryID { get; init; }
	public string?  ForeignKeyUnitOfferedHistory { get; set; }
	[Display(Name = "Year")]
	public int? Year { get; init; }
	[Display(Name = "Basic Fixed Rent")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? BasicFixedMonthlyRent { get; init; }
	[Display(Name = "Percentage Rent")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? PercentageRent { get; init; }
	[Display(Name = "Minimum Monthly Rent")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? MinimumMonthlyRent { get; init; }
	[Display(Name = "Contract Grouping Count")]
	public int? ContractGroupingCount { get; init; }
	
	public DateTime LastModifiedDate { get; set; }
	public UnitOfferedHistoryViewModel? UnitOfferedHistory { get; init; }
		
	
}
