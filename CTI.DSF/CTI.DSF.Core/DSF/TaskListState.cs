using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.DSF.Core.DSF;

public record TaskListState : BaseEntity
{
	public string? TaskListCode { get; set; }
	public string TaskDescription { get; init; } = "";
    public string? SubTask { get; init; } = "";
    public string TaskClassification { get; private set; } = "";
	public string TaskFrequency { get; private set; } = "";
	public int TaskDueDay { get; init; }
	public DateTime TargetDueDate { get; init; }
	public string? Company { get; private set; }
	public string? Department { get; private set; }
	public string? Section { get; init; }
	public string? Team { get; init; }
	public string? PrimaryEndorser { get; init; }
	public string PrimaryApprover { get; private set; } = "";
	public string? AlternateEndorser { get; init; }
	public string AlternateApprover { get; private set; } = "";
    public bool? IsMilestone { get; init; }
    public string? ParentTaskId { get; init; }
    public IList<AssignmentState>? AssignmentList { get; set; }
    public TaskListState? ParentTask { get; set; }
    public IList<TaskListState>? ChildTaskList { get; set; }
    public void GenerateTaskListCode()
	{
		TaskListCode = Guid.NewGuid().ToString();
	}
	public void SetInformationFromParent(TaskListState parentTask)
	{
        Company = parentTask.Company;
        Department = parentTask.Department;
        TaskFrequency = parentTask.TaskFrequency;
        PrimaryApprover = parentTask.PrimaryApprover;
        AlternateApprover = parentTask.AlternateApprover;
        TaskClassification = parentTask.TaskClassification;
        Entity = parentTask.Entity;
    }
}
