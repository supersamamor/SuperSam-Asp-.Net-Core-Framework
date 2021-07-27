using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Authorization.Models
{
    public class AuthorizeViewModel
    {
        [Display(Name = "Application")]
        public string? ApplicationName { get; set; }

        [Display(Name = "Scope")]
        public string? Scope { get; set; }
    }
}
