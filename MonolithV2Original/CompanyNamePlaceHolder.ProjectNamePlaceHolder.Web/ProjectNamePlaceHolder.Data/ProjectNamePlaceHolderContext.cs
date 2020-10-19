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
            #region Is Unique
            builder.Entity<Models.MainModulePlaceHolder>()
            .HasIndex(c => new { c.Code }).IsUnique();
            #endregion

            #region Disable Cascade Delete
            var cascadeFKs = builder.Model.GetEntityTypes()
           .SelectMany(t => t.GetForeignKeys())
           .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);
            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }
            #endregion

          
            base.OnModelCreating(builder);
        }
        public virtual DbSet<ProjectNamePlaceHolderUser> ProjectNamePlaceHolderUser { get; set; }
        public virtual DbSet<MainModulePlaceHolder> MainModulePlaceHolder { get; set; }
        public virtual DbSet<ProjectNamePlaceHolderApiClient> ProjectNamePlaceHolderApiClient { get; set; }     
    }
}
