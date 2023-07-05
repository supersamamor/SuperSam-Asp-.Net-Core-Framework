using CNPlaceHolder.Common.Web.Utility.Extensions;
using CNPlaceHolder.PNPlaceHolder.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CNPlaceHolder.PNPlaceHolder.Web.Areas.PNPlaceHolder.Models;

public record TnamePlaceHolderViewModel : BaseViewModel
{	
	[Display(Name = "Column Description")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Colname { get; init; } = "";
	
	public DateTime LastModifiedDate { get; set; }
		
	
}
