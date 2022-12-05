using CTI.Common.Core.Base.Models;

namespace CTI.FAS.Core.FAS;

public record EnrolledPayeeState : BaseEntity
{
    public string CompanyId { get; init; } = "";
    public string CreditorId { get; init; } = "";
    public string PayeeAccountNumber { get; init; } = "";
    public string PayeeAccountType { get; init; } = "";
    public string Email { get; init; } = "";
    public string? Status { get; private set; } = Constants.EnrollmentStatus.Active;
    public string? EnrollmentBatchId { get; init; }
    public CompanyState? Company { get; init; }
    public CreditorState? Creditor { get; init; }
    public IList<PaymentTransactionState>? PaymentTransactionList { get; set; }
    public IList<EnrolledPayeeEmailState>? EnrolledPayeeEmailList { get; set; }
    public EnrollmentBatchState? EnrollmentBatch { get; init; }
    public void TagAsNew()
    {
        this.Status = Constants.EnrollmentStatus.New;
    }
    public void TagAsActive()
    {
        this.Status = Constants.EnrollmentStatus.Active;
    }
}
