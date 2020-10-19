using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectNamePlaceHolder.Data.Models;
using System.Linq;

namespace ProjectNamePlaceHolder.Data
{
    public class ProjectNamePlaceHolderContext : IdentityDbContext<IdentityUser>
    {
        public ProjectNamePlaceHolderContext(DbContextOptions<ProjectNamePlaceHolderContext> options)
            : base(options)
        {         
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.EnableSensitiveDataLogging();         
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public virtual DbSet<ProjectNamePlaceHolderUser> ProjectNamePlaceHolderUser { get; set; }
        public virtual DbSet<ProjectNamePlaceHolderApiClient> ProjectNamePlaceHolderApiClient { get; set; }     
    }
}
