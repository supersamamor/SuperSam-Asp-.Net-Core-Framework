using System.ComponentModel.DataAnnotations;

namespace CTI.FAS.Web.Areas.Admin.Models;

public record GroupViewModel
{
    public string? Id { get; set; } = Guid.NewGuid().ToString();
    [Required]
    public string Name { get; set; } = "";
    [Required]
    public string? Location { get; set; } = "";
    [Required]
    public string? ContactDetails { get; set; } = "";
}
