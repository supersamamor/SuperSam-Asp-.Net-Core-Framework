using Identity.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Identity.Data
{
    public class IdentityContext : IdentityDbContext
    {
        public IdentityContext(DbContextOptions<IdentityContext> options)
          : base(options)
        {
        }
        protected IdentityContext()
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
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
        public DbSet<ProjectNamePlaceHolderUser> ProjectNamePlaceHolderUser { get; set; }

    }
}
