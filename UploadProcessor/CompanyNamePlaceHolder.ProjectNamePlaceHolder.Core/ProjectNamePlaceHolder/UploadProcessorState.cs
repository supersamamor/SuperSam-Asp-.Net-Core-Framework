using CompanyNamePlaceHolder.Common.Core.Base.Models;
using System.ComponentModel;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;

public record UploadProcessorState : BaseEntity
{
	public string FileType { get; init; } = "";
	public string Path { get; init; } = "";
	public string Status { get; init; } = "";
	public DateTime? StartDateTime { get; init; }
	public DateTime? EndDateTime { get; init; }
	public string Module { get; init; } = "";
	public string UploadType { get; init; } = "";
	
	
	public IList<UploadStagingState>? UploadStagingList { get; set; }
	
}
