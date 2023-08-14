using CTI.Common.Data;
using CTI.Common.Identity.Abstractions;
using CTI.DSF.Core.DSF;
using Microsoft.EntityFrameworkCore;

namespace CTI.DSF.Infrastructure.Data;

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
	
    public DbSet<TaskListState> TaskList { get; set; } = default!;
	public DbSet<AssignmentState> Assignment { get; set; } = default!;
    public DbSet<DeliveryState> Delivery { get; set; } = default!;

    public DbSet<CompanyState> Company { get; set; } = default!;
    public DbSet<DepartmentState> Department { get; set; } = default!;
    public DbSet<SectionState> Section { get; set; } = default!;
    public DbSet<TeamState> Team { get; set; } = default!;

    public DbSet<ApprovalState> Approval { get; set; } = default!;
	public DbSet<ApproverSetupState> ApproverSetup { get; set; } = default!;
	public DbSet<ApproverAssignmentState> ApproverAssignment { get; set; } = default!;
	public DbSet<ApprovalRecordState> ApprovalRecord { get; set; } = default!;
    public DbSet<HolidayState> Holiday { get; set; } = default!;
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
        modelBuilder.Entity<TaskListState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<TaskListState>().HasIndex(p => p.Entity);
        modelBuilder.Entity<TaskListState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<AssignmentState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<AssignmentState>().HasIndex(p => p.Entity);
        modelBuilder.Entity<AssignmentState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
        modelBuilder.Entity<DeliveryState>().HasIndex(p => p.LastModifiedDate); modelBuilder.Entity<DeliveryState>().HasIndex(p => p.Entity); 
        modelBuilder.Entity<DeliveryState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
        modelBuilder.Entity<HolidayState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);

        modelBuilder.Entity<CompanyState>().HasIndex(p => p.LastModifiedDate); modelBuilder.Entity<CompanyState>().HasIndex(p => p.Entity); modelBuilder.Entity<CompanyState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
        modelBuilder.Entity<DepartmentState>().HasIndex(p => p.LastModifiedDate); modelBuilder.Entity<DepartmentState>().HasIndex(p => p.Entity); modelBuilder.Entity<DepartmentState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
        modelBuilder.Entity<SectionState>().HasIndex(p => p.LastModifiedDate); modelBuilder.Entity<SectionState>().HasIndex(p => p.Entity); modelBuilder.Entity<SectionState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
        modelBuilder.Entity<TeamState>().HasIndex(p => p.LastModifiedDate); modelBuilder.Entity<TeamState>().HasIndex(p => p.Entity); modelBuilder.Entity<TeamState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);

        modelBuilder.Entity<TaskListState>().HasIndex(p => p.TaskListCode).IsUnique();
		modelBuilder.Entity<AssignmentState>().HasIndex(p => p.AssignmentCode).IsUnique();
        modelBuilder.Entity<DeliveryState>().HasIndex(p => p.DeliveryCode).IsUnique();

        modelBuilder.Entity<CompanyState>().HasIndex(p => p.CompanyCode).IsUnique();
        modelBuilder.Entity<CompanyState>().HasIndex(p => p.CompanyName).IsUnique();
        modelBuilder.Entity<DepartmentState>().HasIndex(p => p.DepartmentCode).IsUnique();
        modelBuilder.Entity<SectionState>().HasIndex(p => p.SectionCode).IsUnique();
        modelBuilder.Entity<TeamState>().HasIndex(p => p.TeamCode).IsUnique();
        modelBuilder.Entity<HolidayState>().HasIndex(p => p.HolidayName).IsUnique();

        modelBuilder.Entity<TaskListState>().Property(e => e.TaskDescription).HasMaxLength(255);
		
        modelBuilder.Entity<TaskListState>().HasMany(t => t.AssignmentList).WithOne(l => l.TaskList).HasForeignKey(t => t.TaskListId);
		
		modelBuilder.Entity<AssignmentState>().HasMany(t => t.DeliveryList).WithOne(l => l.Assignment).HasForeignKey(t => t.AssignmentId);

        modelBuilder.Entity<CompanyState>().HasMany(t => t.DepartmentList).WithOne(l => l.Company).HasForeignKey(t => t.CompanyCode);
        modelBuilder.Entity<DepartmentState>().HasMany(t => t.SectionList).WithOne(l => l.Department).HasForeignKey(t => t.DepartmentCode);
        modelBuilder.Entity<SectionState>().HasMany(t => t.TeamList).WithOne(l => l.Section).HasForeignKey(t => t.SectionCode);

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
        modelBuilder.Entity<HolidayState>().Property(e => e.HolidayName).HasMaxLength(255);
        base.OnModelCreating(modelBuilder);
    }
}
