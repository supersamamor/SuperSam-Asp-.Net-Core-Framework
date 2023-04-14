using CelerSoft.Common.Core.Base.Models;
using System.ComponentModel;

namespace CelerSoft.TurboERP.Core.TurboERP;

public record UnitState : BaseEntity
{
	public string Abbreviations { get; init; } = "";
	public string Name { get; init; } = "";
	
	
	public IList<ItemState>? ItemList { get; set; }
	
}
