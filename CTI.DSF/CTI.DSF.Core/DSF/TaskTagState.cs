using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.DSF.Core.DSF;

public record TaskTagState : BaseEntity
{
	public string TaskListId { get; init; } = "";
	public string TagId { get; init; } = "";
	
	public TaskListState? TaskList { get; init; }
	public TagsState? Tags { get; init; }
	
	
}
