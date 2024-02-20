using CompanyNamePlaceHolder.Common.Utility.Extensions;
using System.ComponentModel.DataAnnotations;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Models;

public record AuditLogViewModel
{
    public int Id { get; set; }

    [Display(Name = "User")]
    public string UserId { get; set; } = "";

    [Display(Name = "Type")]
    public string Type { get; set; } = "";

    [Display(Name = "Table")]
    public string TableName { get; set; } = "";

    [Display(Name = "Timestamp")]
    public DateTime DateTime { get; set; }

    [Display(Name = "Old Values")]
    public string OldValues { get; set; } = "";

    [Display(Name = "New Values")]
    public string NewValues { get; set; } = "";

    [Display(Name = "Affected Columns")]
    public string AffectedColumns { get; set; } = "";

    [Display(Name = "Primary Key")]
    public string PrimaryKey { get; set; } = "";

    [Display(Name = "Timestamp")]
    public string Timestamp => DateTime.ToString("R");

    [Display(Name = "Affected Columns")]
    public string AffectedColumnsPretty => AffectedColumns.JsonPrettify();

    [Display(Name = "Primary Key")]
    public string PrimaryKeyPretty => PrimaryKey.JsonPrettify();

    [Display(Name = "Old Values")]
    public string OldValuesPretty => OldValues.JsonPrettify();

    [Display(Name = "New Values")]
    public string NewValuesPretty => NewValues.JsonPrettify();

    [Display(Name = "Timestamp")]
    public DateTime TimeStamp => DateTime.ToLocalTime();

    [Display(Name = "TraceId")]
    public string? TraceId { get; set; }

    public AuditLogUserViewModel User { get; set; } = new();
}

public record AuditLogUserViewModel
{
    [Display(Name = "User")]
    public string Id { get; set; } = "";

    [Display(Name = "User")]
    public string Name { get; set; } = "";
}
