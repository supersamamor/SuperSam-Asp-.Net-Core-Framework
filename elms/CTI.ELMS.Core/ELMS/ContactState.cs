using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.ELMS.Core.ELMS;

public record ContactState : BaseEntity
{
	public string? LeadID { get; init; }
	public int? ContactType { get; init; }
	public string ContactDetails { get; init; } = "";
	
	public LeadState? Lead { get; init; }
	
	
}
