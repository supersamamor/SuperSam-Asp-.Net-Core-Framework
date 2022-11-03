using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.ELMS.Core.ELMS;

public record OperationTypeState : BaseEntity
{
	public string OperationTypeName { get; init; } = "";
	
	
	public IList<LeadState>? LeadList { get; set; }
	
}
