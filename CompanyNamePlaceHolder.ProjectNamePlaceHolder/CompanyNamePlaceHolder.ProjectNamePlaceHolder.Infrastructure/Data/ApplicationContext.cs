using CompanyNamePlaceHolder.Common.Data;
using CompanyNamePlaceHolder.Common.Identity.Abstractions;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using Microsoft.EntityFrameworkCore;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;

public class ApplicationContext : AuditableDbContext<ApplicationContext>
{
    private readonly IAuthenticatedUser _authenticatedUser;

    public ApplicationContext(DbContextOptions<ApplicationContext> options,
                              IAuthenticatedUser authenticatedUser) : base(options, authenticatedUser)
    {
        _authenticatedUser = authenticatedUser;
    }
    public DbSet<ReportState> Report { get; set; } = default!;
    public DbSet<ReportTableState> ReportTable { get; set; } = default!;
    public DbSet<ReportTableJoinParameterState> ReportTableJoinParameter { get; set; } = default!;
    public DbSet<ReportColumnHeaderState> ReportColumnHeader { get; set; } = default!;
    public DbSet<ReportColumnDetailState> ReportColumnDetail { get; set; } = default!;
    public DbSet<ReportFilterGroupingState> ReportFilterGrouping { get; set; } = default!;
    public DbSet<ReportColumnFilterState> ReportColumnFilter { get; set; } = default!;
    public DbSet<ReportQueryFilterState> ReportQueryFilter { get; set; } = default!;
    public DbSet<ReportRoleAssignmentState> ReportRoleAssignment { get; set; } = default!;
    public DbSet<MainModulePlaceHolderState> MainModulePlaceHolder { get; set; } = default!;
	
	
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
        modelBuilder.Entity<MainModulePlaceHolderState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<MainModulePlaceHolderState>().HasIndex(p => p.Entity);modelBuilder.Entity<MainModulePlaceHolderState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		
        modelBuilder.Entity<MainModulePlaceHolderState>().HasIndex(p => p.Code).IsUnique();
		
        modelBuilder.Entity<MainModulePlaceHolderState>().Property(e => e.Code).HasMaxLength(255);
		
        
		
        base.OnModelCreating(modelBuilder);
    }
}
