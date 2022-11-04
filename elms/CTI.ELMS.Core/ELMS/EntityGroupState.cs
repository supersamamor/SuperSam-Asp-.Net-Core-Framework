using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.ELMS.Core.ELMS;

public record EntityGroupState : BaseEntity
{
	public string? PPlusConnectionSetupID { get; init; }
	public string EntityName { get; init; } = "";
	public string? PPLUSEntityCode { get; init; }
	public string EntityShortName { get; init; } = "";
	public string TINNo { get; init; } = "";
	public string EntityDescription { get; init; } = "";
	public string EntityAddress { get; init; } = "";
	public string? EntityAddress2 { get; init; }
	public bool IsDisabled { get; init; }
	
	public PPlusConnectionSetupState? PPlusConnectionSetup { get; init; }
	
	public IList<ProjectState>? ProjectList { get; set; }
	public IList<IFCATransactionTypeState>? IFCATransactionTypeList { get; set; }
	
}
