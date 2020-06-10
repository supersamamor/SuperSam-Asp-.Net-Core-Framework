using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Identity.Data.Models
{
    public class User : IdentityUser
    {
        [StringLength(500)]    
        [Required]
        public string FullName { get; set; }     
    }
}
