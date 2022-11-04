using CTI.Common.Web.Utility.Extensions;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.ELMS.Web.Areas.ELMS.Models;

public record LeadTaskNextStepViewModel : BaseViewModel
{	
	[Display(Name = "Lead Task")]
	
	public string? LeadTaskId { get; init; }
	public string?  ForeignKeyLeadTask { get; set; }
	[Display(Name = "Client Feedback")]
	
	public string? ClientFeedbackId { get; init; }
	public string?  ForeignKeyClientFeedback { get; set; }
	[Display(Name = "Next Step")]
	
	public string? NextStepId { get; init; }
	public string?  ForeignKeyNextStep { get; set; }
	[Display(Name = "PCT Day")]
	public int? PCTDay { get; init; }
	
	public DateTime LastModifiedDate { get; set; }
	public LeadTaskViewModel? LeadTask { get; init; }
	public ClientFeedbackViewModel? ClientFeedback { get; init; }
	public NextStepViewModel? NextStep { get; init; }
		
	
}
