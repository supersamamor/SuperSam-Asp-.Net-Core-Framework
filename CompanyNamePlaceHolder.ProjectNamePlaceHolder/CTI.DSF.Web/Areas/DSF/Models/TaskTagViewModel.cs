using CTI.Common.Web.Utility.Extensions;
using CTI.DSF.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.DSF.Web.Areas.DSF.Models;

public record TaskTagViewModel : BaseViewModel
{	
	[Display(Name = "Task Master")]
	[Required]
	
	public string TaskMasterId { get; init; } = "";
	public string?  ReferenceFieldTaskMasterId { get; set; }
	[Display(Name = "Tag")]
	[Required]
	
	public string TagId { get; init; } = "";
	public string?  ReferenceFieldTagId { get; set; }
	
	public DateTime LastModifiedDate { get; set; }
	public TaskMasterViewModel? TaskMaster { get; init; }
	public TagsViewModel? Tags { get; init; }
		
	
}
