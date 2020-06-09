using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Template.Data
{
    public class TemplateContext : DbContext
    {
        public TemplateContext(DbContextOptions<TemplateContext> options)
          : base(options)
        {
        }
        protected TemplateContext()
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region Is Unique
            builder.Entity<Models.Template>()
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
        public virtual DbSet<Models.Template> Template { get; set; }     
    }
}
