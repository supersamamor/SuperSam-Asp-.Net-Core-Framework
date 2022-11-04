using CTI.Common.Web.Utility.Extensions;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.ELMS.Web.Areas.ELMS.Models;

public record PPlusConnectionSetupViewModel : BaseViewModel
{	
	[Display(Name = "PPlus Version")]
	[Required]
	[StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
	public string PPlusVersionName { get; init; } = "";
	[Display(Name = "Table Prefix")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string TablePrefix { get; init; } = "";
	[Display(Name = "Connection String")]
	[Required]
	[StringLength(1000, ErrorMessage = "{0} length can't be more than {1}.")]
	public string ConnectionString { get; init; } = "";
	[Display(Name = "Exhibit Theme Codes")]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? ExhibitThemeCodes { get; init; }
	
	public DateTime LastModifiedDate { get; set; }
		
	public IList<EntityGroupViewModel>? EntityGroupList { get; set; }
	
}
