using CTI.Common.Web.Utility.Extensions;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.ELMS.Web.Areas.ELMS.Models;

public record EntityGroupViewModel : BaseViewModel
{	
	[Display(Name = "PPlus Connection Setup")]
	
	public string? PPlusConnectionSetupID { get; init; }
	public string?  ForeignKeyPPlusConnectionSetup { get; set; }
	[Display(Name = "Entity Name")]
	[Required]
	[StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
	public string EntityName { get; init; } = "";
	[Display(Name = "PPLUS Entity Code")]
	[StringLength(5, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? PPLUSEntityCode { get; init; }
	[Display(Name = "TIN Number")]
	[Required]
	[StringLength(17, ErrorMessage = "{0} length can't be more than {1}.")]
	public string TINNo { get; init; } = "";
	[Display(Name = "Entity Short Name")]
	[Required]
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string EntityShortName { get; init; } = "";
	[Display(Name = "Is Disabled")]
	public bool IsDisabled { get; init; }
	[Display(Name = "Entity Address")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string EntityAddress { get; init; } = "";
	[Display(Name = "Entity Description")]
	[Required]
	[StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
	public string EntityDescription { get; init; } = "";
	[Display(Name = "Entity Address 2")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string EntityAddress2 { get; init; } = "";
	
	public DateTime LastModifiedDate { get; set; }
	public PPlusConnectionSetupViewModel? PPlusConnectionSetup { get; init; }
		
	public IList<ProjectViewModel>? ProjectList { get; set; }
	public IList<IFCATransactionTypeViewModel>? IFCATransactionTypeList { get; set; }
	
}
