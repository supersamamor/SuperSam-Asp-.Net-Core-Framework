using CelerSoft.Common.Core.Base.Models;
using System.ComponentModel;

namespace CelerSoft.TurboERP.Core.TurboERP;

public record CustomerState : BaseEntity
{
	public string Company { get; init; } = "";
	public string? TINNumber { get; init; }
	public string Address { get; init; } = "";
	
	
	public IList<CustomerContactPersonState>? CustomerContactPersonList { get; set; }
	public IList<OrderState>? OrderList { get; set; }
	
}
