using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Models
{
    public record RoleViewModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; } = "";
    }
}
