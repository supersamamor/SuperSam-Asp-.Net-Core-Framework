using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.LocationApi.Core.LocationApi;

public record CityState : BaseEntity
{
	public string ProvinceId { get; init; } = "";
	public string Code { get; init; } = "";
	public string Name { get; init; } = "";
	
	public ProvinceState? Province { get; init; }
	
	public IList<BarangayState>? BarangayList { get; set; }
	
}
