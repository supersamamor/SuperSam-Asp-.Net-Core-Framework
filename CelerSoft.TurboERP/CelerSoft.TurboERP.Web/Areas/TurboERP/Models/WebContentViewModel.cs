using CelerSoft.Common.Web.Utility.Extensions;
using CelerSoft.TurboERP.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CelerSoft.TurboERP.Web.Areas.TurboERP.Models;

public record WebContentViewModel : BaseViewModel
{	
	[Display(Name = "Code")]
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Code { get; init; }
	[Display(Name = "Content")]
	[Required]
	
	public string Content { get; init; } = "";
	[Display(Name = "Page Name")]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? PageName { get; init; }
	[Display(Name = "Page Title")]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? PageTitle { get; init; }
	
	public DateTime LastModifiedDate { get; set; }
		
	
}
