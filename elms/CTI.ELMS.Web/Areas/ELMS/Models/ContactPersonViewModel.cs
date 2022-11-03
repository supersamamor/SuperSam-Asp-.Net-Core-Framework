using CTI.Common.Web.Utility.Extensions;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.ELMS.Web.Areas.ELMS.Models;

public record ContactPersonViewModel : BaseViewModel
{	
	[Display(Name = "Lead")]
	[Required]
	
	public string LeadId { get; init; } = "";
	public string?  ForeignKeyLead { get; set; }
	[Display(Name = "Salutation")]
	
	public string? SalutationID { get; init; }
	public string?  ForeignKeySalutation { get; set; }
	[Display(Name = "First Name")]
	[Required]
	[StringLength(35, ErrorMessage = "{0} length can't be more than {1}.")]
	public string FirstName { get; init; } = "";
	[Display(Name = "Middle Name")]
	[StringLength(30, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? MiddleName { get; init; }
	[Display(Name = "Last Name")]
	[Required]
	[StringLength(70, ErrorMessage = "{0} length can't be more than {1}.")]
	public string LastName { get; init; } = "";
	[Display(Name = "Position")]
	[Required]
	[StringLength(120, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Position { get; init; } = "";
	[Display(Name = "Is SOA Recipient")]
	public bool IsSOARecipient { get; init; }
	[Display(Name = "Is AN Signatory")]
	public bool IsANSignatory { get; init; }
	[Display(Name = "Is COL Signatory")]
	public bool IsCOLSignatory { get; init; }
	
	public DateTime LastModifiedDate { get; set; }
	public LeadViewModel? Lead { get; init; }
	public SalutationViewModel? Salutation { get; init; }
		
	
}
