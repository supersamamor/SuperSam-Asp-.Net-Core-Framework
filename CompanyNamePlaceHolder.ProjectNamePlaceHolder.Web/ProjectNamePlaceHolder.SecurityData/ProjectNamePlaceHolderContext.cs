using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectNamePlaceHolder.SecurityData.Models;

namespace ProjectNamePlaceHolder.SecurityData
{
    public class ProjectNamePlaceHolderContext : IdentityDbContext<IdentityUser>
    {
        public ProjectNamePlaceHolderContext(DbContextOptions<ProjectNamePlaceHolderContext> options)
            : base(options)
        {
        }
        public DbSet<ProjectNamePlaceHolderUser> ProjectNamePlaceHolderUser { get; set; }
    }
}
