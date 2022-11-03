using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.ELMS.Core.ELMS;

public record IFCAARAllocationState : BaseEntity
{
	public string? ProjectID { get; init; }
	public string TenantContractNo { get; init; } = "";
	public string DocumentNo { get; init; } = "";
	public decimal? TransactionAmount { get; init; }
	public string TransactionType { get; init; } = "";
	public DateTime? DocumentDate { get; init; }
	
	public ProjectState? Project { get; init; }
	
	
}
