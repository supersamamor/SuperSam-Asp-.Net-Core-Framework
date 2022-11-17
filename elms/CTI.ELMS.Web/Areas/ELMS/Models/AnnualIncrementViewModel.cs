using CTI.Common.Web.Utility.Extensions;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.ELMS.Web.Areas.ELMS.Models;

public record AnnualIncrementViewModel : BaseViewModel
{	
	[Display(Name = "Unit Offered")]
	
	public string? UnitOfferedID { get; init; }
	public string?  ForeignKeyUnitOffered { get; set; }
	[Display(Name = "Year")]
	public int? Year { get; init; }
	[Display(Name = "Basic Fixed Monthly Rent")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? BasicFixedMonthlyRent { get; set; }
	[Display(Name = "Percentage Rent")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? PercentageRent { get; set; }
	[Display(Name = "Minimum Monthly Rent")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? MinimumMonthlyRent { get; set; }
	
	public DateTime LastModifiedDate { get; set; }
	public UnitOfferedViewModel? UnitOffered { get; init; }
		
	
}
