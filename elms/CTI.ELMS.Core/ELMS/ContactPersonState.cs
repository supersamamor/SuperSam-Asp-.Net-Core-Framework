using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.ELMS.Core.ELMS;

public record ContactPersonState : BaseEntity
{
	public string LeadId { get; init; } = "";
	public string? SalutationID { get; init; }
	public string FirstName { get; init; } = "";
	public string? MiddleName { get; init; }
	public string LastName { get; init; } = "";
	public string Position { get; init; } = "";
	public bool IsSOARecipient { get; init; }
	public bool IsANSignatory { get; init; }
	public bool IsCOLSignatory { get; init; }
	
	public LeadState? Lead { get; init; }
	public SalutationState? Salutation { get; init; }
	
	
}
