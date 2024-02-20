using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.DSF.Core.DSF;

public record TaskTagState : BaseEntity
{
	public string TaskMasterId { get; init; } = "";
	public string TagId { get; init; } = "";
	
	public TaskMasterState? TaskMaster { get; init; }
	public TagsState? Tags { get; init; }
	
	
}
