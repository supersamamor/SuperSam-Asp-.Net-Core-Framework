using CTI.Common.Web.Utility.Extensions;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.ELMS.Web.Areas.ELMS.Models;

public record IFCATransactionTypeViewModel : BaseViewModel
{	
	[Display(Name = "Trans Code")]
	[Required]
	[StringLength(10, ErrorMessage = "{0} length can't be more than {1}.")]
	public string TransCode { get; init; } = "";
	[Display(Name = "Trans Group")]
	[Required]
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string TransGroup { get; init; } = "";
	[Display(Name = "Description")]
	[Required]
	[StringLength(500, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Description { get; init; } = "";
	[Display(Name = "Entity")]
	
	public string? EntityID { get; init; }
	public string?  ForeignKeyEntityGroup { get; set; }
	[Display(Name = "Mode")]
	[Required]
	[StringLength(1, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Mode { get; init; } = "";
	
	public DateTime LastModifiedDate { get; set; }
	public EntityGroupViewModel? EntityGroup { get; init; }
		
	
}
