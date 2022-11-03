using CTI.Common.Web.Utility.Extensions;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.ELMS.Web.Areas.ELMS.Models;

public record UnitGroupViewModel : BaseViewModel
{	
	[Display(Name = "Units Information")]
	[Required]
	
	public string UnitsInformation { get; init; } = "";
	[Display(Name = "Lot Area")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? LotArea { get; init; }
	[Display(Name = "Basic Fixed")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? BasicFixedMonthlyRent { get; init; }
	[Display(Name = "Offering History")]
	
	public string? OfferingHistoryID { get; init; }
	public string?  ForeignKeyOfferingHistory { get; set; }
	[Display(Name = "Unit Offered")]
	public int? UnitOfferedHistoryID { get; init; }
	[Display(Name = "Is Fixed")]
	public bool IsFixedMonthlyRent { get; init; }
	[Display(Name = "Area Type")]
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string AreaTypeDescription { get; init; } = "";
	
	public DateTime LastModifiedDate { get; set; }
	public OfferingHistoryViewModel? OfferingHistory { get; init; }
		
	
}
