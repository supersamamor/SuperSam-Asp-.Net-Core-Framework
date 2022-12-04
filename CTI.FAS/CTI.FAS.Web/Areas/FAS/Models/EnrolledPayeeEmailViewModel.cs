using CTI.Common.Web.Utility.Extensions;
using CTI.FAS.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.FAS.Web.Areas.FAS.Models;

public record EnrolledPayeeEmailViewModel : BaseViewModel
{	
	[Display(Name = "Email")]
	[Required]
	[EmailAddress]
	[StringLength(70, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Email { get; init; } = "";
	[Display(Name = "Creditor")]
	[Required]
	
	public string EnrolledPayeeId { get; init; } = "";
	public string?  ForeignKeyEnrolledPayee { get; set; }
	
	public DateTime LastModifiedDate { get; set; }
	public EnrolledPayeeViewModel? EnrolledPayee { get; init; }
		
	
}
