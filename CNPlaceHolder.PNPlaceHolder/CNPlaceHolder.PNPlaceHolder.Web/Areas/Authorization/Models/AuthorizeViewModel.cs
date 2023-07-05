using System.ComponentModel.DataAnnotations;

namespace CNPlaceHolder.PNPlaceHolder.Web.Areas.Authorization.Models;

public class AuthorizeViewModel
{
    [Display(Name = "Application")]
    public string? ApplicationName { get; set; }

    [Display(Name = "Scope")]
    public string? Scope { get; set; }
}
