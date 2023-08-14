using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.DSF.Core.DSF;

public record AssignmentState : BaseEntity
{
	public string? AssignmentCode { get; set; }
    public string? TaskListCode { get; init; }
    public string PrimaryAsignee { get; init; } = "";
	public string? AlternateAsignee { get; init; }
	public DateTime StartDate { get; init; }
	public DateTime EndDate { get; init; }

	public string? TaskListId { get; init; }
	public TaskListState? TaskList { get; init; }
	public IList<DeliveryState>? DeliveryList { get; set; }
	
	public void GenerateAssignmentCode()
	{
		AssignmentCode = Guid.NewGuid().ToString();
	}
}
