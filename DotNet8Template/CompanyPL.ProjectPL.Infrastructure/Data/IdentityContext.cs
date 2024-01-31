using CompanyPL.ProjectPL.Core.Identity;
using CompanyPL.ProjectPL.Core.Oidc;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CompanyPL.ProjectPL.Infrastructure.Data
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
		    builder.Entity<ApplicationUser>().Property(e => e.Id).HasMaxLength(36);
            builder.Entity<ApplicationRole>().Property(e => e.Id).HasMaxLength(36);
            base.OnModelCreating(builder);
        }

        public DbSet<Entity> Entities { get; set; } = default!;
    }
}
