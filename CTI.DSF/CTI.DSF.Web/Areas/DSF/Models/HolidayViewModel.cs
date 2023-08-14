using CTI.Common.Web.Utility.Extensions;
using CTI.DSF.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.DSF.Web.Areas.DSF.Models;

public record HolidayViewModel : BaseViewModel
{	
	[Display(Name = "Holiday")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string HolidayName { get; init; } = "";
	[Display(Name = "Date")]
	[Required]
	public DateTime HolidayDate { get; init; } = DateTime.Now.Date;
	
	public DateTime LastModifiedDate { get; set; }
		
	
}
