using CTI.Common.Core.Base.Models;

namespace CTI.DSF.Core.DSF;

public record UploadProcessorState : BaseEntity
{
    public string FileType { get; init; } = "";
    public string Path { get; init; } = "";
    public string Status { get; set; } = "";
    public DateTime? StartDateTime { get; private set; }
    public DateTime? EndDateTime { get; private set; }
    public string Module { get; init; } = "";
    public string UploadType { get; init; } = "";
    public string Remarks { get; private set; } = "";
    public string ExceptionFilePath { get; private set; } = "";
    public IList<UploadStagingState>? UploadStagingList { get; set; }
    public void SetStart()
    {
        this.StartDateTime = DateTime.UtcNow;
    }
    public void SetDone()
    {
        this.Status = Constants.FileUploadStatus.Done;
        this.EndDateTime = DateTime.UtcNow;
    }
    public void SetFailed(string exceptionFilePath,string remarks)
    {
        this.Status = Constants.FileUploadStatus.Failed;
        this.EndDateTime = DateTime.UtcNow;
        this.ExceptionFilePath = exceptionFilePath;
        this.Remarks = remarks;
    }
}
