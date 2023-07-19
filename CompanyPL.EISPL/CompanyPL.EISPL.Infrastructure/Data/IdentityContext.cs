using CompanyPL.EISPL.Core.Identity;
using CompanyPL.EISPL.Core.Oidc;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CompanyPL.EISPL.Infrastructure.Data
{
    public class IdentityContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
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
            base.OnModelCreating(builder);
        }

        public DbSet<Entity> Entities { get; set; } = default!;
    }
}
