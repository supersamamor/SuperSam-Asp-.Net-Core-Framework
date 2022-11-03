using CTI.Common.Web.Utility.Extensions;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.ELMS.Web.Areas.ELMS.Models;

public record LeadViewModel : BaseViewModel
{	
	[Display(Name = "Type")]
	[Required]
	
	public string ClientType { get; init; } = "";
	[Display(Name = "Brand")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Brand { get; init; } = "";
	[Display(Name = "Registered Name")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Company { get; init; } = "";
	[Display(Name = "Street")]
	[StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Street { get; init; }
	[Display(Name = "City")]
	[StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? City { get; init; }
	[Display(Name = "Province")]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Province { get; init; }
	[Display(Name = "Country")]
	[Required]
	
	public string Country { get; init; } = "";
	[Display(Name = "Lead Source")]
	[Required]
	
	public string LeadSourceId { get; init; } = "";
	public string?  ForeignKeyLeadSource { get; set; }
	[Display(Name = "Lead Touchpoint")]
	[Required]
	
	public string LeadTouchpointId { get; init; } = "";
	public string?  ForeignKeyLeadTouchPoint { get; set; }
	[Display(Name = "Type of Operation")]
	[Required]
	
	public string OperationTypeID { get; init; } = "";
	public string?  ForeignKeyOperationType { get; set; }
	[Display(Name = "Nature of Business")]
	[Required]
	
	public string BusinessNatureID { get; init; } = "";
	public string?  ForeignKeyBusinessNature { get; set; }
	[Display(Name = "Classification")]
	[Required]
	
	public string BusinessNatureSubItemID { get; init; } = "";
	public string?  ForeignKeyBusinessNatureSubItem { get; set; }
	[Display(Name = "Category")]
	[Required]
	
	public string BusinessNatureCategoryID { get; init; } = "";
	public string?  ForeignKeyBusinessNatureCategory { get; set; }
	[Display(Name = "TIN No.")]
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? TINNumber { get; init; }
	[Display(Name = "Franchise")]
	public bool IsFranchise { get; init; }
	
	public DateTime LastModifiedDate { get; set; }
	public LeadSourceViewModel? LeadSource { get; init; }
	public LeadTouchPointViewModel? LeadTouchPoint { get; init; }
	public OperationTypeViewModel? OperationType { get; init; }
	public BusinessNatureViewModel? BusinessNature { get; init; }
	public BusinessNatureSubItemViewModel? BusinessNatureSubItem { get; init; }
	public BusinessNatureCategoryViewModel? BusinessNatureCategory { get; init; }
		
	public IList<ContactViewModel>? ContactList { get; set; }
	public IList<ContactPersonViewModel>? ContactPersonList { get; set; }
	public IList<ActivityViewModel>? ActivityList { get; set; }
	public IList<OfferingViewModel>? OfferingList { get; set; }
	public IList<OfferingHistoryViewModel>? OfferingHistoryList { get; set; }
	
}
