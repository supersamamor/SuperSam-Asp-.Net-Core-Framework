using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectNamePlaceHolder.SecurityData.Models;
namespace ProjectNamePlaceHolder.SecurityData
{
    public class ProjectNamePlaceHolderContext : IdentityDbContext<AppUser>
    {
        public ProjectNamePlaceHolderContext(DbContextOptions<ProjectNamePlaceHolderContext> options)
            : base(options)
        {
        }  
    }
}
