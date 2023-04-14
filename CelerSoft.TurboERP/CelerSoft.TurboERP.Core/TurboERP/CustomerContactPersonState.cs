using CelerSoft.Common.Core.Base.Models;
using System.ComponentModel;

namespace CelerSoft.TurboERP.Core.TurboERP;

public record CustomerContactPersonState : BaseEntity
{
	public string? CustomerId { get; init; }
	public string? FullName { get; init; }
	public string? Position { get; init; }
	public string Email { get; init; } = "";
	public string MobileNumber { get; init; } = "";
	public string? PhoneNumber { get; init; }
	
	public CustomerState? Customer { get; init; }
	
	
}
