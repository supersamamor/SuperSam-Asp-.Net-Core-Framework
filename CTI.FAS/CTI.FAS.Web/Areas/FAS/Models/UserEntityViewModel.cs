using CTI.Common.Web.Utility.Extensions;
using CTI.FAS.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.FAS.Web.Areas.FAS.Models;

public record UserEntityViewModel : BaseViewModel
{	
	[Display(Name = "Pplus User Id")]
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string PplusUserId { get; init; } = "";
	[Display(Name = "Entity")]
	[Required]
	
	public string CompanyId { get; init; } = "";
	public string?  ForeignKeyCompany { get; set; }
	
	public DateTime LastModifiedDate { get; set; }
	public CompanyViewModel? Company { get; init; }
		
	
}
