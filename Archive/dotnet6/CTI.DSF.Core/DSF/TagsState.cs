using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.DSF.Core.DSF;

public record TagsState : BaseEntity
{
	public string Name { get; init; } = "";
	
	
	public IList<TaskTagState>? TaskTagList { get; set; }
	
}
