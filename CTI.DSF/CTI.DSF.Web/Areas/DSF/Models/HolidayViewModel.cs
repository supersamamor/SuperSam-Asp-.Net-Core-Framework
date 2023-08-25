using CTI.Common.Web.Utility.Extensions;
using CTI.DSF.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.DSF.Web.Areas.DSF.Models;

public record HolidayViewModel : BaseViewModel
{	
	[Display(Name = "Holiday Date")]
	public DateTime? HolidayDate { get; init; } = DateTime.Now.Date;
	[Display(Name = "Holiday Name")]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? HolidayName { get; init; }
	
	public DateTime LastModifiedDate { get; set; }
		
	
}
