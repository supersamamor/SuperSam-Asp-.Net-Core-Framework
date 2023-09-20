using CompanyNamePlaceHolder.Common.Core.Base.Models;
using System.ComponentModel;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;

public record AssignmentState : BaseEntity
{
	public string AssignmentCode { get; init; } = "";
	public string TaskListCode { get; init; } = "";
	public string PrimaryAsignee { get; init; } = "";
	public string? AlternateAsignee { get; init; }
	public DateTime StartDate { get; init; }
	public DateTime EndDate { get; init; }
	
	public TaskListState? TaskList { get; init; }
	
	
}
