using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.DSF.Core.DSF;

public record TaskMasterState : BaseEntity
{
	public int TaskNo { get; init; }
	public string TaskDescription { get; init; } = "";
	public string TaskClassification { get; init; } = "";
	public string TaskFrequency { get; init; } = "";
	public int? TaskDueDay { get; init; }
	public DateTime? TargetDueDate { get; init; }
	public string HolidayTag { get; init; } = "";
	public bool Active { get; init; }
	
	
	public IList<TaskCompanyAssignmentState>? TaskCompanyAssignmentList { get; set; }
	public IList<TaskTagState>? TaskTagList { get; set; }
	
}
