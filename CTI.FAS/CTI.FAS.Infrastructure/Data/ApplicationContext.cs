using CTI.Common.Data;
using CTI.Common.Identity.Abstractions;
using CTI.FAS.Core.FAS;
using Microsoft.EntityFrameworkCore;

namespace CTI.FAS.Infrastructure.Data;

public class ApplicationContext : AuditableDbContext<ApplicationContext>
{
    private readonly IAuthenticatedUser _authenticatedUser;

    public ApplicationContext(DbContextOptions<ApplicationContext> options,
                              IAuthenticatedUser authenticatedUser) : base(options, authenticatedUser)
    {
        _authenticatedUser = authenticatedUser;
    }

    public DbSet<DatabaseConnectionSetupState> DatabaseConnectionSetup { get; set; } = default!;
	public DbSet<CompanyState> Company { get; set; } = default!;
	public DbSet<ProjectState> Project { get; set; } = default!;
	public DbSet<TenantState> Tenant { get; set; } = default!;
	public DbSet<UserEntityState> UserEntity { get; set; } = default!;
	public DbSet<CheckReleaseOptionState> CheckReleaseOption { get; set; } = default!;
	public DbSet<BatchState> Batch { get; set; } = default!;
	public DbSet<GeneratedState> Generated { get; set; } = default!;
	public DbSet<CreditorState> Creditor { get; set; } = default!;
	public DbSet<CreditorEmailState> CreditorEmail { get; set; } = default!;
	
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
        modelBuilder.Entity<DatabaseConnectionSetupState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<DatabaseConnectionSetupState>().HasIndex(p => p.Entity);modelBuilder.Entity<DatabaseConnectionSetupState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<CompanyState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<CompanyState>().HasIndex(p => p.Entity);modelBuilder.Entity<CompanyState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<ProjectState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<ProjectState>().HasIndex(p => p.Entity);modelBuilder.Entity<ProjectState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<TenantState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<TenantState>().HasIndex(p => p.Entity);modelBuilder.Entity<TenantState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<UserEntityState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<UserEntityState>().HasIndex(p => p.Entity);modelBuilder.Entity<UserEntityState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<CheckReleaseOptionState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<CheckReleaseOptionState>().HasIndex(p => p.Entity);modelBuilder.Entity<CheckReleaseOptionState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<BatchState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<BatchState>().HasIndex(p => p.Entity);modelBuilder.Entity<BatchState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<GeneratedState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<GeneratedState>().HasIndex(p => p.Entity);modelBuilder.Entity<GeneratedState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<CreditorState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<CreditorState>().HasIndex(p => p.Entity);modelBuilder.Entity<CreditorState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<CreditorEmailState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<CreditorEmailState>().HasIndex(p => p.Entity);modelBuilder.Entity<CreditorEmailState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		
        modelBuilder.Entity<DatabaseConnectionSetupState>().HasIndex(p => p.Code).IsUnique();
		modelBuilder.Entity<DatabaseConnectionSetupState>().HasIndex(p => p.Name).IsUnique();
		
        modelBuilder.Entity<DatabaseConnectionSetupState>().Property(e => e.Code).HasMaxLength(20);
		modelBuilder.Entity<DatabaseConnectionSetupState>().Property(e => e.Name).HasMaxLength(100);
		modelBuilder.Entity<DatabaseConnectionSetupState>().Property(e => e.DatabaseAndServerName).HasMaxLength(100);
		modelBuilder.Entity<DatabaseConnectionSetupState>().Property(e => e.InhouseDatabaseAndServerName).HasMaxLength(100);
		modelBuilder.Entity<DatabaseConnectionSetupState>().Property(e => e.SystemConnectionString).HasMaxLength(1000);
		modelBuilder.Entity<CompanyState>().Property(e => e.Name).HasMaxLength(255);
		modelBuilder.Entity<CompanyState>().Property(e => e.Code).HasMaxLength(5);
		modelBuilder.Entity<CompanyState>().Property(e => e.EntityAddress).HasMaxLength(255);
		modelBuilder.Entity<CompanyState>().Property(e => e.EntityAddressSecondLine).HasMaxLength(255);
		modelBuilder.Entity<CompanyState>().Property(e => e.EntityDescription).HasMaxLength(100);
		modelBuilder.Entity<CompanyState>().Property(e => e.EntityShortName).HasMaxLength(20);
		modelBuilder.Entity<CompanyState>().Property(e => e.TINNo).HasMaxLength(17);
		modelBuilder.Entity<CompanyState>().Property(e => e.SubmitPlace).HasMaxLength(1000);
		modelBuilder.Entity<CompanyState>().Property(e => e.EmailTelephoneNumber).HasMaxLength(50);
		modelBuilder.Entity<CompanyState>().Property(e => e.Bank).HasMaxLength(255);
		modelBuilder.Entity<CompanyState>().Property(e => e.BankCode).HasMaxLength(12);
		modelBuilder.Entity<CompanyState>().Property(e => e.AccountName).HasMaxLength(255);
		modelBuilder.Entity<CompanyState>().Property(e => e.AccountType).HasMaxLength(30);
		modelBuilder.Entity<CompanyState>().Property(e => e.AccountNumber).HasMaxLength(14);
		modelBuilder.Entity<CompanyState>().Property(e => e.DeliveryMethod).HasMaxLength(3);
		modelBuilder.Entity<CompanyState>().Property(e => e.EWBDelMethod).HasMaxLength(3);
		modelBuilder.Entity<CompanyState>().Property(e => e.PaymentType).HasMaxLength(30);
		modelBuilder.Entity<ProjectState>().Property(e => e.Name).HasMaxLength(255);
		modelBuilder.Entity<ProjectState>().Property(e => e.Code).HasMaxLength(20);
		modelBuilder.Entity<ProjectState>().Property(e => e.ProjectAddress).HasMaxLength(255);
		modelBuilder.Entity<ProjectState>().Property(e => e.Location).HasMaxLength(100);
		modelBuilder.Entity<TenantState>().Property(e => e.Name).HasMaxLength(255);
		modelBuilder.Entity<TenantState>().Property(e => e.Code).HasMaxLength(255);
		modelBuilder.Entity<UserEntityState>().Property(e => e.PplusUserId).HasMaxLength(50);
		modelBuilder.Entity<CheckReleaseOptionState>().Property(e => e.Name).HasMaxLength(255);
		modelBuilder.Entity<GeneratedState>().Property(e => e.Name).HasMaxLength(100);
		modelBuilder.Entity<GeneratedState>().Property(e => e.DocumentNumber).HasMaxLength(15);
		modelBuilder.Entity<GeneratedState>().Property(e => e.CheckNumber).HasMaxLength(15);
		modelBuilder.Entity<GeneratedState>().Property(e => e.Release).HasMaxLength(1);
		modelBuilder.Entity<GeneratedState>().Property(e => e.PaymentType).HasMaxLength(9);
		modelBuilder.Entity<GeneratedState>().Property(e => e.TextFileName).HasMaxLength(50);
		modelBuilder.Entity<GeneratedState>().Property(e => e.PdfReport).HasMaxLength(50);
		modelBuilder.Entity<GeneratedState>().Property(e => e.Emailed).HasMaxLength(9);
		modelBuilder.Entity<GeneratedState>().Property(e => e.GroupCode).HasMaxLength(9);
		modelBuilder.Entity<GeneratedState>().Property(e => e.Status).HasMaxLength(9);
		modelBuilder.Entity<CreditorState>().Property(e => e.CreditorAccount).HasMaxLength(50);
		modelBuilder.Entity<CreditorState>().Property(e => e.AccountName).HasMaxLength(255);
		modelBuilder.Entity<CreditorState>().Property(e => e.AccountType).HasMaxLength(2);
		modelBuilder.Entity<CreditorState>().Property(e => e.AccountNumber).HasMaxLength(20);
		modelBuilder.Entity<CreditorState>().Property(e => e.PayeeAccountName).HasMaxLength(50);
		modelBuilder.Entity<CreditorState>().Property(e => e.PayeeAccountNumber).HasMaxLength(50);
		modelBuilder.Entity<CreditorState>().Property(e => e.Email).HasMaxLength(50);
		modelBuilder.Entity<CreditorState>().Property(e => e.PayeeAccountCode).HasMaxLength(50);
		modelBuilder.Entity<CreditorState>().Property(e => e.PayeeAccountTIN).HasMaxLength(20);
		modelBuilder.Entity<CreditorState>().Property(e => e.PayeeAccountAddress).HasMaxLength(60);
		modelBuilder.Entity<CreditorState>().Property(e => e.Status).HasMaxLength(30);
		modelBuilder.Entity<CreditorEmailState>().Property(e => e.Email).HasMaxLength(50);
		
        modelBuilder.Entity<DatabaseConnectionSetupState>().HasMany(t => t.CompanyList).WithOne(l => l.DatabaseConnectionSetup).HasForeignKey(t => t.DatabaseConnectionSetupId);
		modelBuilder.Entity<CompanyState>().HasMany(t => t.ProjectList).WithOne(l => l.Company).HasForeignKey(t => t.CompanyId);
		modelBuilder.Entity<ProjectState>().HasMany(t => t.TenantList).WithOne(l => l.Project).HasForeignKey(t => t.ProjectId);
		modelBuilder.Entity<CompanyState>().HasMany(t => t.UserEntityList).WithOne(l => l.Company).HasForeignKey(t => t.CompanyId);
		modelBuilder.Entity<CreditorState>().HasMany(t => t.CheckReleaseOptionList).WithOne(l => l.Creditor).HasForeignKey(t => t.CreditorId);
		modelBuilder.Entity<CompanyState>().HasMany(t => t.GeneratedList).WithOne(l => l.Company).HasForeignKey(t => t.CompanyId);
		modelBuilder.Entity<CreditorState>().HasMany(t => t.GeneratedList).WithOne(l => l.Creditor).HasForeignKey(t => t.CreditorId);
		modelBuilder.Entity<BatchState>().HasMany(t => t.GeneratedList).WithOne(l => l.Batch).HasForeignKey(t => t.BatchId);
		modelBuilder.Entity<CompanyState>().HasMany(t => t.CreditorList).WithOne(l => l.Company).HasForeignKey(t => t.CompanyId);
		modelBuilder.Entity<CreditorState>().HasMany(t => t.CreditorEmailList).WithOne(l => l.Creditor).HasForeignKey(t => t.CreditorId);
		
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
