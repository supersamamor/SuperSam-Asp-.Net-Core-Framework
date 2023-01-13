using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.LocationApi.Core.LocationApi;

public record CountryState : BaseEntity
{
	public string Name { get; init; } = "";
	public string Code { get; init; } = "";
	
	
	public IList<RegionState>? RegionList { get; set; }
	
}
