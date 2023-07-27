using CTI.Common.Core.Base.Models;


namespace CTI.DPI.Core.DPI;

public record ReportQueryFilterState : BaseEntity
{
    public string ReportId { get; init; } = "";
	public string FieldName { get; init; } = "";
    public string? FieldDescription { get; init; }
    public string DataType { get; init; } = "";
    public string? CustomDropdownValues { get; init; }
    public string? DropdownTableKeyAndValue { get; init; }  
    public string? DropdownFilter { get; init; }
    public int Sequence { get; init; }
    public ReportState? Report { get; init; }
	
	
}
