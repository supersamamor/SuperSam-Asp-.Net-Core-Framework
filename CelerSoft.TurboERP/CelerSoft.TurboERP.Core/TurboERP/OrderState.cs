using CelerSoft.Common.Core.Base.Models;
using System.ComponentModel;

namespace CelerSoft.TurboERP.Core.TurboERP;

public record OrderState : BaseEntity
{
	public string CheckedByFullName { get; init; } = "";
	public string? Code { get; init; }
	public string CustomerId { get; init; } = "";
	public string Remarks { get; init; } = "";
	public string ShopperUsername { get; init; } = "";
	public string? Status { get; init; }
	
	public CustomerState? Customer { get; init; }
	
	public IList<OrderItemState>? OrderItemList { get; set; }
	
}
