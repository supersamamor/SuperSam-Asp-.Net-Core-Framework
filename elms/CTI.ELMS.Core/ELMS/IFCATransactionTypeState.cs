using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.ELMS.Core.ELMS;

public record IFCATransactionTypeState : BaseEntity
{
	public string TransCode { get; init; } = "";
	public string TransGroup { get; init; } = "";
	public string Description { get; init; } = "";
	public string? EntityID { get; init; }
	public string Mode { get; init; } = "";
	
	public EntityGroupState? EntityGroup { get; init; }
	
	
}
