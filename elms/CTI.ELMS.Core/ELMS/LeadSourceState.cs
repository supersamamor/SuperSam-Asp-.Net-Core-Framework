using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.ELMS.Core.ELMS;

public record LeadSourceState : BaseEntity
{
	public string LeadSourceName { get; init; } = "";
	
	
	public IList<LeadState>? LeadList { get; set; }
	
}
