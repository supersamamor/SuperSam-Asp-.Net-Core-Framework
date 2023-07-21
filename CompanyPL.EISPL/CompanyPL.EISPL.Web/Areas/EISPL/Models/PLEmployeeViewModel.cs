using CompanyPL.Common.Web.Utility.Extensions;
using CompanyPL.EISPL.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CompanyPL.EISPL.Web.Areas.EISPL.Models;

public record PLEmployeeViewModel : BaseViewModel
{	
	[Display(Name = "PL First Name")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string PLFirstName { get; init; } = "";
	[Display(Name = "PL Middle Name")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string PLMiddleName { get; init; } = "";
	[Display(Name = "PL Employee Code")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string PLEmployeeCode { get; init; } = "";
	[Display(Name = "PL Last Name")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string PLLastName { get; init; } = "";
	
	public DateTime LastModifiedDate { get; set; }
		
	public IList<PLContactInformationViewModel>? PLContactInformationList { get; set; }
	public IList<PLHealthDeclarationViewModel>? PLHealthDeclarationList { get; set; }
	public IList<TestViewModel>? TestList { get; set; }
	
}
