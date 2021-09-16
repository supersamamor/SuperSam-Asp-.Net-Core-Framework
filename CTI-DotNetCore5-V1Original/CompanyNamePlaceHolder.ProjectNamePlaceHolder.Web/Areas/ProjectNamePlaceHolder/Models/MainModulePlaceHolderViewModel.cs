using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.ProjectNamePlaceHolder.Models
{
    public record MainModulePlaceHolderViewModel
    {
        public string? Id { get; set; } = Guid.NewGuid().ToString();
        
        [Required]
        public string? Code { get; set; }
        
    }
}
