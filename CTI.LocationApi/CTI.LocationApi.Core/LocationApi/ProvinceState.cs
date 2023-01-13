using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.LocationApi.Core.LocationApi;

public record ProvinceState : BaseEntity
{
	public string RegionId { get; init; } = "";
	public string Code { get; init; } = "";
	public string Name { get; init; } = "";
	
	public RegionState? Region { get; init; }
	
	public IList<CityState>? CityList { get; set; }
	
}
