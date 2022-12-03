using CTI.Common.Web.Utility.Extensions;
using CTI.FAS.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.FAS.Web.Areas.FAS.Models;

public record CheckReleaseOptionViewModel : BaseViewModel
{	
	[Display(Name = "Creditor")]
	[Required]
	
	public string CreditorId { get; init; } = "";
	public string?  ForeignKeyCreditor { get; set; }
	[Display(Name = "Name")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Name { get; init; } = "";
	
	public DateTime LastModifiedDate { get; set; }
	public CreditorViewModel? Creditor { get; init; }
		
	
}
