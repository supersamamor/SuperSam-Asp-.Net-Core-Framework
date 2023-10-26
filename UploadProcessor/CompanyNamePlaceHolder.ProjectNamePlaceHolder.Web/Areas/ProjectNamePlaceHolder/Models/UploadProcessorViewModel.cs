using CompanyNamePlaceHolder.Common.Web.Utility.Extensions;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.ProjectNamePlaceHolder.Models;

public record UploadProcessorViewModel : BaseViewModel
{	
	[Display(Name = "File Type")]
	[Required]
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string FileType { get; init; } = "";
	[Display(Name = "Path")]
	[Required]
	[StringLength(450, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Path { get; init; } = "";
	[Display(Name = "Status")]
	[Required]
	[StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Status { get; init; } = "";
	[Display(Name = "Start Date/Time")]
	public DateTime? StartDateTime { get; init; } = DateTime.Now.Date;
	[Display(Name = "End Date/Time")]
	public DateTime? EndDateTime { get; init; } = DateTime.Now.Date;
	[Display(Name = "Module")]
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Module { get; init; } = "";
	[Display(Name = "Upload Type")]
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string UploadType { get; init; } = "";
	
	public DateTime LastModifiedDate { get; set; }
		
	public IList<UploadStagingViewModel>? UploadStagingList { get; set; }
	
}
