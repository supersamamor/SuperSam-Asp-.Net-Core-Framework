using CTI.Common.Web.Utility.Extensions;
using CTI.LocationApi.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.LocationApi.Web.Areas.LocationApi.Models;

public record BarangayViewModel : BaseViewModel
{	
	[Display(Name = "City")]
	[Required]
	
	public string CityId { get; init; } = "";
	public string?  ForeignKeyCity { get; set; }
	[Display(Name = "Code")]
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Code { get; init; } = "";
	[Display(Name = "Name")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Name { get; init; } = "";
	
	public DateTime LastModifiedDate { get; set; }
	public CityViewModel? City { get; init; }
		
	
}
