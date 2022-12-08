using CTI.Common.Core.Base.Models;
using CTI.FAS.Core.Constants;
using System.ComponentModel;

namespace CTI.FAS.Core.FAS;

public record PaymentTransactionState : BaseEntity
{
    public string EnrolledPayeeId { get; init; } = "";
    public string? BatchId { get; private set; }
    public DateTime? TransmissionDate { get; private set; }
    public string DocumentNumber { get; init; } = "";
    public DateTime DocumentDate { get; init; }
    public decimal DocumentAmount { get; init; }
    public string CheckNumber { get; init; } = "";
    public string PaymentType { get; init; } = "";
    public string TextFileName { get; init; } = "";
    public string PdfReport { get; init; } = "";
    public string GroupCode { get; init; } = "";
    public string Status { get; private set; } = "";
    public decimal IfcaBatchNumber { get; init; }
    public decimal IfcaLineNumber { get; init; }
    public int EmailSentCount { get; private set; }
    public DateTime? EmailSentDateTime { get; private set; }
    public bool IsForSending { get; private set; }
    public string AccountTransaction { get; init; } = "";
    public EnrolledPayeeState? EnrolledPayee { get; set; }
    public BatchState? Batch { get; init; }
    public void TagAsGeneratedAndSetBatch(string batchId)
    {
        this.Status = PaymentTransactionStatus.Generated;
        this.BatchId = batchId;
        this.TransmissionDate = DateTime.Now.Date;
    }
    public void TagAsSent(string batchId)
    {
        this.Status = PaymentTransactionStatus.Sent;
        this.BatchId = batchId;
        this.IsForSending = true;
    }
    public void TagAsRevoked()
    {
        this.Status = PaymentTransactionStatus.Revoked;
        this.TransmissionDate = null;
    }
    public void TagAsEmailSent()
    {
        this.EmailSentDateTime = DateTime.Now;        
        this.IsForSending = false;
        this.EmailSentCount += 1;
    }
}
