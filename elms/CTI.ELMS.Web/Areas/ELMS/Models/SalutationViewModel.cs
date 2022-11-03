using CTI.Common.Web.Utility.Extensions;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.ELMS.Web.Areas.ELMS.Models;

public record SalutationViewModel : BaseViewModel
{	
	[Display(Name = "Salutation Description")]
	[Required]
	[StringLength(15, ErrorMessage = "{0} length can't be more than {1}.")]
	public string SalutationDescription { get; init; } = "";
	
	public DateTime LastModifiedDate { get; set; }
		
	public IList<ContactPersonViewModel>? ContactPersonList { get; set; }
	
}
