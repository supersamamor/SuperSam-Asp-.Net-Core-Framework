using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.ELMS.Core.ELMS;

public record ReportTableYTDExpirySummaryState : BaseEntity
{
	public int? EntityID { get; init; }
	public string EntityShortName { get; init; } = "";
	public string EntityName { get; init; } = "";
	public int? ProjectID { get; init; }
	public string ProjectName { get; init; } = "";
	public string Location { get; init; } = "";
	public decimal? LandArea { get; init; }
	public decimal? TotalGLA { get; init; }
	public string ColumnName { get; init; } = "";
	public decimal? ExpiryLotArea { get; init; }
	public decimal? Renewed { get; init; }
	public decimal? NewLeases { get; init; }
	public decimal? WithProspectNego { get; init; }
	public int? OrderBy { get; init; }
	public int? VerticalOrderBy { get; init; }
	public int? ReportYear { get; init; }
	public DateTime? ProcessedDate { get; init; }
	
	
	
}
