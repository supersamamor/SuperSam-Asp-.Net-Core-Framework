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
	public string?  ForeignKeyParentModule { get; set; }
	[Display(Name = "File Upload")]	
	public string? FileUpload { get; init; }
	[Required]
	public IFormFile? FileUploadForm { get; set; }
	public string? GeneratedFileUploadPath
	{
		get
		{
			return this.FileUploadForm?.FileName == null ? this.FileUpload : "\\" + WebConstants.MainModule + "\\" + this.Id + "\\" + nameof(this.FileUpload) + "\\" + this.FileUploadForm!.FileName;
		}
	}
	[Display(Name = "Code")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Code { get; init; } = "";
	
	public DateTime LastModifiedDate { get; set; }
	public ParentModuleViewModel? ParentModule { get; init; }
		
	public IList<SubDetailItemViewModel>? SubDetailItemList { get; set; }
	public IList<SubDetailListViewModel>? SubDetailListList { get; set; }
	
}
