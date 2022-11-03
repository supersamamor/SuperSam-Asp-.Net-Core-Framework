using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.ELMS.Core.ELMS;

public record UnitActivityState : BaseEntity
{
	public string UnitID { get; init; } = "";
	public string? ActivityHistoryID { get; init; }
	public string ActivityID { get; init; } = "";
	
	public UnitState? Unit { get; init; }
	public ActivityHistoryState? ActivityHistory { get; init; }
	public ActivityState? Activity { get; init; }
	
	
}
