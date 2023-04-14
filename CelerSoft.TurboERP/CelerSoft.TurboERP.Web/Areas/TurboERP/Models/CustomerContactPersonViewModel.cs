using CelerSoft.Common.Web.Utility.Extensions;
using CelerSoft.TurboERP.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CelerSoft.TurboERP.Web.Areas.TurboERP.Models;

public record CustomerContactPersonViewModel : BaseViewModel
{	
	[Display(Name = "Customer")]
	
	public string? CustomerId { get; init; }
	public string?  ForeignKeyCustomer { get; set; }
	[Required]
	[Display(Name = "Name")]
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? FullName { get; init; }
	[Display(Name = "Position")]
	[StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? Position { get; init; }
	[Display(Name = "Email")]
	[Required]
	[EmailAddress]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Email { get; init; } = "";
	[Display(Name = "Mobile Number")]
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string MobileNumber { get; init; } = "";
	[Display(Name = "Phone Number")]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? PhoneNumber { get; init; }
	
	public DateTime LastModifiedDate { get; set; }
	public CustomerViewModel? Customer { get; init; }
		
	
}
