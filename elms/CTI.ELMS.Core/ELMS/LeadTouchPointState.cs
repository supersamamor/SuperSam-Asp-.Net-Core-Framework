using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.ELMS.Core.ELMS;

public record LeadTouchPointState : BaseEntity
{
	public string LeadTouchPointName { get; init; } = "";
	
	
	public IList<LeadState>? LeadList { get; set; }
	
}
