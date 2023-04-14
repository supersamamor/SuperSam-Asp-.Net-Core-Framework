using CelerSoft.Common.Core.Base.Models;
using System.ComponentModel;

namespace CelerSoft.TurboERP.Core.TurboERP;

public record BrandState : BaseEntity
{
	public string Name { get; init; } = "";
	
	
	public IList<ProductState>? ProductList { get; set; }
	
}
