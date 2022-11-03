using System.ComponentModel.DataAnnotations;

namespace CTI.ELMS.Web.Areas.Authorization.Models;

public class AuthorizeViewModel
{
    [Display(Name = "Application")]
    public string? ApplicationName { get; set; }

    [Display(Name = "Scope")]
    public string? Scope { get; set; }
}
