using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.ELMS.Core.ELMS;

public record UserProjectAssignmentState : BaseEntity
{
	public string? UserId { get; init; }
	public string? ProjectID { get; init; }
	
	public ProjectState? Project { get; init; }
	
	
}
