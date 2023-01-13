using CTI.Common.Web.Utility.Extensions;
using CTI.LocationApi.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.LocationApi.Web.Areas.LocationApi.Models;

public record CityViewModel : BaseViewModel
{	
	[Display(Name = "Province")]
	[Required]
	
	public string ProvinceId { get; init; } = "";
	public string?  ForeignKeyProvince { get; set; }
	[Display(Name = "Code")]
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Code { get; init; } = "";
	[Display(Name = "Name")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Name { get; init; } = "";
	
	public DateTime LastModifiedDate { get; set; }
	public ProvinceViewModel? Province { get; init; }
		
	public IList<BarangayViewModel>? BarangayList { get; set; }
	
}
