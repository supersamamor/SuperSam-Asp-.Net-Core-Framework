using CompanyNamePlaceHolder.Common.Web.Utility.Extensions;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.ProjectNamePlaceHolder.Models;

public record UploadStagingViewModel : BaseViewModel
{	
	[Display(Name = "File Type")]
	[Required]
	
	public string UploadProcessorId { get; init; } = "";
	public string?  ReferenceFieldUploadProcessorId { get; set; }
	[Display(Name = "Path")]
	[Required]
	
	public string Data { get; init; } = "";
	[Display(Name = "Status")]
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Status { get; init; } = "";
	[Display(Name = "Start Date/Time")]
	public DateTime? ProcessedDateTime { get; init; } = DateTime.Now.Date;
	
	public DateTime LastModifiedDate { get; set; }
	public UploadProcessorViewModel? UploadProcessor { get; init; }
		
	
}
