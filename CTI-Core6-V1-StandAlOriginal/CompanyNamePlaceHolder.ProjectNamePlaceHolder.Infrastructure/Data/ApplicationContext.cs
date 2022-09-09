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

    public DbSet<MainModuleState> MainModule { get; set; } = default!;
	public DbSet<ParentModuleState> ParentModule { get; set; } = default!;
	public DbSet<SubDetailItemState> SubDetailItem { get; set; } = default!;
	public DbSet<SubDetailListState> SubDetailList { get; set; } = default!;
	
	public DbSet<ApprovalState> Approval { get; set; } = default!;
	public DbSet<ApproverSetupState> ApproverSetup { get; set; } = default!;
	public DbSet<ApproverAssignmentState> ApproverAssignment { get; set; } = default!;
	public DbSet<ApprovalRecordState> ApprovalRecord { get; set; } = default!;
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
        modelBuilder.Entity<MainModuleState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<MainModuleState>().HasIndex(p => p.Entity);modelBuilder.Entity<MainModuleState>().HasQueryFilter(e => _authenticatedUser.Entity == "DEFAULT" || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<ParentModuleState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<ParentModuleState>().HasIndex(p => p.Entity);modelBuilder.Entity<ParentModuleState>().HasQueryFilter(e => _authenticatedUser.Entity == "DEFAULT" || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<SubDetailItemState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<SubDetailItemState>().HasIndex(p => p.Entity);modelBuilder.Entity<SubDetailItemState>().HasQueryFilter(e => _authenticatedUser.Entity == "DEFAULT" || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<SubDetailListState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<SubDetailListState>().HasIndex(p => p.Entity);modelBuilder.Entity<SubDetailListState>().HasQueryFilter(e => _authenticatedUser.Entity == "DEFAULT" || e.Entity == _authenticatedUser.Entity);
		
        modelBuilder.Entity<ParentModuleState>().HasIndex(p => p.Name).IsUnique();
		
        modelBuilder.Entity<MainModuleState>().Property(e => e.Code).HasMaxLength(255);
		modelBuilder.Entity<ParentModuleState>().Property(e => e.Name).HasMaxLength(255);
		modelBuilder.Entity<SubDetailItemState>().Property(e => e.Code).HasMaxLength(255);
		modelBuilder.Entity<SubDetailListState>().Property(e => e.Code).HasMaxLength(255);
		
        modelBuilder.Entity<ParentModuleState>().HasMany(t => t.MainModuleList).WithOne(l => l.ParentModule).HasForeignKey(t => t.ParentModuleId);
		modelBuilder.Entity<MainModuleState>().HasMany(t => t.SubDetailItemList).WithOne(l => l.MainModule).HasForeignKey(t => t.TestForeignKeyTwo);
		modelBuilder.Entity<MainModuleState>().HasMany(t => t.SubDetailListList).WithOne(l => l.MainModule).HasForeignKey(t => t.TestForeignKeyOne);
		
		modelBuilder.Entity<ApprovalRecordState>().HasQueryFilter(e => _authenticatedUser.Entity == "DEFAULT" || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<ApprovalState>().HasQueryFilter(e => _authenticatedUser.Entity == "DEFAULT" || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<ApproverSetupState>().HasQueryFilter(e => _authenticatedUser.Entity == "DEFAULT" || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<ApproverAssignmentState>().HasQueryFilter(e => _authenticatedUser.Entity == "DEFAULT" || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<ApprovalRecordState>().HasIndex(l => l.DataId);
		modelBuilder.Entity<ApprovalRecordState>().Property(e => e.DataId).HasMaxLength(450);
		modelBuilder.Entity<ApprovalRecordState>().Property(e => e.ApproverSetupId).HasMaxLength(450);
		modelBuilder.Entity<ApprovalRecordState>().HasIndex(l => l.ApproverSetupId);
		modelBuilder.Entity<ApprovalRecordState>().HasIndex(l => l.Status);
		modelBuilder.Entity<ApprovalRecordState>().Property(e => e.Status).HasMaxLength(450);
		modelBuilder.Entity<ApprovalState>().HasIndex(l => l.ApproverUserId);
		modelBuilder.Entity<ApprovalState>().HasIndex(l => l.Status);
		modelBuilder.Entity<ApprovalState>().HasIndex(l => l.EmailSendingStatus);
		modelBuilder.Entity<ApprovalState>().Property(e => e.ApproverUserId).HasMaxLength(450);
		modelBuilder.Entity<ApprovalState>().Property(e => e.Status).HasMaxLength(450);
		modelBuilder.Entity<ApprovalState>().Property(e => e.EmailSendingStatus).HasMaxLength(450);
		modelBuilder.Entity<ApproverSetupState>().Property(e => e.TableName).HasMaxLength(450);
		modelBuilder.Entity<ApproverSetupState>().Property(e => e.ApprovalType).HasMaxLength(450);
		modelBuilder.Entity<ApproverSetupState>().Property(e => e.EmailSubject).HasMaxLength(450);
		modelBuilder.Entity<ApproverSetupState>().Property(e => e.WorkflowName).HasMaxLength(450);
		modelBuilder.Entity<ApproverSetupState>().HasIndex(e => new { e.WorkflowName, e.ApprovalSetupType, e.TableName, e.Entity }).IsUnique();
		modelBuilder.Entity<ApproverAssignmentState>().Property(e => e.ApproverUserId).HasMaxLength(450);
		modelBuilder.Entity<ApproverAssignmentState>().Property(e => e.ApproverRoleId).HasMaxLength(450);
		modelBuilder.Entity<ApproverAssignmentState>().HasIndex(e => new { e.ApproverSetupId, e.ApproverUserId, e.ApproverRoleId }).IsUnique();
        base.OnModelCreating(modelBuilder);
    }
}
