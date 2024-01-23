using CompanyPL.Common.Core.Base.Models;
using System.ComponentModel;

namespace CompanyPL.ProjectPL.Core.ProjectPL;

public record UploadStagingState : BaseEntity
{
	public string UploadProcessorId { get; init; } = "";
	public string Data { get; init; } = "";
	public string Status { get; init; } = "";
	public DateTime? ProcessedDateTime { get; init; }
    public string Remarks { get; init; } = "";
    public UploadProcessorState? UploadProcessor { get; init; }
	
	
}
