using CompanyPL.Common.Web.Utility.Extensions;
using CompanyPL.EISPL.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CompanyPL.EISPL.Web.Areas.EISPL.Models;

public record TestViewModel : BaseViewModel
{	
	[Display(Name = "Employee")]
	[Required]
	
	public string PLEmployeeId { get; init; } = "";
	public string?  ForeignKeyPLEmployee { get; set; }
	[Display(Name = "Test Column")]
	[Required]
	
	public string TestColumn { get; init; } = "";
	
	public DateTime LastModifiedDate { get; set; }
	public PLEmployeeViewModel? PLEmployee { get; init; }
		
	
}
