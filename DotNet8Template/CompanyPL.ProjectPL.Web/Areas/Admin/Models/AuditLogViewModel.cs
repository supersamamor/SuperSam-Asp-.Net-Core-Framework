using CompanyPL.Common.Utility.Extensions;
using CompanyPL.ProjectPL.Application.Helpers;
using System.ComponentModel.DataAnnotations;

namespace CompanyPL.ProjectPL.Web.Areas.Admin.Models;

public record AuditLogViewModel
{
    public int Id { get; set; }

    [Display(Name = "User")]
    public string? UserId { get; set; } = "";

    [Display(Name = "Type")]
    public string? Type { get; set; } = "";

    [Display(Name = "Table")]
    public string? TableName { get; set; } = "";

    [Display(Name = "Timestamp")]
    public DateTime DateTime { get; set; }

    [Display(Name = "Old Values")]
    public string? OldValues { get; set; } = "";

    [Display(Name = "New Values")]
    public string? NewValues { get; set; } = "";

    [Display(Name = "Affected Columns")]
    public string AffectedColumns { get; set; } = "";

    [Display(Name = "Primary Key")]
    public string? PrimaryKey { get; set; } = "";

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
    public string? TypeFormatted
    {
        get
        {
            string buttonColor = "success";
            string buttonButtonClass = "fas fa-save";
            if (Enum.TryParse(this.Type, out Common.Data.AuditType auditType))
            {
                switch (auditType)
                {
                    case Common.Data.AuditType.Create:
                        buttonColor = "success";
                        buttonButtonClass = "fas fa-save";
                        break;
                    case Common.Data.AuditType.Update:
                        buttonColor = "info";
                        buttonButtonClass = "fas fa-pen";
                        break;
                    case Common.Data.AuditType.Delete:
                        buttonColor = "warning";
                        buttonButtonClass = "fas fa-trash";
                        break;
                    default:                       
                        break;
                }
            }         
            return "<div class=\"timeline-badge " + buttonColor + "\"> <i class=\"" + buttonButtonClass + "\" title=\"" + this.Type + "\"></i></div>";
        }
    }
    public string? DateTimeFormatted
    {
        get
        {           
            return this.DateTime.ApplyTimeOffset().ToString("MMM dd, yyyy hh:mm:ss tt");
        }
    }
    public string? DateTimeDuration
    {
        get
        {          
            // Current date
            DateTime currentDate = DateTime.UtcNow;
            // Calculate difference
            TimeSpan difference = currentDate - this.DateTime;
            // Calculate years, months, days, hours
            int years = currentDate.Year - this.DateTime.Year;
            int months = currentDate.Month - this.DateTime.Month;
            int days = currentDate.Day - this.DateTime.Day;
            int hours = currentDate.Hour - this.DateTime.Hour;
            int minutes = currentDate.Minute - this.DateTime.Minute;

            // Adjust for negative months, days, hours
            if (months < 0 || (months == 0 && days < 0) || (months == 0 && days == 0 && hours < 0))
            {
                years--;
                months += 12;
            }

            if (days < 0 || (days == 0 && hours < 0))
            {
                months--;
                days += DateTime.DaysInMonth(this.DateTime.Year, this.DateTime.Month);
            }

            if (hours < 0)
            {
                days--;
                hours += 24;
            }
            if (minutes < 0)
            {
                hours--;
                minutes += 60;
            }
            // Format the output
            string result = "";
            if (years > 0)
            {
                result += $" {years} yr{(years > 1 ? "s" : "")}, ";
            }
            if (months > 0)
            {
                result += $" {months} month{(months > 1 ? "s" : "")}, ";
            }
            if (days > 0)
            {
                result += $" {days} day{(days > 1 ? "s" : "")}, ";
            }
            if (hours > 0)
            {
                result += $" {hours} hour{(hours > 1 ? "s" : "")}";
            }
            if (minutes > 0)
            {
                if (result.Trim() != "") // Check if there are already other metrics included
                {
                    result += " and"; // Add 'and' separator
                }
                result += $" {minutes} minute{(minutes > 1 ? "s" : "")}";
            }
            else
            {               
                result = result.TrimEnd(' ', ','); // Trim trailing space or comma
            }
            return result.Trim().Replace("  ", " ");
        }
    }
}

public record AuditLogUserViewModel
{
    [Display(Name = "User")]
    public string Id { get; set; } = "";

    [Display(Name = "User")]
    public string Name { get; set; } = "";
}
