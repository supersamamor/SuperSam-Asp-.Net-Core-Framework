using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.LocationApi.Core.LocationApi;

public record CountryState : BaseEntity
{
	public string Name { get; init; } = "";
	public string Code { get; init; } = "";
	public string Citizenship { get; set; } = "";
	public string AreaCode { get; set; } = "";

	public IList<RegionState>? RegionList { get; set; }
	
}
