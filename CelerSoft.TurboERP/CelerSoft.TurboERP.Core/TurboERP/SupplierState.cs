using CelerSoft.Common.Core.Base.Models;
using System.ComponentModel;

namespace CelerSoft.TurboERP.Core.TurboERP;

public record SupplierState : BaseEntity
{
	public string Company { get; init; } = "";
	public string? TINNumber { get; init; }
	public string Address { get; init; } = "";
	
	
	public IList<SupplierContactPersonState>? SupplierContactPersonList { get; set; }
	public IList<SupplierQuotationState>? SupplierQuotationList { get; set; }
	
}
