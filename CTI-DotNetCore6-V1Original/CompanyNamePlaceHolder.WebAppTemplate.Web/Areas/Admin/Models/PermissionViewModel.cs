using System.ComponentModel.DataAnnotations;

namespace CompanyNamePlaceHolder.WebAppTemplate.Web.Areas.Admin.Models;

public record PermissionViewModel
{
    [Display(Name = "Permission")]
    public string Permission { get; init; } = "";
    [Display(Name = "Enabled")]
    public bool Enabled { get; init; } = false;
}
