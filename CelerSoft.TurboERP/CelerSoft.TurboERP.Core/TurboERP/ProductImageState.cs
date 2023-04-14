using CelerSoft.Common.Core.Base.Models;
using System.ComponentModel;

namespace CelerSoft.TurboERP.Core.TurboERP;

public record ProductImageState : BaseEntity
{
	public string? ProductId { get; init; }
	public string? Path { get; init; } = "";
	
	public ProductState? Product { get; init; }
	
	
}
