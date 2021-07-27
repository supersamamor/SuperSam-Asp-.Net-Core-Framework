using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Models
{
    public record EntityViewModel
    {
        public string? Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string? Name { get; set; }
    }
}
