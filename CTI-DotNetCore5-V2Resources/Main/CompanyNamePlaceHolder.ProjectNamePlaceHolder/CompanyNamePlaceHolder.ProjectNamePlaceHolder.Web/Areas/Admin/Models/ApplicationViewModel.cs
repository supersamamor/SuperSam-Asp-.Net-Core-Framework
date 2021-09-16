using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Models
{
    public record ApplicationViewModel
    {
        [Required]
        [Display(Name = "Client Id")]
        public string ClientId { get; set; } = Guid.NewGuid().ToString();
        [Required]
        [Display(Name = "Client Secret")]
        public string ClientSecret { get; set; } = Guid.NewGuid().ToString();
        [Required]
        [Display(Name = "Name")]
        public string? DisplayName { get; set; }
        [Required]
        [Display(Name = "Redirect URI")]
        public string? RedirectUri { get; set; }
        [Required]
        [Display(Name = "Scopes")]
        public string? Scopes { get; set; }
    }
}
