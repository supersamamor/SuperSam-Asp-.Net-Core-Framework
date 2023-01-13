using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.LocationApi.Core.LocationApi;

public record LocationState : BaseEntity
{
	public string BarangayCode { get; init; } = "";
	public string Barangay { get; init; } = "";
	public string CityCode { get; init; } = "";
	public string City { get; init; } = "";
	public string ProvinceCode { get; init; } = "";
	public string Province { get; init; } = "";
	public string RegionCode { get; init; } = "";
	public string Region { get; init; } = "";
	public string Full { get; init; } = "";
	
	
	
}
