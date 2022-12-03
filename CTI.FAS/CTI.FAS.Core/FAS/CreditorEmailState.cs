using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.FAS.Core.FAS;

public record CreditorEmailState : BaseEntity
{
	public string Email { get; init; } = "";
	public string CreditorId { get; init; } = "";
	
	public CreditorState? Creditor { get; init; }
	
	
}
