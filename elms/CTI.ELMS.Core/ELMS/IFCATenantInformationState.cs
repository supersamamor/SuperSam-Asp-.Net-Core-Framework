using CTI.Common.Core.Base.Models;
using System.ComponentModel;

namespace CTI.ELMS.Core.ELMS;

public record IFCATenantInformationState : BaseEntity
{
	public string OfferingID { get; init; } = "";
	public string TenantContractNo { get; init; } = "";
	public bool IsExhibit { get; init; }
	public string ProjectID { get; init; } = "";
	public string TradeName { get; init; } = "";
	public string TINNumber { get; init; } = "";
	public decimal? PaidSecurityDeposit { get; init; }
	public decimal? Allowance { get; init; }
	public string TenantCategory { get; init; } = "";
	public bool IsAnchor { get; init; }
	public string TenantClassification { get; init; } = "";
	
	public OfferingState? Offering { get; init; }
	public ProjectState? Project { get; init; }
	
	public IList<IFCAUnitInformationState>? IFCAUnitInformationList { get; set; }
	public IList<ReportTableCollectionDetailState>? ReportTableCollectionDetailList { get; set; }
	
}
