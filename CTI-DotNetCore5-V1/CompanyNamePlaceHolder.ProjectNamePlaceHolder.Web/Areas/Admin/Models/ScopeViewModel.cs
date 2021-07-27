using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Models
{
    public record ScopeViewModel
    {
        [Required]
        [RegularExpression(@"^\S+$", ErrorMessage = "Whitespaces are not allowed")]
        [Display(Name = "Name")]
        public string? Name { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string? DisplayName { get; set; }
        [Required]
        [Display(Name = "API URL")]
        public string? Resources { get; set; }
    }
}
