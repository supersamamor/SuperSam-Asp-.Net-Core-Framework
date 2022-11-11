using CTI.ELMS.Web.Models;
using System.ComponentModel.DataAnnotations;

namespace CTI.ELMS.Web.Areas.ELMS.Models;

public record ActivityViewModel : BaseViewModel
{	
	[Display(Name = "Lead")]	
	public string? LeadID { get; init; }
	public string?  ForeignKeyLead { get; set; }
	[Display(Name = "Project")]
	[Required]
	public string? ProjectID { get; init; }
	public string?  ForeignKeyProject { get; set; }
	[Display(Name = "Lead Task")]
	[Required]
	public string? LeadTaskId { get; init; }
	public string?  ForeignKeyLeadTask { get; set; }
	[Display(Name = "Activity Date")]
	[Required]
	public DateTime? ActivityDate { get; init; } = DateTime.Now.Date;
	[Display(Name = "Client Feedback")]
	[Required]
	public string? ClientFeedbackId { get; init; }
	public string?  ForeignKeyClientFeedback { get; set; }
	[Display(Name = "Next Step")]
	[Required]
	public string? NextStepId { get; init; }
	public string?  ForeignKeyNextStep { get; set; }
	[Display(Name = "Target Date")]
	public DateTime? TargetDate { get; init; }
	[Display(Name = "Activity Remarks")]
	[StringLength(500, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? ActivityRemarks { get; init; }
	public DateTime LastModifiedDate { get; set; }
	public LeadViewModel? Lead { get; init; }
	public ProjectViewModel? Project { get; init; }
	public LeadTaskViewModel? LeadTask { get; init; }
	public ClientFeedbackViewModel? ClientFeedback { get; init; }
	public NextStepViewModel? NextStep { get; init; }		
	public IList<ActivityHistoryViewModel>? ActivityHistoryList { get; set; }
	public IList<UnitActivityViewModel>? UnitActivityList { get; set; }
	
}
