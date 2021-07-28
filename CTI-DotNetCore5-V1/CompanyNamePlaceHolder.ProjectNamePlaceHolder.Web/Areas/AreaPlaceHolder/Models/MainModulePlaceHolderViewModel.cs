using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.AreaPlaceHolder.Models
{
    public record MainModulePlaceHolderViewModel
    {
        public string? Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string? Code { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? Type { get; set; }
        [Required]
        public string? Status { get; set; }
    }
}
