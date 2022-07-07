using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using CTI.Common.Data;
using CTI.Common.Identity.Abstractions;
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

    public DbSet<MainModulePlaceHolderState> MainModulePlaceHolder { get; set; } = default!;
    public DbSet<SubDetailItemPlaceHolderState> SubDetailItemPlaceHolder { get; set; } = default!;
    public DbSet<SubDetailListPlaceHolderState> SubDetailListPlaceHolder { get; set; } = default!;
    public DbSet<ApprovalState> Approval { get; set; } = default!;
    public DbSet<ApproverSetupState> ApproverSetup { get; set; } = default!;
    public DbSet<ApproverAssignmentState> ApproverAssignment { get; set; } = default!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes()
                                                   .SelectMany(t => t.GetProperties()))
        {
            if (property.ClrType == typeof(decimal) || property.ClrType == typeof(decimal?))
            {
                property.SetColumnType("decimal(18,6)");
            }
            if (property.Name == "CreatedBy" || property.Name == "LastModifiedBy" || property.Name == "Entity" +
                "")
            {
                property.SetMaxLength(450);
            }
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
        modelBuilder.Entity<MainModulePlaceHolderState>().HasIndex(p => p.LastModifiedDate); modelBuilder.Entity<MainModulePlaceHolderState>().HasIndex(p => p.Entity); modelBuilder.Entity<MainModulePlaceHolderState>().HasQueryFilter(e => _authenticatedUser.Entity == "DEFAULT" || e.Entity == _authenticatedUser.Entity);
        modelBuilder.Entity<SubDetailItemPlaceHolderState>().HasIndex(p => p.LastModifiedDate); modelBuilder.Entity<SubDetailItemPlaceHolderState>().HasIndex(p => p.Entity); modelBuilder.Entity<SubDetailItemPlaceHolderState>().HasQueryFilter(e => _authenticatedUser.Entity == "DEFAULT" || e.Entity == _authenticatedUser.Entity);
        modelBuilder.Entity<SubDetailListPlaceHolderState>().HasIndex(p => p.LastModifiedDate); modelBuilder.Entity<SubDetailListPlaceHolderState>().HasIndex(p => p.Entity); modelBuilder.Entity<SubDetailListPlaceHolderState>().HasQueryFilter(e => _authenticatedUser.Entity == "DEFAULT" || e.Entity == _authenticatedUser.Entity);

        modelBuilder.Entity<MainModulePlaceHolderState>().HasIndex(p => p.Code).IsUnique();
        modelBuilder.Entity<SubDetailItemPlaceHolderState>().HasIndex(p => p.Code).IsUnique();
        modelBuilder.Entity<SubDetailListPlaceHolderState>().HasIndex(p => p.Code).IsUnique();

        modelBuilder.Entity<MainModulePlaceHolderState>().Property(e => e.Code).HasMaxLength(255);

        modelBuilder.Entity<SubDetailItemPlaceHolderState>().Property(e => e.Code).HasMaxLength(255);

        modelBuilder.Entity<SubDetailListPlaceHolderState>().Property(e => e.Code).HasMaxLength(255);

        modelBuilder.Entity<MainModulePlaceHolderState>().HasMany(t => t.SubDetailItemPlaceHolderList).WithOne(l => l.MainModulePlaceHolder).HasForeignKey(t => t.MainModulePlaceHolderId);
        modelBuilder.Entity<MainModulePlaceHolderState>().HasMany(t => t.SubDetailListPlaceHolderList).WithOne(l => l.MainModulePlaceHolder).HasForeignKey(t => t.MainModulePlaceHolderId);
        modelBuilder.Entity<ApprovalState>().Property(e => e.ApproverUserId).HasMaxLength(450);
        modelBuilder.Entity<ApprovalState>().Property(e => e.Status).HasMaxLength(450);
        modelBuilder.Entity<ApproverSetupState>().Property(e => e.TableName).HasMaxLength(450);
        modelBuilder.Entity<ApproverSetupState>().Property(e => e.ApprovalType).HasMaxLength(450);
        modelBuilder.Entity<ApproverAssignmentState>().Property(e => e.ApproverUserId).HasMaxLength(450);
        base.OnModelCreating(modelBuilder);
    }
}
