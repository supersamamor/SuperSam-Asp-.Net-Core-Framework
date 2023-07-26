using CTI.DPI.Web.Models;
using System.ComponentModel.DataAnnotations;

namespace CTI.DPI.Web.Areas.DPI.Models;

public record ReportQueryFilterViewModel : BaseViewModel
{	
	[Display(Name = "Report")]
	
	public string? ReportId { get; init; }
	public string?  ForeignKeyReport { get; set; }
	[Display(Name = "Field Name")]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? FieldName { get; init; }
	public DateTime LastModifiedDate { get; set; }
	public ReportViewModel? Report { get; init; }
    public string FieldValue { get; init; } = "";
}
