using CTI.Common.Web.Utility.Extensions;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.ELMS.Web.Areas.ELMS.Models;

public record ClientFeedbackViewModel : BaseViewModel
{	
	[Display(Name = "Client Feedback Name")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string ClientFeedbackName { get; init; } = "";
	
	public DateTime LastModifiedDate { get; set; }
		
	public IList<LeadTaskClientFeedBackViewModel>? LeadTaskClientFeedBackList { get; set; }
	public IList<LeadTaskNextStepViewModel>? LeadTaskNextStepList { get; set; }
	public IList<ActivityViewModel>? ActivityList { get; set; }
	public IList<ActivityViewModel>? ActivityHistoryList { get; set; }
	
}
