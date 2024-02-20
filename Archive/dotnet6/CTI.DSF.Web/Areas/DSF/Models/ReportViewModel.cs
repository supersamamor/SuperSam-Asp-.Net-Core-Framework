using CTI.DSF.Web.Models;
using System.ComponentModel.DataAnnotations;
namespace CTI.DSF.Web.Areas.DSF.Models;
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
    [Display(Name = "Display on Report Module")]
    public bool DisplayOnReportModule { get; set; } = false;
    public DateTime LastModifiedDate { get; set; }   
    public IList<ReportQueryFilterViewModel>? ReportQueryFilterList { get; set; }
    [Display(Name = "Role")]
    public IEnumerable<string>? ReportRoleAssignmentList { get; set; }
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
    public string? FieldLabel
    {
        get { return this.FieldDescription != null ? this.FieldDescription : this.FieldName; }
    }
}

