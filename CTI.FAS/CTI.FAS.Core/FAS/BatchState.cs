using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.FAS.Core.FAS;

public record BatchState : BaseEntity
{
	public DateTime Date { get; init; }
	public int Batch { get; init; }
	public string? FilePath { get; init; }
	public string? Url { get; init; }
	public string? UserId { get; init; }
	public string? EmailStatus { get; init; } = Constants.EmailStatus.Pending;
	public DateTime? EmailDateTime { get; init; }
	public IList<PaymentTransactionState>? PaymentTransactionList { get; set; }
	
}
