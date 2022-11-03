using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.ELMS.Core.ELMS;

public record ReportTableCollectionDetailState : BaseEntity
{
	public string? ProjectID { get; init; }
	public string? IFCATenantInformationID { get; init; }
	public int? Month { get; init; }
	public string Year { get; init; } = "";
	public bool IsTerminated { get; init; }
	public decimal? CurrentMonth { get; init; }
	public decimal? PrevMonth1 { get; init; }
	public decimal? PrevMonth2 { get; init; }
	public decimal? PrevMonth3 { get; init; }
	public decimal? Prior { get; init; }
	public decimal? TotalOverDue { get; init; }
	public decimal? GrandTotal { get; init; }
	public decimal? Rental { get; init; }
	public decimal? CusaAC { get; init; }
	public decimal? Utilities { get; init; }
	public decimal? Deposits { get; init; }
	public decimal? Interests { get; init; }
	public decimal? Penalty { get; init; }
	public decimal? Others { get; init; }
	public decimal? PaidSD { get; init; }
	public decimal? SDExposure { get; init; }
	public decimal? PayableCurrentMonth { get; init; }
	public decimal? PayablePrevMonth1 { get; init; }
	public decimal? PayablePrevMonth2 { get; init; }
	public decimal? PayablePrevMonth3 { get; init; }
	public decimal? PayablePrior { get; init; }
	public string Column1 { get; init; } = "";
	public string Column2 { get; init; } = "";
	public string Column3 { get; init; } = "";
	public string Column4 { get; init; } = "";
	
	public ProjectState? Project { get; init; }
	public IFCATenantInformationState? IFCATenantInformation { get; init; }
	
	
}
