using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.FAS.Core.FAS;

public record EnrolledPayeeEmailState : BaseEntity
{
	public string Email { get; init; } = "";
	public string EnrolledPayeeId { get; init; } = "";
	
	public EnrolledPayeeState? EnrolledPayee { get; init; }
	
	
}
