using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Models
{
    public record PermissionViewModel
    {
        [Display(Name = "Permission")]
        public string? Permission { get; init; }
        [Display(Name = "Enabled")]
        public bool Enabled { get; init; } = false;
    }
}
