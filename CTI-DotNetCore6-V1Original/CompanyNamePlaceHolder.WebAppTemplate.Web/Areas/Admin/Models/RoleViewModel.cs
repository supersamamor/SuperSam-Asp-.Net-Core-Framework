using System.ComponentModel.DataAnnotations;

namespace CompanyNamePlaceHolder.WebAppTemplate.Web.Areas.Admin.Models;

public record RoleViewModel
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    [Required]
    [Display(Name = "Name")]
    public string Name { get; set; } = "";
}
