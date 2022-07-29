using System.ComponentModel.DataAnnotations;

namespace CTI.TenantSales.Web.Areas.Admin.Models;

public record ScopeViewModel
{
    [Required]
    [RegularExpression(@"^\S+$", ErrorMessage = "Whitespaces are not allowed")]
    [Display(Name = "Name")]
    public string Name { get; set; } = "";
    [Required]
    [Display(Name = "Description")]
    public string DisplayName { get; set; } = "";
    [Required]
    [Display(Name = "API URL")]
    public string Resources { get; set; } = "";
}
