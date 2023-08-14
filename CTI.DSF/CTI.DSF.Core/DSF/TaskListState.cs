using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.DSF.Core.DSF;

public record TaskListState : BaseEntity
{
	public string? TaskListCode { get; set; }
	public string TaskDescription { get; init; } = "";
    public string? SubTask { get; init; } = "";
    public string TaskClassification { get; init; } = "";
	public string TaskFrequency { get; init; } = "";
	public int TaskDueDay { get; init; }
	public DateTime TargetDueDate { get; init; }
	public string? Company { get; init; }
	public string? Department { get; init; }
	public string? Section { get; init; }
	public string? Team { get; init; }
	public string? PrimaryEndorser { get; init; }
	public string PrimaryApprover { get; init; } = "";
	public string? AlternateEndorser { get; init; }
	public string AlternateApprover { get; init; } = "";
    public bool? IsMilestone { get; init; }
    public string? ParentTaskId { get; init; }
    public IList<AssignmentState>? AssignmentList { get; set; }
    public TaskListState? ParentTask { get; set; }
    public IList<TaskListState>? ChildTaskList { get; set; }
    public void GenerateTaskListCode()
	{
		TaskListCode = Guid.NewGuid().ToString();
	}
	
}
