using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Identity.Data.Models
{
    public class ProjectNamePlaceHolderUser : BaseEntity
    {
        [StringLength(500)]    
        [Required]
        public string FullName { get; set; }
        public IdentityUser Identity { get; set; }
    }
}
