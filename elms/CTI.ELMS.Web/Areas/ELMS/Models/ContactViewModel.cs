using CTI.Common.Web.Utility.Extensions;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.ELMS.Web.Areas.ELMS.Models;

public record ContactViewModel : BaseViewModel
{	
	[Display(Name = "Lead")]
	
	public string? LeadID { get; init; }
	public string?  ForeignKeyLead { get; set; }
	[Display(Name = "Contact Type")]
	public int? ContactType { get; init; }
	[Display(Name = "Contact Details")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string ContactDetails { get; init; } = "";
	
	public DateTime LastModifiedDate { get; set; }
	public LeadViewModel? Lead { get; init; }
		
	
}
