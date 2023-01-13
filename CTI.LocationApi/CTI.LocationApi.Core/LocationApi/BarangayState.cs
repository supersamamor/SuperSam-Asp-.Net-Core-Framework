using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.LocationApi.Core.LocationApi;

public record BarangayState : BaseEntity
{
	public string CityId { get; init; } = "";
	public string Code { get; init; } = "";
	public string Name { get; init; } = "";
	
	public CityState? City { get; init; }
	
	
}
