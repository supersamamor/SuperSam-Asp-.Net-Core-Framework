using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.FAS.Core.FAS;

public record CheckReleaseOptionState : BaseEntity
{
	public string CreditorId { get; init; } = "";
	public string Name { get; init; } = "";
	
	public CreditorState? Creditor { get; init; }
	
	
}
