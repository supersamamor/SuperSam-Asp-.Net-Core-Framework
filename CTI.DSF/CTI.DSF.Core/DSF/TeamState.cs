using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.DSF.Core.DSF;

public record TeamState : BaseEntity
{
	public string? SectionCode { get; init; }
	public string TeamCode { get; init; } = "";
	public string TeamName { get; init; } = "";
	
	public SectionState? Section { get; init; }
	
	
}
