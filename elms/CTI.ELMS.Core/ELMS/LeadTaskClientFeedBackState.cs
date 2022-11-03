using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.ELMS.Core.ELMS;

public record LeadTaskClientFeedBackState : BaseEntity
{
	public string? LeadTaskId { get; init; }
	public string? ClientFeedbackId { get; init; }
	public string? ActivityStatus { get; init; }
	
	public LeadTaskState? LeadTask { get; init; }
	public ClientFeedbackState? ClientFeedback { get; init; }
	
	
}
