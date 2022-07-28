using CompanyNamePlaceHolder.Common.Web.Utility.Extensions;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.ProjectNamePlaceHolder.Models;

public record SubDetailListViewModel : BaseViewModel
{	
	[Display(Name = "Code")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Code { get; init; } = "";
	[Display(Name = "MainModulePlaceHolder")]
	[Required]
	public string TestForeignKeyOne { get; init; } = "";
	public string?  ForeignKeyMainModule { get; set; }
	
	public DateTime LastModifiedDate { get; set; }
	public MainModuleViewModel? MainModule { get; init; }
		
	
}
