using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace SubComponentPlaceHolder.Data
{
    public class SubComponentPlaceHolderContext : DbContext
    {
        public SubComponentPlaceHolderContext(DbContextOptions<SubComponentPlaceHolderContext> options)
          : base(options)
        {
        }
        protected SubComponentPlaceHolderContext()
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region Is Unique
            builder.Entity<Models.MainModulePlaceHolder>()
            .HasIndex(c => new { c.Code }).IsUnique();

            builder.Entity<Models.MainModulePlaceHolder>()
            .HasIndex(c => new { c.MainModulePlaceHolderId }).IsUnique();
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
        public virtual DbSet<Models.MainModulePlaceHolder> MainModulePlaceHolder { get; set; }     
    }
}
