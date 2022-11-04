using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.ELMS.Core.ELMS;

public record PPlusConnectionSetupState : BaseEntity
{
	public string PPlusVersionName { get; init; } = "";
	public string TablePrefix { get; init; } = "";
	public string ConnectionString { get; init; } = "";
	public string? ExhibitThemeCodes { get; init; }
	
	
	public IList<EntityGroupState>? EntityGroupList { get; set; }
	
}
