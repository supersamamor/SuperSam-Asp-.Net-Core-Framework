using CTI.Common.Web.Utility.Identity;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.Inventory;
using Microsoft.EntityFrameworkCore;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;

public class ApplicationContext : AuditableDbContext
{
    private readonly IAuthenticatedUser _authenticatedUser;

    public ApplicationContext(DbContextOptions<ApplicationContext> options,
                              IAuthenticatedUser authenticatedUser) : base(options, authenticatedUser)
    {
        _authenticatedUser = authenticatedUser;
    }

    public DbSet<ProjectState> Projects { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes()
                                                   .SelectMany(t => t.GetProperties())
                                                   .Where(p => p.ClrType == typeof(decimal)
                                                               || p.ClrType == typeof(decimal?)))
        {
            property.SetColumnType("decimal(18,6)");
        }

        // NOTE: DO NOT CREATE EXTENSION METHOD FOR QUERY FILTER!!!
        // It causes filter to be evaluated before user has signed in

        modelBuilder.Entity<ProjectState>().HasIndex(e => e.Name);
        modelBuilder.Entity<ProjectState>().HasIndex(e => e.Code);
        modelBuilder.Entity<ProjectState>().HasIndex(e => e.Status);
        modelBuilder.Entity<ProjectState>().HasIndex(e => e.LastModifiedDate);
        modelBuilder.Entity<ProjectState>().HasIndex(e => e.Entity);
        modelBuilder.Entity<ProjectState>().HasQueryFilter(e => _authenticatedUser.Entity == "DEFAULT" || e.Entity == _authenticatedUser.Entity);

        base.OnModelCreating(modelBuilder);
    }
}
