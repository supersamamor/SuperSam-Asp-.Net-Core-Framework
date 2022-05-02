using CTI.Common.Web.Utility.Extensions;
using CTI.WebAppTemplate.Core.Inventory;
using CTI.WebAppTemplate.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CTI.WebAppTemplate.Web.Areas.Inventory.Models;

public record ProjectViewModel : BaseViewModel
{
    [Display(Name = "Code")]
    [Required]
    [StringLength(20, ErrorMessage = "{0} length can't be more than {1}.")]
    public string Code { get; init; } = "";

    [Display(Name = "Status")]
    [Required]
    public string Status { get; init; } = "Active";

    [Display(Name = "Name")]
    [Required]
    [StringLength(100, ErrorMessage = "{0} length can't be more than {1}.")]
    public string Name { get; init; } = "";

    [Display(Name = "Description")]
    [Required]
    public string Description { get; init; } = "";

    public SelectList Statuses { get; set; } = EnumUtilities.ToSelectList<ProjectStatuses>();
}
