using CTI.Common.Data;
using CTI.Common.Identity.Abstractions;
using CTI.SQLReportAutoSender.Core.SQLReportAutoSender;
using Microsoft.EntityFrameworkCore;

namespace CTI.SQLReportAutoSender.Infrastructure.Data;

public class ApplicationContext : AuditableDbContext<ApplicationContext>
{
    private readonly IAuthenticatedUser _authenticatedUser;

    public ApplicationContext(DbContextOptions<ApplicationContext> options,
                              IAuthenticatedUser authenticatedUser) : base(options, authenticatedUser)
    {
        _authenticatedUser = authenticatedUser;
    }

    public DbSet<ScheduleFrequencyState> ScheduleFrequency { get; set; } = default!;
	public DbSet<ScheduleParameterState> ScheduleParameter { get; set; } = default!;
	public DbSet<ScheduleFrequencyParameterSetupState> ScheduleFrequencyParameterSetup { get; set; } = default!;
	public DbSet<ReportState> Report { get; set; } = default!;
	public DbSet<ReportDetailState> ReportDetail { get; set; } = default!;
	public DbSet<MailSettingState> MailSetting { get; set; } = default!;
	public DbSet<MailRecipientState> MailRecipient { get; set; } = default!;
	public DbSet<ReportScheduleSettingState> ReportScheduleSetting { get; set; } = default!;
	public DbSet<CustomScheduleState> CustomSchedule { get; set; } = default!;
	public DbSet<ReportInboxState> ReportInbox { get; set; } = default!;
	
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
        modelBuilder.Entity<ScheduleFrequencyState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<ScheduleFrequencyState>().HasIndex(p => p.Entity);modelBuilder.Entity<ScheduleFrequencyState>().HasQueryFilter(e => _authenticatedUser.Entity == "DEFAULT" || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<ScheduleParameterState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<ScheduleParameterState>().HasIndex(p => p.Entity);modelBuilder.Entity<ScheduleParameterState>().HasQueryFilter(e => _authenticatedUser.Entity == "DEFAULT" || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<ScheduleFrequencyParameterSetupState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<ScheduleFrequencyParameterSetupState>().HasIndex(p => p.Entity);modelBuilder.Entity<ScheduleFrequencyParameterSetupState>().HasQueryFilter(e => _authenticatedUser.Entity == "DEFAULT" || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<ReportState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<ReportState>().HasIndex(p => p.Entity);modelBuilder.Entity<ReportState>().HasQueryFilter(e => _authenticatedUser.Entity == "DEFAULT" || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<ReportDetailState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<ReportDetailState>().HasIndex(p => p.Entity);modelBuilder.Entity<ReportDetailState>().HasQueryFilter(e => _authenticatedUser.Entity == "DEFAULT" || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<MailSettingState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<MailSettingState>().HasIndex(p => p.Entity);modelBuilder.Entity<MailSettingState>().HasQueryFilter(e => _authenticatedUser.Entity == "DEFAULT" || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<MailRecipientState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<MailRecipientState>().HasIndex(p => p.Entity);modelBuilder.Entity<MailRecipientState>().HasQueryFilter(e => _authenticatedUser.Entity == "DEFAULT" || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<ReportScheduleSettingState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<ReportScheduleSettingState>().HasIndex(p => p.Entity);modelBuilder.Entity<ReportScheduleSettingState>().HasQueryFilter(e => _authenticatedUser.Entity == "DEFAULT" || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<CustomScheduleState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<CustomScheduleState>().HasIndex(p => p.Entity);modelBuilder.Entity<CustomScheduleState>().HasQueryFilter(e => _authenticatedUser.Entity == "DEFAULT" || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<ReportInboxState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<ReportInboxState>().HasIndex(p => p.Entity);modelBuilder.Entity<ReportInboxState>().HasQueryFilter(e => _authenticatedUser.Entity == "DEFAULT" || e.Entity == _authenticatedUser.Entity);
		
        modelBuilder.Entity<ScheduleFrequencyState>().HasIndex(p => p.Description).IsUnique();
		modelBuilder.Entity<ScheduleParameterState>().HasIndex(p => p.Description).IsUnique();
		
        modelBuilder.Entity<ScheduleFrequencyState>().Property(e => e.Description).HasMaxLength(50);
		modelBuilder.Entity<ScheduleParameterState>().Property(e => e.Description).HasMaxLength(50);
		modelBuilder.Entity<ReportState>().Property(e => e.Description).HasMaxLength(255);
		modelBuilder.Entity<ReportState>().Property(e => e.LatestFileGeneratedPath).HasMaxLength(500);
		modelBuilder.Entity<ReportDetailState>().Property(e => e.Description).HasMaxLength(255);
		modelBuilder.Entity<ReportDetailState>().Property(e => e.ConnectionString).HasMaxLength(255);
		modelBuilder.Entity<ReportDetailState>().Property(e => e.QueryString).HasMaxLength(8000);
		modelBuilder.Entity<MailSettingState>().Property(e => e.Account).HasMaxLength(50);
		modelBuilder.Entity<MailSettingState>().Property(e => e.Password).HasMaxLength(255);
		modelBuilder.Entity<MailSettingState>().Property(e => e.Body).HasMaxLength(2500);
		modelBuilder.Entity<MailSettingState>().Property(e => e.Subject).HasMaxLength(50);
		modelBuilder.Entity<MailRecipientState>().Property(e => e.RecipientEmail).HasMaxLength(100);
		modelBuilder.Entity<ReportScheduleSettingState>().Property(e => e.Value).HasMaxLength(50);
		modelBuilder.Entity<ReportInboxState>().Property(e => e.Status).HasMaxLength(450);
		modelBuilder.Entity<ReportInboxState>().Property(e => e.Remarks).HasMaxLength(1000);
		
        modelBuilder.Entity<ScheduleFrequencyState>().HasMany(t => t.ScheduleFrequencyParameterSetupList).WithOne(l => l.ScheduleFrequency).HasForeignKey(t => t.ScheduleFrequencyId);
		modelBuilder.Entity<ScheduleParameterState>().HasMany(t => t.ScheduleFrequencyParameterSetupList).WithOne(l => l.ScheduleParameter).HasForeignKey(t => t.ScheduleParameterId);
		modelBuilder.Entity<ScheduleFrequencyState>().HasMany(t => t.ReportList).WithOne(l => l.ScheduleFrequency).HasForeignKey(t => t.ScheduleFrequencyId);
		modelBuilder.Entity<ReportState>().HasMany(t => t.ReportDetailList).WithOne(l => l.Report).HasForeignKey(t => t.ReportId);
		modelBuilder.Entity<ReportState>().HasMany(t => t.MailSettingList).WithOne(l => l.Report).HasForeignKey(t => t.ReportId);
		modelBuilder.Entity<ReportState>().HasMany(t => t.MailRecipientList).WithOne(l => l.Report).HasForeignKey(t => t.ReportId);
		modelBuilder.Entity<ReportState>().HasMany(t => t.ReportScheduleSettingList).WithOne(l => l.Report).HasForeignKey(t => t.ReportId);
		modelBuilder.Entity<ScheduleFrequencyState>().HasMany(t => t.ReportScheduleSettingList).WithOne(l => l.ScheduleFrequency).HasForeignKey(t => t.ScheduleFrequencyId);
		modelBuilder.Entity<ScheduleParameterState>().HasMany(t => t.ReportScheduleSettingList).WithOne(l => l.ScheduleParameter).HasForeignKey(t => t.ScheduleParameterId);
		modelBuilder.Entity<ReportState>().HasMany(t => t.CustomScheduleList).WithOne(l => l.Report).HasForeignKey(t => t.ReportId);
		modelBuilder.Entity<ReportState>().HasMany(t => t.ReportInboxList).WithOne(l => l.Report).HasForeignKey(t => t.ReportId);
		
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
