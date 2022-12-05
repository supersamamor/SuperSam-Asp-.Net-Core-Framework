using CTI.FAS.Core.Identity;
using CTI.FAS.Core.Oidc;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CTI.FAS.Infrastructure.Data
{
    public class IdentityContext : IdentityDbContext<ApplicationUser>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options)
            : base(options)
        {
        }
        public DbSet<OidcApplication> OpenIddictApplications { get; set; } = default!;
        public DbSet<OidcAuthorization> OpenIddictAuthorizations { get; set; } = default!;
        public DbSet<OidcScope> OpenIddictScopes { get; set; } = default!;
        public DbSet<OidcToken> OpenIddictTokens { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Entity>().HasIndex(e => e.Name).IsUnique();
            builder.Entity<ApplicationUser>().HasIndex(e => e.PplusId).IsUnique();
            builder.Entity<Group>().HasIndex(e => e.Name).IsUnique();
            base.OnModelCreating(builder);
        }

        public DbSet<Entity> Entities { get; set; } = default!;
        public DbSet<Group> Group { get; set; } = default!;
    }
}
