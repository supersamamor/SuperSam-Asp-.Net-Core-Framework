using CTI.Common.Data;
using CTI.Common.Identity.Abstractions;
using CTI.DPI.Core.DPI;
using Microsoft.EntityFrameworkCore;

namespace CTI.DPI.Infrastructure.Data;

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
        modelBuilder.Entity<ReportState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<ReportState>().HasIndex(p => p.Entity);modelBuilder.Entity<ReportState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<ReportTableState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<ReportTableState>().HasIndex(p => p.Entity);modelBuilder.Entity<ReportTableState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<ReportTableJoinParameterState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<ReportTableJoinParameterState>().HasIndex(p => p.Entity);modelBuilder.Entity<ReportTableJoinParameterState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<ReportColumnHeaderState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<ReportColumnHeaderState>().HasIndex(p => p.Entity);modelBuilder.Entity<ReportColumnHeaderState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<ReportColumnDetailState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<ReportColumnDetailState>().HasIndex(p => p.Entity);modelBuilder.Entity<ReportColumnDetailState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<ReportFilterGroupingState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<ReportFilterGroupingState>().HasIndex(p => p.Entity);modelBuilder.Entity<ReportFilterGroupingState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<ReportColumnFilterState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<ReportColumnFilterState>().HasIndex(p => p.Entity);modelBuilder.Entity<ReportColumnFilterState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<ReportQueryFilterState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<ReportQueryFilterState>().HasIndex(p => p.Entity);modelBuilder.Entity<ReportQueryFilterState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		
        modelBuilder.Entity<ReportState>().HasIndex(p => p.ReportName).IsUnique();
		
        modelBuilder.Entity<ReportState>().Property(e => e.ReportName).HasMaxLength(255);
		modelBuilder.Entity<ReportState>().Property(e => e.QueryType).HasMaxLength(50);
		modelBuilder.Entity<ReportState>().Property(e => e.ReportOrChartType).HasMaxLength(50);
		modelBuilder.Entity<ReportState>().Property(e => e.QueryString).HasMaxLength(8000);
		modelBuilder.Entity<ReportTableState>().Property(e => e.TableName).HasMaxLength(255);
		modelBuilder.Entity<ReportTableState>().Property(e => e.Alias).HasMaxLength(255);
		modelBuilder.Entity<ReportTableState>().Property(e => e.JoinType).HasMaxLength(50);
		modelBuilder.Entity<ReportTableJoinParameterState>().Property(e => e.LogicalOperator).HasMaxLength(50);
		modelBuilder.Entity<ReportTableJoinParameterState>().Property(e => e.FieldName).HasMaxLength(255);
		modelBuilder.Entity<ReportTableJoinParameterState>().Property(e => e.JoinFromFieldName).HasMaxLength(255);
		modelBuilder.Entity<ReportColumnHeaderState>().Property(e => e.Alias).HasMaxLength(255);
		modelBuilder.Entity<ReportColumnHeaderState>().Property(e => e.AggregationOperator).HasMaxLength(50);
		modelBuilder.Entity<ReportColumnDetailState>().Property(e => e.FieldName).HasMaxLength(255);
		modelBuilder.Entity<ReportColumnDetailState>().Property(e => e.Function).HasMaxLength(50);
		modelBuilder.Entity<ReportColumnDetailState>().Property(e => e.ArithmeticOperator).HasMaxLength(50);
		modelBuilder.Entity<ReportFilterGroupingState>().Property(e => e.LogicalOperator).HasMaxLength(50);
		modelBuilder.Entity<ReportColumnFilterState>().Property(e => e.LogicalOperator).HasMaxLength(50);
		modelBuilder.Entity<ReportColumnFilterState>().Property(e => e.FieldName).HasMaxLength(255);
		modelBuilder.Entity<ReportColumnFilterState>().Property(e => e.ComparisonOperator).HasMaxLength(50);
		modelBuilder.Entity<ReportQueryFilterState>().Property(e => e.FieldName).HasMaxLength(255);	
		
        modelBuilder.Entity<ReportState>().HasMany(t => t.ReportTableList).WithOne(l => l.Report).HasForeignKey(t => t.ReportId);
		modelBuilder.Entity<ReportState>().HasMany(t => t.ReportTableJoinParameterList).WithOne(l => l.Report).HasForeignKey(t => t.ReportId);
		modelBuilder.Entity<ReportTableState>().HasMany(t => t.ReportTableJoinParameterList).WithOne(l => l.ReportTable).HasForeignKey(t => t.TableId);
		modelBuilder.Entity<ReportState>().HasMany(t => t.ReportColumnHeaderList).WithOne(l => l.Report).HasForeignKey(t => t.ReportId);
		modelBuilder.Entity<ReportColumnHeaderState>().HasMany(t => t.ReportColumnDetailList).WithOne(l => l.ReportColumnHeader).HasForeignKey(t => t.ReportColumnId);
		modelBuilder.Entity<ReportTableState>().HasMany(t => t.ReportColumnDetailList).WithOne(l => l.ReportTable).HasForeignKey(t => t.TableId);
		modelBuilder.Entity<ReportState>().HasMany(t => t.ReportFilterGroupingList).WithOne(l => l.Report).HasForeignKey(t => t.ReportId);
		modelBuilder.Entity<ReportFilterGroupingState>().HasMany(t => t.ReportColumnFilterList).WithOne(l => l.ReportFilterGrouping).HasForeignKey(t => t.ReportFilterGroupingId);
		modelBuilder.Entity<ReportTableState>().HasMany(t => t.ReportColumnFilterList).WithOne(l => l.ReportTable).HasForeignKey(t => t.TableId);
		modelBuilder.Entity<ReportState>().HasMany(t => t.ReportQueryFilterList).WithOne(l => l.Report).HasForeignKey(t => t.ReportId);
		
		modelBuilder.Entity<ApprovalRecordState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<ApprovalState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<ApproverSetupState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<ApproverAssignmentState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
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
