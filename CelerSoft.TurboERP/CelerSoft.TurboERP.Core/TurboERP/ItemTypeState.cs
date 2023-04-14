using CelerSoft.Common.Core.Base.Models;
using System.ComponentModel;

namespace CelerSoft.TurboERP.Core.TurboERP;

public record ItemTypeState : BaseEntity
{
	public string Name { get; init; } = "";
	
	
	public IList<ItemState>? ItemList { get; set; }
	
}
