using CTI.Common.Web.Utility.Extensions;
using CTI.DSF.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.DSF.Web.Areas.DSF.Models;

public record TaskTagViewModel : BaseViewModel
{	
	[Display(Name = "Task")]
	[Required]
	
	public string TaskListId { get; init; } = "";
	public string?  ReferenceFieldTaskListId { get; set; }
	[Display(Name = "Tag")]
	[Required]
	
	public string TagId { get; init; } = "";
	public string?  ReferenceFieldTagId { get; set; }
	
	public DateTime LastModifiedDate { get; set; }
	public TaskListViewModel? TaskList { get; init; }
	public TagsViewModel? Tags { get; init; }
		
	
}
