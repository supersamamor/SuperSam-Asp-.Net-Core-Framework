using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.FAS.Core.FAS;

public record EnrollmentBatchState : BaseEntity
{
    public DateTime Date { get; init; }
    public int Batch { get; init; }
    public string? FilePath { get; init; }
    public string? Url { get; init; }
    public string? UserId { get; init; }
    public string? EmailStatus { get; private set; } = Constants.EmailStatus.Pending;
    public DateTime? EmailDateTime { get; private set; }
    public string? CompanyId { get; init; }
    public string? BatchStatusType { get; init; }
    public IList<EnrolledPayeeState>? EnrolledPayeeList { get; set; }
    public void TagAsSent()
    {
        this.EmailStatus = Constants.EmailStatus.Sent;
        this.EmailDateTime = DateTime.Now;
    }
    public void TagAsFailed()
    {
        this.EmailStatus = Constants.EmailStatus.Failed;
        this.EmailDateTime = DateTime.Now;
    }
}
