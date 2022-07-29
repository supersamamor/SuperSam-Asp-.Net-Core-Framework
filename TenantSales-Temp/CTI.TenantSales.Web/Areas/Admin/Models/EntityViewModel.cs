using System.ComponentModel.DataAnnotations;

namespace CTI.TenantSales.Web.Areas.Admin.Models;

public record EntityViewModel
{
    public string? Id { get; set; } = Guid.NewGuid().ToString();
    [Required]
    public string Name { get; set; } = "";
}
