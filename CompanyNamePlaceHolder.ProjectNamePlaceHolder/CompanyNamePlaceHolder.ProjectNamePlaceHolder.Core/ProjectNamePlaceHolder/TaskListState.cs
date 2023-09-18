using CompanyNamePlaceHolder.Common.Core.Base.Models;
using System.ComponentModel;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;

public record TaskListState : BaseEntity
{
	public string TaskListCode { get; init; } = "";
	public string TaskDescription { get; init; } = "";
	public string TaskType { get; init; } = "";
	public string TaskFrequency { get; init; } = "";
	public int TaskDueDay { get; init; }
	public DateTime TargetDueDate { get; init; }
	public string? PrimaryEndorser { get; init; }
	public string PrimaryApprover { get; init; } = "";
	public string? AlternateEndorser { get; init; }
	public string AlternateApprover { get; init; } = "";
	
	
	public IList<AssignmentState>? AssignmentList { get; set; }
	
}
