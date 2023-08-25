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
	
    public DbSet<CompanyState> Company { get; set; } = default!;
	public DbSet<DepartmentState> Department { get; set; } = default!;
	public DbSet<SectionState> Section { get; set; } = default!;
	public DbSet<TeamState> Team { get; set; } = default!;
	public DbSet<HolidayState> Holiday { get; set; } = default!;
	public DbSet<TagsState> Tags { get; set; } = default!;
	public DbSet<TaskListState> TaskList { get; set; } = default!;
	public DbSet<TaskApproverState> TaskApprover { get; set; } = default!;
	public DbSet<TaskTagState> TaskTag { get; set; } = default!;
	public DbSet<AssignmentState> Assignment { get; set; } = default!;
	public DbSet<DeliveryState> Delivery { get; set; } = default!;
	
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
		foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties()
                                               .Where(p => p.Name.Equals("CreatedBy", StringComparison.OrdinalIgnoreCase)
                                               || p.Name.Equals("LastModifiedBy", StringComparison.OrdinalIgnoreCase)
                                               || p.Name.Equals("Entity", StringComparison.OrdinalIgnoreCase)
                                               || p.Name.Equals("LastModifiedDate", StringComparison.OrdinalIgnoreCase)))
            {
                if (!property.Name.Equals("LastModifiedDate", StringComparison.OrdinalIgnoreCase))
                {
                    property.SetMaxLength(36);
                }
                entityType.AddIndex(property);
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
        modelBuilder.Entity<CompanyState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<DepartmentState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<SectionState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<TeamState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<HolidayState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<TagsState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<TaskListState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<TaskApproverState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<TaskTagState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<AssignmentState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<DeliveryState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		
        modelBuilder.Entity<CompanyState>().HasIndex(p => p.CompanyCode).IsUnique();
		modelBuilder.Entity<CompanyState>().HasIndex(p => p.CompanyName).IsUnique();
		modelBuilder.Entity<HolidayState>().HasIndex(p => p.HolidayName).IsUnique();
		modelBuilder.Entity<TagsState>().HasIndex(p => p.Name).IsUnique();
		
        modelBuilder.Entity<CompanyState>().Property(e => e.CompanyCode).HasMaxLength(450);
		modelBuilder.Entity<CompanyState>().Property(e => e.CompanyName).HasMaxLength(450);
		modelBuilder.Entity<DepartmentState>().Property(e => e.DepartmentCode).HasMaxLength(450);
		modelBuilder.Entity<SectionState>().Property(e => e.SectionCode).HasMaxLength(450);
		modelBuilder.Entity<TeamState>().Property(e => e.TeamCode).HasMaxLength(450);
		modelBuilder.Entity<HolidayState>().Property(e => e.HolidayName).HasMaxLength(255);
		modelBuilder.Entity<TagsState>().Property(e => e.Name).HasMaxLength(255);
		modelBuilder.Entity<TaskListState>().Property(e => e.TaskDescription).HasMaxLength(255);
		modelBuilder.Entity<TaskApproverState>().Property(e => e.ApproverUserId).HasMaxLength(36);
		modelBuilder.Entity<AssignmentState>().Property(e => e.AssignmentCode).HasMaxLength(450);
		modelBuilder.Entity<AssignmentState>().Property(e => e.PrimaryAssignee).HasMaxLength(36);
		modelBuilder.Entity<AssignmentState>().Property(e => e.AlternateAssignee).HasMaxLength(36);
		modelBuilder.Entity<DeliveryState>().Property(e => e.DeliveryCode).HasMaxLength(450);
		
        modelBuilder.Entity<CompanyState>().HasMany(t => t.DepartmentList).WithOne(l => l.Company).HasForeignKey(t => t.CompanyCode);
		modelBuilder.Entity<DepartmentState>().HasMany(t => t.SectionList).WithOne(l => l.Department).HasForeignKey(t => t.DepartmentCode);
		modelBuilder.Entity<SectionState>().HasMany(t => t.TeamList).WithOne(l => l.Section).HasForeignKey(t => t.SectionCode);
		modelBuilder.Entity<CompanyState>().HasMany(t => t.TaskListList).WithOne(l => l.Company).HasForeignKey(t => t.CompanyId);
		modelBuilder.Entity<DepartmentState>().HasMany(t => t.TaskListList).WithOne(l => l.Department).HasForeignKey(t => t.DepartmentId);
		modelBuilder.Entity<SectionState>().HasMany(t => t.TaskListList).WithOne(l => l.Section).HasForeignKey(t => t.SectionId);
		modelBuilder.Entity<TeamState>().HasMany(t => t.TaskListList).WithOne(l => l.Team).HasForeignKey(t => t.TeamId);
		modelBuilder.Entity<TaskListState>().HasMany(t => t.TaskApproverList).WithOne(l => l.TaskList).HasForeignKey(t => t.TaskListId);
		modelBuilder.Entity<TaskListState>().HasMany(t => t.TaskTagList).WithOne(l => l.TaskList).HasForeignKey(t => t.TaskListId);
		modelBuilder.Entity<TagsState>().HasMany(t => t.TaskTagList).WithOne(l => l.Tags).HasForeignKey(t => t.TagId);
		modelBuilder.Entity<TaskListState>().HasMany(t => t.AssignmentList).WithOne(l => l.TaskList).HasForeignKey(t => t.TaskListId);
		modelBuilder.Entity<AssignmentState>().HasMany(t => t.DeliveryList).WithOne(l => l.Assignment).HasForeignKey(t => t.AssignmentId);
		
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
