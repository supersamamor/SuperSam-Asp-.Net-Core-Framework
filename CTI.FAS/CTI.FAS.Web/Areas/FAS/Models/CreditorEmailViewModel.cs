using CTI.Common.Web.Utility.Extensions;
using CTI.FAS.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.FAS.Web.Areas.FAS.Models;

public record CreditorEmailViewModel : BaseViewModel
{	
	[Display(Name = "Email")]
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Email { get; init; } = "";
	[Display(Name = "Creditor")]
	[Required]
	
	public string CreditorId { get; init; } = "";
	public string?  ForeignKeyCreditor { get; set; }
	
	public DateTime LastModifiedDate { get; set; }
	public CreditorViewModel? Creditor { get; init; }
		
	
}
