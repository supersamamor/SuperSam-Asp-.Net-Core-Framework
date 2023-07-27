using CTI.DPI.Web.Models;
using System.ComponentModel.DataAnnotations;

namespace CTI.DPI.Web.Areas.DPI.Models;

public record ReportQueryFilterViewModel : BaseViewModel
{	
	[Display(Name = "Report")]	
	public string? ReportId { get; init; }
	public string?  ForeignKeyReport { get; set; }
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
