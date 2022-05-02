using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CTI.WebAppTemplate.Web.Areas.Identity.Data;

public class IdentityContext : IdentityDbContext<ApplicationUser>
{
    public IdentityContext(DbContextOptions<IdentityContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Entity>().HasIndex(e => e.Name).IsUnique();
        base.OnModelCreating(builder);
    }

    public DbSet<Entity> Entities { get; set; } = default!;
}
