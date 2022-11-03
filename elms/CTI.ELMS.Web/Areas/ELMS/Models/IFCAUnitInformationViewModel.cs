using CTI.Common.Web.Utility.Extensions;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.ELMS.Web.Areas.ELMS.Models;

public record IFCAUnitInformationViewModel : BaseViewModel
{	
	[Display(Name = "Unit")]
	
	public string? UnitID { get; init; }
	public string?  ForeignKeyUnit { get; set; }
	[Display(Name = "IFCA Tenant Information")]
	
	public string? IFCATenantInformationID { get; init; }
	public string?  ForeignKeyIFCATenantInformation { get; set; }
	[Display(Name = "Rental Rate")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? RentalRate { get; init; }
	[Display(Name = "Budgeted Amount")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? BudgetedAmount { get; init; }
	[Display(Name = "Start Date")]
	public DateTime? StartDate { get; init; } = DateTime.Now.Date;
	[Display(Name = "End Date")]
	public DateTime? EndDate { get; init; } = DateTime.Now.Date;
	[Display(Name = "Basic Fixed Monthly Rent")]
	
	[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
	public decimal? BasicFixedMonthlyRent { get; init; }
	
	public DateTime LastModifiedDate { get; set; }
	public UnitViewModel? Unit { get; init; }
	public IFCATenantInformationViewModel? IFCATenantInformation { get; init; }
		
	
}
