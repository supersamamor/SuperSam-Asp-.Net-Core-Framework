using CTI.Common.Web.Utility.Extensions;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.ELMS.Web.Areas.ELMS.Models;

public record LeadTaskClientFeedBackViewModel : BaseViewModel
{	
	[Display(Name = "Lead Task")]
	
	public string? LeadTaskId { get; init; }
	public string?  ForeignKeyLeadTask { get; set; }
	[Display(Name = "Client Feedback")]
	
	public string? ClientFeedbackId { get; init; }
	public string?  ForeignKeyClientFeedback { get; set; }
	[Display(Name = "Activity Status")]
	[StringLength(25, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? ActivityStatus { get; init; }
	
	public DateTime LastModifiedDate { get; set; }
	public LeadTaskViewModel? LeadTask { get; init; }
	public ClientFeedbackViewModel? ClientFeedback { get; init; }
		
	
}
