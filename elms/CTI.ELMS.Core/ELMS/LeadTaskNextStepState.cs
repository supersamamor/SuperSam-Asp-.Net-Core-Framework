using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.ELMS.Core.ELMS;

public record LeadTaskNextStepState : BaseEntity
{
	public string? LeadTaskId { get; init; }
	public string? ClientFeedbackId { get; init; }
	public string? NextStepId { get; init; }
	public int? PCTDay { get; init; }
	
	public LeadTaskState? LeadTask { get; init; }
	public ClientFeedbackState? ClientFeedback { get; init; }
	public NextStepState? NextStep { get; init; }
	
	
}
