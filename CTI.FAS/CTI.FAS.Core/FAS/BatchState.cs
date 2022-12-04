using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.FAS.Core.FAS;

public record BatchState : BaseEntity
{
	public DateTime Date { get; init; }
	public int Batch { get; init; }
	
	
	public IList<PaymentTransactionState>? PaymentTransactionList { get; set; }
	
}
