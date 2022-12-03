using CTI.Common.Web.Utility.Extensions;
using CTI.FAS.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.FAS.Web.Areas.FAS.Models;

public record BatchViewModel : BaseViewModel
{	
	[Display(Name = "Date")]
	[Required]
	public DateTime Date { get; init; } = DateTime.Now.Date;
	[Display(Name = "Batch")]
	[Required]
	public int Batch { get; init; }
	
	public DateTime LastModifiedDate { get; set; }
		
	public IList<GeneratedViewModel>? GeneratedList { get; set; }
	
}
