using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ProjectNamePlaceHolder.SecurityData.Models
{
    public class AppUser : IdentityUser
    {      
        [StringLength(500)]
        public string FullName { get; set; }
    }
}
