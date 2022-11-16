using CTI.Common.Web.Utility.Extensions;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.ELMS.Web.Areas.ELMS.Models;

public record PreSelectedUnitViewModel : BaseViewModel
{	
	[Display(Name = "Offering")]
	
	public string? OfferingID { get; set; }
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
	
	public DateTime LastModifiedDate { get; set; }
	public OfferingViewModel? Offering { get; init; }
	public UnitViewModel? Unit { get; init; }
		
	
}
