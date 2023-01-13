using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.LocationApi.Core.LocationApi;

public record RegionState : BaseEntity
{
	public string CountryId { get; init; } = "";
	public string Code { get; init; } = "";
	public string Name { get; init; } = "";
	
	public CountryState? Country { get; init; }
	
	public IList<ProvinceState>? ProvinceList { get; set; }
	
}
