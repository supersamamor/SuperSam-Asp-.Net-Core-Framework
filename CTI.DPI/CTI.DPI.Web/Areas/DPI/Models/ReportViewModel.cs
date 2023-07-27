using CTI.DPI.Web.Models;
using System.ComponentModel.DataAnnotations;
namespace CTI.DPI.Web.Areas.DPI.Models;
public record ReportViewModel : BaseViewModel
{	
	[Display(Name = "Report Name")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string ReportName { get; init; } = "";
	[Display(Name = "Query Type")]
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string QueryType { get; init; } = "";
	[Display(Name = "Report / Chart Type")]
	[Required]
	[StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
	public string ReportOrChartType { get; init; } = "";
	[Display(Name = "Distinct")]
	[Required]
	public bool IsDistinct { get; init; }
	[Display(Name = "Query String")]
	[StringLength(8000, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? QueryString { get; init; }
	[Display(Name = "Display on Dashboard")]
	public bool DisplayOnDashboard { get; set; } = true;
    [Display(Name = "Sequence")]
    [Required]
    public int Sequence { get; init; }
    public DateTime LastModifiedDate { get; set; }		
	public IList<ReportTableViewModel>? ReportTableList { get; set; }
	public IList<ReportTableJoinParameterViewModel>? ReportTableJoinParameterList { get; set; }
	public IList<ReportColumnHeaderViewModel>? ReportColumnHeaderList { get; set; }
	public IList<ReportFilterGroupingViewModel>? ReportFilterGroupingList { get; set; }
	public IList<ReportQueryFilterViewModel>? ReportQueryFilterList { get; set; }
	
}
public record ReportColumnDetailViewModel : BaseViewModel
{
    [Display(Name = "Report Column")]
    public string? ReportColumnId { get; init; }
    public string? ForeignKeyReportColumnHeader { get; set; }
    [Display(Name = "Table")]
    public string? TableId { get; init; }
    public string? ForeignKeyReportTable { get; set; }
    [Display(Name = "Field Name")]
    [StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
    public string? FieldName { get; init; }
    [Display(Name = "Function")]
    [StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
    public string? Function { get; init; }
    [Display(Name = "Arithmetic Operator")]
    [StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
    public string? ArithmeticOperator { get; init; }
    [Display(Name = "Sequence")]
    public int? Sequence { get; init; }
    public DateTime LastModifiedDate { get; set; }
    public ReportColumnHeaderViewModel? ReportColumnHeader { get; init; }
    public ReportTableViewModel? ReportTable { get; init; }
}
public record ReportColumnFilterViewModel : BaseViewModel
{
    [Display(Name = "Report Filter Grouping")]
    public string? ReportFilterGroupingId { get; init; }
    public string? ForeignKeyReportFilterGrouping { get; set; }
    [Display(Name = "Logical Operator")]
    [StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
    public string? LogicalOperator { get; init; }
    [Display(Name = "Table")]
    public string? TableId { get; init; }
    public string? ForeignKeyReportTable { get; set; }
    [Display(Name = "Field Name")]
    [StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
    public string? FieldName { get; init; }
    [Display(Name = "Comparison Operator")]
    [StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
    public string? ComparisonOperator { get; init; }
    [Display(Name = "Is String")]
    public bool IsString { get; init; }
    public DateTime LastModifiedDate { get; set; }
    public ReportFilterGroupingViewModel? ReportFilterGrouping { get; init; }
    public ReportTableViewModel? ReportTable { get; init; }
}

public record ReportColumnHeaderViewModel : BaseViewModel
{
    [Display(Name = "Report")]
    public string? ReportId { get; init; }
    public string? ForeignKeyReport { get; set; }
    [Display(Name = "Alias")]
    [StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
    public string? Alias { get; init; }
    [Display(Name = "Aggregation Operator")]
    [StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
    public string? AggregationOperator { get; init; }
    public DateTime LastModifiedDate { get; set; }
    public ReportViewModel? Report { get; init; }
    public IList<ReportColumnDetailViewModel>? ReportColumnDetailList { get; set; }
}

public record ReportFilterGroupingViewModel : BaseViewModel
{
    [Display(Name = "Report")]
    public string? ReportId { get; init; }
    public string? ForeignKeyReport { get; set; }
    [Display(Name = "Logical Operator")]
    [StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
    public string? LogicalOperator { get; init; }
    [Display(Name = "Level")]
    public int? GroupLevel { get; init; }
    [Display(Name = "Sequence")]
    public int? Sequence { get; init; }
    public DateTime LastModifiedDate { get; set; }
    public ReportViewModel? Report { get; init; }
    public IList<ReportColumnFilterViewModel>? ReportColumnFilterList { get; set; }
}

public record ReportQueryFilterViewModel : BaseViewModel
{
    [Display(Name = "Report")]
    public string? ReportId { get; init; }
    public string? ForeignKeyReport { get; set; }
    [Display(Name = "Field Name")]
    [Required]
    [StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
    public string? FieldName { get; init; }
    [Display(Name = "Field Description")]
    [StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
    public string? FieldDescription { get; init; }
    [Display(Name = "Data Type")]
    [Required]
    [StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
    public string DataType { get; init; } = "";
    [Display(Name = "Custom Dropdown Values")]
    [StringLength(1000, ErrorMessage = "{0} length can't be more than {1}.")]
    public string? CustomDropdownValues { get; init; }
    [Display(Name = "Dropdown (Table, Key, Value)")]
    [StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
    public string? DropdownTableKeyAndValue { get; init; }
    [Display(Name = "Dropdown Filter")]
    [StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
    public string? DropdownFilter { get; init; }
    public int Sequence { get; init; }
    public DateTime LastModifiedDate { get; set; }
    public ReportViewModel? Report { get; init; }
    public string FieldValue { get; init; } = "";
}
public record ReportTableJoinParameterViewModel : BaseViewModel
{
    [Display(Name = "Report")]
    [Required]
    [StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
    public string ReportId { get; init; } = "";
    public string? ForeignKeyReport { get; set; }
    [Display(Name = "Logical Operator")]
    [StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
    public string? LogicalOperator { get; init; }
    [Display(Name = "Table")]
    [Required]
    public string TableId { get; init; } = "";
    public string? ForeignKeyReportTable { get; set; }
    [Display(Name = "Field Name")]
    [Required]
    [StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
    public string FieldName { get; init; } = "";
    [Display(Name = "Join from Table")]
    [Required]
    public string JoinFromTableId { get; init; } = "";
    [Display(Name = "Join from FieldName")]
    [Required]
    [StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
    public string JoinFromFieldName { get; init; } = "";
    [Display(Name = "Sequence")]
    [Required]
    public int Sequence { get; init; }
    public DateTime LastModifiedDate { get; set; }
    public ReportViewModel? Report { get; init; }
    public ReportTableViewModel? ReportTable { get; init; }
}

public record ReportTableViewModel : BaseViewModel
{
    [Display(Name = "Report")]
    [Required]
    public string ReportId { get; init; } = "";
    public string? ForeignKeyReport { get; set; }
    [Display(Name = "Table Name")]
    [Required]
    [StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
    public string TableName { get; init; } = "";
    [Display(Name = "Alias")]
    [StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
    public string? Alias { get; init; }
    [Display(Name = "Join Type")]
    [StringLength(50, ErrorMessage = "{0} length can't be more than {1}.")]
    public string? JoinType { get; init; }
    [Display(Name = "Sequence")]
    [Required]
    public int Sequence { get; init; }
    public DateTime LastModifiedDate { get; set; }
    public ReportViewModel? Report { get; init; }
    public IList<ReportTableJoinParameterViewModel>? ReportTableJoinParameterList { get; set; }
    public IList<ReportColumnDetailViewModel>? ReportColumnDetailList { get; set; }
    public IList<ReportColumnFilterViewModel>? ReportColumnFilterList { get; set; }
}
