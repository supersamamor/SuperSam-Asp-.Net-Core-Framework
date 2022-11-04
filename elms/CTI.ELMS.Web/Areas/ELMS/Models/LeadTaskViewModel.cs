using CTI.Common.Web.Utility.Extensions;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.ELMS.Web.Areas.ELMS.Models;

public record LeadTaskViewModel : BaseViewModel
{	
	[Display(Name = "Name")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string LeadTaskName { get; init; } = "";
	
	public DateTime LastModifiedDate { get; set; }
		
	public IList<LeadTaskClientFeedBackViewModel>? LeadTaskClientFeedBackList { get; set; }
	public IList<LeadTaskNextStepViewModel>? LeadTaskNextStepList { get; set; }
	public IList<ActivityViewModel>? ActivityList { get; set; }
	public IList<ActivityHistoryViewModel>? ActivityHistoryList { get; set; }
	
}
