using CNPlaceHolder.Common.Web.Utility.Extensions;
using CNPlaceHolder.PNPlaceHolder.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CNPlaceHolder.PNPlaceHolder.Web.Areas.PNPlaceHolder.Models;

public record ModPlaceHolderViewModel : BaseViewModel
{	
	[Display(Name = "Col Description")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string ColPlaceHolder { get; init; } = "";
	
	public DateTime LastModifiedDate { get; set; }
		
	
}
