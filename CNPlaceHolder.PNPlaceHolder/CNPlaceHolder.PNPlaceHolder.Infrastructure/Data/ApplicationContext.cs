using CNPlaceHolder.Common.Data;
using CNPlaceHolder.Common.Identity.Abstractions;
using CNPlaceHolder.PNPlaceHolder.Core.PNPlaceHolder;
using Microsoft.EntityFrameworkCore;

namespace CNPlaceHolder.PNPlaceHolder.Infrastructure.Data;

public class ApplicationContext : AuditableDbContext<ApplicationContext>
{
    private readonly IAuthenticatedUser _authenticatedUser;

    public ApplicationContext(DbContextOptions<ApplicationContext> options,
                              IAuthenticatedUser authenticatedUser) : base(options, authenticatedUser)
    {
        _authenticatedUser = authenticatedUser;
    }

    public DbSet<TnamePlaceHolderState> TnamePlaceHolder { get; set; } = default!;
	
	
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes()
                                                   .SelectMany(t => t.GetProperties())
                                                   .Where(p => p.ClrType == typeof(decimal)
                                                               || p.ClrType == typeof(decimal?)))
        {
            property.SetColumnType("decimal(18,6)");
        }
        #region Disable Cascade Delete
        var cascadeFKs = modelBuilder.Model.GetEntityTypes()
        .SelectMany(t => t.GetForeignKeys())
        .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);
        foreach (var fk in cascadeFKs)
        {
            fk.DeleteBehavior = DeleteBehavior.Restrict;
        }
        #endregion
        modelBuilder.Entity<Audit>().Property(e => e.PrimaryKey).HasMaxLength(120);
        modelBuilder.Entity<Audit>().HasIndex(p => p.PrimaryKey);
        // NOTE: DO NOT CREATE EXTENSION METHOD FOR QUERY FILTER!!!
        // It causes filter to be evaluated before user has signed in
        modelBuilder.Entity<TnamePlaceHolderState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<TnamePlaceHolderState>().HasIndex(p => p.Entity);modelBuilder.Entity<TnamePlaceHolderState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		
        modelBuilder.Entity<TnamePlaceHolderState>().HasIndex(p => p.Colname).IsUnique();
		
        modelBuilder.Entity<TnamePlaceHolderState>().Property(e => e.Colname).HasMaxLength(255);
		
        
		
        base.OnModelCreating(modelBuilder);
    }
}
