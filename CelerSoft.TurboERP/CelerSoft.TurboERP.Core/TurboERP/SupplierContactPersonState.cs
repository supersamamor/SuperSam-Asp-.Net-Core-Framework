using CelerSoft.Common.Core.Base.Models;
using System.ComponentModel;

namespace CelerSoft.TurboERP.Core.TurboERP;

public record SupplierContactPersonState : BaseEntity
{
	public string? SupplierId { get; init; }
	public string? FullName { get; init; }
	public string? Position { get; init; }
	public string Email { get; init; } = "";
	public string MobileNumber { get; init; } = "";
	public string? PhoneNumber { get; init; }
	
	public SupplierState? Supplier { get; init; }
	
	
}
