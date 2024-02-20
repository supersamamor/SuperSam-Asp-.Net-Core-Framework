using CTI.Common.Data;
using CTI.Common.Identity.Abstractions;
using CTI.ContractManagement.Core.ContractManagement;
using Microsoft.EntityFrameworkCore;

namespace CTI.ContractManagement.Infrastructure.Data;

public class ApplicationContext : AuditableDbContext<ApplicationContext>
{
    private readonly IAuthenticatedUser _authenticatedUser;

    public ApplicationContext(DbContextOptions<ApplicationContext> options,
                              IAuthenticatedUser authenticatedUser) : base(options, authenticatedUser)
    {
        _authenticatedUser = authenticatedUser;
    }

    public DbSet<ApplicationConfigurationState> ApplicationConfiguration { get; set; } = default!;
	public DbSet<MilestoneStageState> MilestoneStage { get; set; } = default!;
	public DbSet<FrequencyState> Frequency { get; set; } = default!;
	public DbSet<PricingTypeState> PricingType { get; set; } = default!;
	public DbSet<ProjectCategoryState> ProjectCategory { get; set; } = default!;
	public DbSet<DeliverableState> Deliverable { get; set; } = default!;
	public DbSet<ClientState> Client { get; set; } = default!;
	public DbSet<ProjectState> Project { get; set; } = default!;
	public DbSet<ProjectDeliverableState> ProjectDeliverable { get; set; } = default!;
	public DbSet<ProjectMilestoneState> ProjectMilestone { get; set; } = default!;
	public DbSet<ProjectPackageState> ProjectPackage { get; set; } = default!;
	public DbSet<ProjectPackageAdditionalDeliverableState> ProjectPackageAdditionalDeliverable { get; set; } = default!;
	public DbSet<ProjectHistoryState> ProjectHistory { get; set; } = default!;
	public DbSet<ProjectDeliverableHistoryState> ProjectDeliverableHistory { get; set; } = default!;
	public DbSet<ProjectMilestoneHistoryState> ProjectMilestoneHistory { get; set; } = default!;
	public DbSet<ProjectPackageHistoryState> ProjectPackageHistory { get; set; } = default!;
	public DbSet<ProjectPackageAdditionalDeliverableHistoryState> ProjectPackageAdditionalDeliverableHistory { get; set; } = default!;
	
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
        modelBuilder.Entity<ApplicationConfigurationState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<ApplicationConfigurationState>().HasIndex(p => p.Entity);modelBuilder.Entity<ApplicationConfigurationState>().HasQueryFilter(e => _authenticatedUser.Entity == "DEFAULT" || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<MilestoneStageState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<MilestoneStageState>().HasIndex(p => p.Entity);modelBuilder.Entity<MilestoneStageState>().HasQueryFilter(e => _authenticatedUser.Entity == "DEFAULT" || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<FrequencyState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<FrequencyState>().HasIndex(p => p.Entity);modelBuilder.Entity<FrequencyState>().HasQueryFilter(e => _authenticatedUser.Entity == "DEFAULT" || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<PricingTypeState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<PricingTypeState>().HasIndex(p => p.Entity);modelBuilder.Entity<PricingTypeState>().HasQueryFilter(e => _authenticatedUser.Entity == "DEFAULT" || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<ProjectCategoryState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<ProjectCategoryState>().HasIndex(p => p.Entity);modelBuilder.Entity<ProjectCategoryState>().HasQueryFilter(e => _authenticatedUser.Entity == "DEFAULT" || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<DeliverableState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<DeliverableState>().HasIndex(p => p.Entity);modelBuilder.Entity<DeliverableState>().HasQueryFilter(e => _authenticatedUser.Entity == "DEFAULT" || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<ClientState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<ClientState>().HasIndex(p => p.Entity);modelBuilder.Entity<ClientState>().HasQueryFilter(e => _authenticatedUser.Entity == "DEFAULT" || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<ProjectState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<ProjectState>().HasIndex(p => p.Entity);modelBuilder.Entity<ProjectState>().HasQueryFilter(e => _authenticatedUser.Entity == "DEFAULT" || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<ProjectDeliverableState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<ProjectDeliverableState>().HasIndex(p => p.Entity);modelBuilder.Entity<ProjectDeliverableState>().HasQueryFilter(e => _authenticatedUser.Entity == "DEFAULT" || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<ProjectMilestoneState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<ProjectMilestoneState>().HasIndex(p => p.Entity);modelBuilder.Entity<ProjectMilestoneState>().HasQueryFilter(e => _authenticatedUser.Entity == "DEFAULT" || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<ProjectPackageState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<ProjectPackageState>().HasIndex(p => p.Entity);modelBuilder.Entity<ProjectPackageState>().HasQueryFilter(e => _authenticatedUser.Entity == "DEFAULT" || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<ProjectPackageAdditionalDeliverableState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<ProjectPackageAdditionalDeliverableState>().HasIndex(p => p.Entity);modelBuilder.Entity<ProjectPackageAdditionalDeliverableState>().HasQueryFilter(e => _authenticatedUser.Entity == "DEFAULT" || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<ProjectHistoryState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<ProjectHistoryState>().HasIndex(p => p.Entity);modelBuilder.Entity<ProjectHistoryState>().HasQueryFilter(e => _authenticatedUser.Entity == "DEFAULT" || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<ProjectDeliverableHistoryState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<ProjectDeliverableHistoryState>().HasIndex(p => p.Entity);modelBuilder.Entity<ProjectDeliverableHistoryState>().HasQueryFilter(e => _authenticatedUser.Entity == "DEFAULT" || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<ProjectMilestoneHistoryState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<ProjectMilestoneHistoryState>().HasIndex(p => p.Entity);modelBuilder.Entity<ProjectMilestoneHistoryState>().HasQueryFilter(e => _authenticatedUser.Entity == "DEFAULT" || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<ProjectPackageHistoryState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<ProjectPackageHistoryState>().HasIndex(p => p.Entity);modelBuilder.Entity<ProjectPackageHistoryState>().HasQueryFilter(e => _authenticatedUser.Entity == "DEFAULT" || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<ProjectPackageAdditionalDeliverableHistoryState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<ProjectPackageAdditionalDeliverableHistoryState>().HasIndex(p => p.Entity);modelBuilder.Entity<ProjectPackageAdditionalDeliverableHistoryState>().HasQueryFilter(e => _authenticatedUser.Entity == "DEFAULT" || e.Entity == _authenticatedUser.Entity);
		
        modelBuilder.Entity<MilestoneStageState>().HasIndex(p => p.Name).IsUnique();
		modelBuilder.Entity<FrequencyState>().HasIndex(p => p.Name).IsUnique();
		modelBuilder.Entity<PricingTypeState>().HasIndex(p => p.Name).IsUnique();
		modelBuilder.Entity<ProjectCategoryState>().HasIndex(p => p.Name).IsUnique();
		modelBuilder.Entity<DeliverableState>().HasIndex(p => p.Name).IsUnique();
		modelBuilder.Entity<ClientState>().HasIndex(p => p.ContactPersonName).IsUnique();
		modelBuilder.Entity<ClientState>().HasIndex(p => p.EmailAddress).IsUnique();
		modelBuilder.Entity<ProjectState>().HasIndex(p => p.DocumentCode).IsUnique();
		
        modelBuilder.Entity<ApplicationConfigurationState>().Property(e => e.AddressLineOne).HasMaxLength(400);
		modelBuilder.Entity<ApplicationConfigurationState>().Property(e => e.AddressLineTwo).HasMaxLength(400);
		modelBuilder.Entity<MilestoneStageState>().Property(e => e.Name).HasMaxLength(255);
		modelBuilder.Entity<FrequencyState>().Property(e => e.Name).HasMaxLength(255);
		modelBuilder.Entity<PricingTypeState>().Property(e => e.Name).HasMaxLength(255);
		modelBuilder.Entity<ProjectCategoryState>().Property(e => e.Name).HasMaxLength(255);
		modelBuilder.Entity<DeliverableState>().Property(e => e.Name).HasMaxLength(255);
		modelBuilder.Entity<ClientState>().Property(e => e.ContactPersonName).HasMaxLength(255);
		modelBuilder.Entity<ClientState>().Property(e => e.ContactPersonPosition).HasMaxLength(255);
		modelBuilder.Entity<ClientState>().Property(e => e.CompanyName).HasMaxLength(255);
		modelBuilder.Entity<ClientState>().Property(e => e.CompanyDescription).HasMaxLength(255);
		modelBuilder.Entity<ClientState>().Property(e => e.CompanyAddressLineOne).HasMaxLength(400);
		modelBuilder.Entity<ClientState>().Property(e => e.CompanyAddressLineTwo).HasMaxLength(400);
		modelBuilder.Entity<ClientState>().Property(e => e.EmailAddress).HasMaxLength(255);
		modelBuilder.Entity<ProjectState>().Property(e => e.ProjectName).HasMaxLength(255);
		modelBuilder.Entity<ProjectState>().Property(e => e.ProjectDescription).HasMaxLength(500);
		modelBuilder.Entity<ProjectState>().Property(e => e.ProjectGoals).HasMaxLength(500);
		modelBuilder.Entity<ProjectState>().Property(e => e.RevisionSummary).HasMaxLength(400);
		modelBuilder.Entity<ProjectState>().Property(e => e.DocumentCode).HasMaxLength(50);
		modelBuilder.Entity<ProjectHistoryState>().Property(e => e.ProjectName).HasMaxLength(255);
		modelBuilder.Entity<ProjectHistoryState>().Property(e => e.ProjectDescription).HasMaxLength(500);
		modelBuilder.Entity<ProjectHistoryState>().Property(e => e.ProjectGoals).HasMaxLength(500);
		modelBuilder.Entity<ProjectHistoryState>().Property(e => e.RevisionSummary).HasMaxLength(400);
		modelBuilder.Entity<ProjectHistoryState>().Property(e => e.DocumentCode).HasMaxLength(50);
		
        modelBuilder.Entity<ProjectCategoryState>().HasMany(t => t.DeliverableList).WithOne(l => l.ProjectCategory).HasForeignKey(t => t.ProjectCategoryId);
		modelBuilder.Entity<ClientState>().HasMany(t => t.ProjectList).WithOne(l => l.Client).HasForeignKey(t => t.ClientId);
		modelBuilder.Entity<PricingTypeState>().HasMany(t => t.ProjectList).WithOne(l => l.PricingType).HasForeignKey(t => t.PricingTypeId);
		modelBuilder.Entity<ProjectState>().HasMany(t => t.ProjectDeliverableList).WithOne(l => l.Project).HasForeignKey(t => t.ProjectId);
		modelBuilder.Entity<DeliverableState>().HasMany(t => t.ProjectDeliverableList).WithOne(l => l.Deliverable).HasForeignKey(t => t.DeliverableId);
		modelBuilder.Entity<ProjectState>().HasMany(t => t.ProjectMilestoneList).WithOne(l => l.Project).HasForeignKey(t => t.ProjectId);
		modelBuilder.Entity<MilestoneStageState>().HasMany(t => t.ProjectMilestoneList).WithOne(l => l.MilestoneStage).HasForeignKey(t => t.MilestoneStageId);
		modelBuilder.Entity<FrequencyState>().HasMany(t => t.ProjectMilestoneList).WithOne(l => l.Frequency).HasForeignKey(t => t.FrequencyId);
		modelBuilder.Entity<ProjectState>().HasMany(t => t.ProjectPackageList).WithOne(l => l.Project).HasForeignKey(t => t.ProjectId);
		modelBuilder.Entity<ProjectPackageState>().HasMany(t => t.ProjectPackageAdditionalDeliverableList).WithOne(l => l.ProjectPackage).HasForeignKey(t => t.ProjectPackageId);
		modelBuilder.Entity<DeliverableState>().HasMany(t => t.ProjectPackageAdditionalDeliverableList).WithOne(l => l.Deliverable).HasForeignKey(t => t.DeliverableId);
		modelBuilder.Entity<ProjectState>().HasMany(t => t.ProjectHistoryList).WithOne(l => l.Project).HasForeignKey(t => t.ProjectId);
		modelBuilder.Entity<ClientState>().HasMany(t => t.ProjectHistoryList).WithOne(l => l.Client).HasForeignKey(t => t.ClientId);
		modelBuilder.Entity<PricingTypeState>().HasMany(t => t.ProjectHistoryList).WithOne(l => l.PricingType).HasForeignKey(t => t.PricingTypeId);
		modelBuilder.Entity<ProjectHistoryState>().HasMany(t => t.ProjectDeliverableHistoryList).WithOne(l => l.ProjectHistory).HasForeignKey(t => t.ProjectHistoryId);
		modelBuilder.Entity<DeliverableState>().HasMany(t => t.ProjectDeliverableHistoryList).WithOne(l => l.Deliverable).HasForeignKey(t => t.DeliverableId);
		modelBuilder.Entity<ProjectHistoryState>().HasMany(t => t.ProjectMilestoneHistoryList).WithOne(l => l.ProjectHistory).HasForeignKey(t => t.ProjectHistoryId);
		modelBuilder.Entity<MilestoneStageState>().HasMany(t => t.ProjectMilestoneHistoryList).WithOne(l => l.MilestoneStage).HasForeignKey(t => t.MilestoneStageId);
		modelBuilder.Entity<FrequencyState>().HasMany(t => t.ProjectMilestoneHistoryList).WithOne(l => l.Frequency).HasForeignKey(t => t.FrequencyId);
		modelBuilder.Entity<ProjectHistoryState>().HasMany(t => t.ProjectPackageHistoryList).WithOne(l => l.ProjectHistory).HasForeignKey(t => t.ProjectHistoryId);
		modelBuilder.Entity<ProjectPackageHistoryState>().HasMany(t => t.ProjectPackageAdditionalDeliverableHistoryList).WithOne(l => l.ProjectPackageHistory).HasForeignKey(t => t.ProjectPackageHistoryId);
		modelBuilder.Entity<DeliverableState>().HasMany(t => t.ProjectPackageAdditionalDeliverableHistoryList).WithOne(l => l.Deliverable).HasForeignKey(t => t.DeliverableId);
		
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
