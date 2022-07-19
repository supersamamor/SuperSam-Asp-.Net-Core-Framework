using CompanyNamePlaceHolder.Common.Web.Utility.Extensions;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.ProjectNamePlaceHolder.Models;

public record MainModuleViewModel : BaseViewModel
{	
	[Display(Name = "ParentModuleId")]
	[Required]
	public string ParentModuleId { get; init; } = "";
	[Display(Name = "Code")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Code { get; init; } = "";
	
	public DateTime LastModifiedDate { get; set; }
	public ParentModuleViewModel? ParentModule { get; init; }
		
	public IList<SubDetailListViewModel>? SubDetailListList { get; set; }
	public IList<SubDetailItemViewModel>? SubDetailItemList { get; set; }
	
}
