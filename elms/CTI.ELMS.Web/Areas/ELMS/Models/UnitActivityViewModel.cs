using CTI.Common.Web.Utility.Extensions;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.ELMS.Web.Areas.ELMS.Models;

public record UnitActivityViewModel : BaseViewModel
{	
	[Display(Name = "Unit")]
	[Required]
	
	public string UnitID { get; init; } = "";
	public string?  ForeignKeyUnit { get; set; }
	[Display(Name = "Activity History")]
	
	public string? ActivityHistoryID { get; init; }
	public string?  ForeignKeyActivityHistory { get; set; }
	[Display(Name = "Activity")]
	[Required]
	
	public string ActivityID { get; init; } = "";
	public string?  ForeignKeyActivity { get; set; }
	
	public DateTime LastModifiedDate { get; set; }
	public UnitViewModel? Unit { get; init; }
	public ActivityHistoryViewModel? ActivityHistory { get; init; }
	public ActivityViewModel? Activity { get; init; }
		
	
}
