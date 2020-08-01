using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectNamePlaceHolder.Data.Models;

namespace ProjectNamePlaceHolder.Data
{
    public class ProjectNamePlaceHolderContext : IdentityDbContext<IdentityUser>
    {
        public ProjectNamePlaceHolderContext(DbContextOptions<ProjectNamePlaceHolderContext> options)
            : base(options)
        {
        }
        public virtual DbSet<ProjectNamePlaceHolderUser> ProjectNamePlaceHolderUser { get; set; }
        public virtual DbSet<MainModulePlaceHolder> MainModulePlaceHolder { get; set; }
        public virtual DbSet<ProjectNamePlaceHolderApiClient> ProjectNamePlaceHolderApiClient { get; set; }     
    }
}
