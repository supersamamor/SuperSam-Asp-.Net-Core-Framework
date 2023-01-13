using CTI.Common.Web.Utility.Extensions;
using CTI.LocationApi.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.LocationApi.Web.Areas.LocationApi.Models;

public record LocationViewModel : BaseViewModel
{	
	[Display(Name = "Barangay Code")]
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string BarangayCode { get; init; } = "";
	[Display(Name = "Barangay")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Barangay { get; init; } = "";
	[Display(Name = "City Code")]
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string CityCode { get; init; } = "";
	[Display(Name = "City")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string City { get; init; } = "";
	[Display(Name = "Province Code")]
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string ProvinceCode { get; init; } = "";
	[Display(Name = "Province")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Province { get; init; } = "";
	[Display(Name = "Region Code")]
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string RegionCode { get; init; } = "";
	[Display(Name = "Region")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Region { get; init; } = "";
	[Display(Name = "Full")]
	[Required]
	
	public string Full { get; init; } = "";
	
	public DateTime LastModifiedDate { get; set; }
		
	
}
