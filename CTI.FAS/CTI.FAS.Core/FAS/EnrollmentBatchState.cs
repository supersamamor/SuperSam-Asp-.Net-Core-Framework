using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.FAS.Core.FAS;

public record EnrollmentBatchState : BaseEntity
{
	public DateTime Date { get; init; }
	public int Batch { get; init; }
	public IList<EnrolledPayeeState>? EnrolledPayeeList { get; set; }
}
