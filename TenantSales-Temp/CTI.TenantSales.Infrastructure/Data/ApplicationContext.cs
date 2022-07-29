using CTI.Common.Data;
using CTI.Common.Identity.Abstractions;
using CTI.TenantSales.Core.TenantSales;
using Microsoft.EntityFrameworkCore;

namespace CTI.TenantSales.Infrastructure.Data;

public class ApplicationContext : AuditableDbContext<ApplicationContext>
{
    private readonly IAuthenticatedUser _authenticatedUser;

    public ApplicationContext(DbContextOptions<ApplicationContext> options,
                              IAuthenticatedUser authenticatedUser) : base(options, authenticatedUser)
    {
        _authenticatedUser = authenticatedUser;
    }

    public DbSet<DatabaseConnectionSetupState> DatabaseConnectionSetup { get; set; } = default!;
	public DbSet<BusinessUnitState> BusinessUnit { get; set; } = default!;
	public DbSet<RentalTypeState> RentalType { get; set; } = default!;
	public DbSet<CompanyState> Company { get; set; } = default!;
	public DbSet<ProjectState> Project { get; set; } = default!;
	public DbSet<ThemeState> Theme { get; set; } = default!;
	public DbSet<TenantState> Tenant { get; set; } = default!;
	public DbSet<TenantPOSSalesState> TenantPOSSales { get; set; } = default!;
	public DbSet<ClassificationState> Classification { get; set; } = default!;
	public DbSet<CategoryState> Category { get; set; } = default!;
	public DbSet<ProjectBusinessUnitState> ProjectBusinessUnit { get; set; } = default!;
	public DbSet<TenantLotState> TenantLot { get; set; } = default!;
	public DbSet<LevelState> Level { get; set; } = default!;
	public DbSet<SalesCategoryState> SalesCategory { get; set; } = default!;
	public DbSet<TenantContactState> TenantContact { get; set; } = default!;
	public DbSet<TenantPOSState> TenantPOS { get; set; } = default!;
	
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
        modelBuilder.Entity<DatabaseConnectionSetupState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<DatabaseConnectionSetupState>().HasIndex(p => p.Entity);modelBuilder.Entity<DatabaseConnectionSetupState>().HasQueryFilter(e => _authenticatedUser.Entity == "DEFAULT" || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<BusinessUnitState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<BusinessUnitState>().HasIndex(p => p.Entity);modelBuilder.Entity<BusinessUnitState>().HasQueryFilter(e => _authenticatedUser.Entity == "DEFAULT" || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<RentalTypeState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<RentalTypeState>().HasIndex(p => p.Entity);modelBuilder.Entity<RentalTypeState>().HasQueryFilter(e => _authenticatedUser.Entity == "DEFAULT" || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<CompanyState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<CompanyState>().HasIndex(p => p.Entity);modelBuilder.Entity<CompanyState>().HasQueryFilter(e => _authenticatedUser.Entity == "DEFAULT" || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<ProjectState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<ProjectState>().HasIndex(p => p.Entity);modelBuilder.Entity<ProjectState>().HasQueryFilter(e => _authenticatedUser.Entity == "DEFAULT" || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<ThemeState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<ThemeState>().HasIndex(p => p.Entity);modelBuilder.Entity<ThemeState>().HasQueryFilter(e => _authenticatedUser.Entity == "DEFAULT" || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<TenantState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<TenantState>().HasIndex(p => p.Entity);modelBuilder.Entity<TenantState>().HasQueryFilter(e => _authenticatedUser.Entity == "DEFAULT" || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<TenantPOSSalesState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<TenantPOSSalesState>().HasIndex(p => p.Entity);modelBuilder.Entity<TenantPOSSalesState>().HasQueryFilter(e => _authenticatedUser.Entity == "DEFAULT" || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<ClassificationState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<ClassificationState>().HasIndex(p => p.Entity);modelBuilder.Entity<ClassificationState>().HasQueryFilter(e => _authenticatedUser.Entity == "DEFAULT" || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<CategoryState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<CategoryState>().HasIndex(p => p.Entity);modelBuilder.Entity<CategoryState>().HasQueryFilter(e => _authenticatedUser.Entity == "DEFAULT" || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<ProjectBusinessUnitState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<ProjectBusinessUnitState>().HasIndex(p => p.Entity);modelBuilder.Entity<ProjectBusinessUnitState>().HasQueryFilter(e => _authenticatedUser.Entity == "DEFAULT" || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<TenantLotState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<TenantLotState>().HasIndex(p => p.Entity);modelBuilder.Entity<TenantLotState>().HasQueryFilter(e => _authenticatedUser.Entity == "DEFAULT" || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<LevelState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<LevelState>().HasIndex(p => p.Entity);modelBuilder.Entity<LevelState>().HasQueryFilter(e => _authenticatedUser.Entity == "DEFAULT" || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<SalesCategoryState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<SalesCategoryState>().HasIndex(p => p.Entity);modelBuilder.Entity<SalesCategoryState>().HasQueryFilter(e => _authenticatedUser.Entity == "DEFAULT" || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<TenantContactState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<TenantContactState>().HasIndex(p => p.Entity);modelBuilder.Entity<TenantContactState>().HasQueryFilter(e => _authenticatedUser.Entity == "DEFAULT" || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<TenantPOSState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<TenantPOSState>().HasIndex(p => p.Entity);modelBuilder.Entity<TenantPOSState>().HasQueryFilter(e => _authenticatedUser.Entity == "DEFAULT" || e.Entity == _authenticatedUser.Entity);
		
        modelBuilder.Entity<DatabaseConnectionSetupState>().HasIndex(p => p.Code).IsUnique();
		modelBuilder.Entity<DatabaseConnectionSetupState>().HasIndex(p => p.Name).IsUnique();
		modelBuilder.Entity<BusinessUnitState>().HasIndex(p => p.Name).IsUnique();
		modelBuilder.Entity<RentalTypeState>().HasIndex(p => p.Name).IsUnique();
		modelBuilder.Entity<ThemeState>().HasIndex(p => p.Name).IsUnique();
		modelBuilder.Entity<ThemeState>().HasIndex(p => p.Code).IsUnique();
		
        modelBuilder.Entity<DatabaseConnectionSetupState>().Property(e => e.Code).HasMaxLength(20);
		modelBuilder.Entity<DatabaseConnectionSetupState>().Property(e => e.Name).HasMaxLength(100);
		modelBuilder.Entity<DatabaseConnectionSetupState>().Property(e => e.DatabaseAndServerName).HasMaxLength(100);
		modelBuilder.Entity<DatabaseConnectionSetupState>().Property(e => e.InhouseDatabaseAndServerName).HasMaxLength(100);
		modelBuilder.Entity<DatabaseConnectionSetupState>().Property(e => e.SystemConnectionString).HasMaxLength(1000);
		modelBuilder.Entity<DatabaseConnectionSetupState>().Property(e => e.ExhibitThemeCodes).HasMaxLength(1000);
		modelBuilder.Entity<BusinessUnitState>().Property(e => e.Name).HasMaxLength(50);
		modelBuilder.Entity<RentalTypeState>().Property(e => e.Name).HasMaxLength(255);
		modelBuilder.Entity<CompanyState>().Property(e => e.Name).HasMaxLength(255);
		modelBuilder.Entity<CompanyState>().Property(e => e.Code).HasMaxLength(5);
		modelBuilder.Entity<CompanyState>().Property(e => e.EntityAddress).HasMaxLength(255);
		modelBuilder.Entity<CompanyState>().Property(e => e.EntityAddressSecondLine).HasMaxLength(255);
		modelBuilder.Entity<CompanyState>().Property(e => e.EntityDescription).HasMaxLength(100);
		modelBuilder.Entity<CompanyState>().Property(e => e.EntityShortName).HasMaxLength(20);
		modelBuilder.Entity<CompanyState>().Property(e => e.TINNo).HasMaxLength(17);
		modelBuilder.Entity<ProjectState>().Property(e => e.Name).HasMaxLength(255);
		modelBuilder.Entity<ProjectState>().Property(e => e.Code).HasMaxLength(20);
		modelBuilder.Entity<ProjectState>().Property(e => e.PayableTo).HasMaxLength(100);
		modelBuilder.Entity<ProjectState>().Property(e => e.ProjectAddress).HasMaxLength(255);
		modelBuilder.Entity<ProjectState>().Property(e => e.Location).HasMaxLength(100);
		modelBuilder.Entity<ProjectState>().Property(e => e.ProjectNameANSection).HasMaxLength(255);
		modelBuilder.Entity<ProjectState>().Property(e => e.SignatoryName).HasMaxLength(255);
		modelBuilder.Entity<ProjectState>().Property(e => e.SignatoryPosition).HasMaxLength(255);
		modelBuilder.Entity<ProjectState>().Property(e => e.ANSignatoryName).HasMaxLength(255);
		modelBuilder.Entity<ProjectState>().Property(e => e.ANSignatoryPosition).HasMaxLength(255);
		modelBuilder.Entity<ProjectState>().Property(e => e.ContractSignatory).HasMaxLength(255);
		modelBuilder.Entity<ProjectState>().Property(e => e.ContractSignatoryPosition).HasMaxLength(50);
		modelBuilder.Entity<ProjectState>().Property(e => e.ContractSignatoryProofOfIdentity).HasMaxLength(255);
		modelBuilder.Entity<ProjectState>().Property(e => e.ContractSignatoryWitness).HasMaxLength(255);
		modelBuilder.Entity<ProjectState>().Property(e => e.ContractSignatoryWitnessPosition).HasMaxLength(50);
		modelBuilder.Entity<ProjectState>().Property(e => e.ProjectGreetingsSection).HasMaxLength(255);
		modelBuilder.Entity<ProjectState>().Property(e => e.ProjectShortName).HasMaxLength(255);
		modelBuilder.Entity<ProjectState>().Property(e => e.SignatureUpper).HasMaxLength(255);
		modelBuilder.Entity<ProjectState>().Property(e => e.SignatureLower).HasMaxLength(255);
		modelBuilder.Entity<ProjectState>().Property(e => e.SalesUploadFolder).HasMaxLength(100);
		modelBuilder.Entity<ProjectState>().Property(e => e.CurrencyCode).HasMaxLength(50);
		modelBuilder.Entity<ThemeState>().Property(e => e.Name).HasMaxLength(40);
		modelBuilder.Entity<ThemeState>().Property(e => e.Code).HasMaxLength(15);
		modelBuilder.Entity<TenantState>().Property(e => e.Name).HasMaxLength(255);
		modelBuilder.Entity<TenantState>().Property(e => e.Code).HasMaxLength(255);
		modelBuilder.Entity<TenantState>().Property(e => e.FileCode).HasMaxLength(20);
		modelBuilder.Entity<TenantState>().Property(e => e.Folder).HasMaxLength(20);
		modelBuilder.Entity<TenantState>().Property(e => e.BranchContact).HasMaxLength(255);
		modelBuilder.Entity<TenantState>().Property(e => e.HeadOfficeContact).HasMaxLength(255);
		modelBuilder.Entity<TenantState>().Property(e => e.ITSupportContact).HasMaxLength(255);
		modelBuilder.Entity<TenantPOSSalesState>().Property(e => e.SalesCategory).HasMaxLength(20);
		modelBuilder.Entity<TenantPOSSalesState>().Property(e => e.FileName).HasMaxLength(100);
		modelBuilder.Entity<TenantPOSSalesState>().Property(e => e.ValidationRemarks).HasMaxLength(1000);
		modelBuilder.Entity<ClassificationState>().Property(e => e.Name).HasMaxLength(80);
		modelBuilder.Entity<ClassificationState>().Property(e => e.Code).HasMaxLength(15);
		modelBuilder.Entity<CategoryState>().Property(e => e.Name).HasMaxLength(80);
		modelBuilder.Entity<CategoryState>().Property(e => e.Code).HasMaxLength(15);
		modelBuilder.Entity<TenantLotState>().Property(e => e.LotNo).HasMaxLength(255);
		modelBuilder.Entity<LevelState>().Property(e => e.Name).HasMaxLength(255);
		modelBuilder.Entity<SalesCategoryState>().Property(e => e.Code).HasMaxLength(20);
		modelBuilder.Entity<SalesCategoryState>().Property(e => e.Name).HasMaxLength(100);
		modelBuilder.Entity<TenantContactState>().Property(e => e.Detail).HasMaxLength(50);
		modelBuilder.Entity<TenantPOSState>().Property(e => e.Code).HasMaxLength(255);
		modelBuilder.Entity<TenantPOSState>().Property(e => e.SerialNumber).HasMaxLength(255);
		
        modelBuilder.Entity<DatabaseConnectionSetupState>().HasMany(t => t.CompanyList).WithOne(l => l.DatabaseConnectionSetup).HasForeignKey(t => t.DatabaseConnectionSetupId);
		modelBuilder.Entity<CompanyState>().HasMany(t => t.ProjectList).WithOne(l => l.Company).HasForeignKey(t => t.CompanyId);
		modelBuilder.Entity<RentalTypeState>().HasMany(t => t.TenantList).WithOne(l => l.RentalType).HasForeignKey(t => t.RentalTypeId);
		modelBuilder.Entity<ProjectState>().HasMany(t => t.TenantList).WithOne(l => l.Project).HasForeignKey(t => t.ProjectId);
		modelBuilder.Entity<LevelState>().HasMany(t => t.TenantList).WithOne(l => l.Level).HasForeignKey(t => t.LevelId);
		modelBuilder.Entity<TenantPOSState>().HasMany(t => t.TenantPOSSalesList).WithOne(l => l.TenantPOS).HasForeignKey(t => t.TenantPOSId);
		modelBuilder.Entity<ThemeState>().HasMany(t => t.ClassificationList).WithOne(l => l.Theme).HasForeignKey(t => t.ThemeId);
		modelBuilder.Entity<ClassificationState>().HasMany(t => t.CategoryList).WithOne(l => l.Classification).HasForeignKey(t => t.ClassificationId);
		modelBuilder.Entity<BusinessUnitState>().HasMany(t => t.ProjectBusinessUnitList).WithOne(l => l.BusinessUnit).HasForeignKey(t => t.BusinessUnitId);
		modelBuilder.Entity<ProjectState>().HasMany(t => t.ProjectBusinessUnitList).WithOne(l => l.Project).HasForeignKey(t => t.ProjectId);
		modelBuilder.Entity<TenantState>().HasMany(t => t.TenantLotList).WithOne(l => l.Tenant).HasForeignKey(t => t.TenantId);
		modelBuilder.Entity<ProjectState>().HasMany(t => t.LevelList).WithOne(l => l.Project).HasForeignKey(t => t.ProjectId);
		modelBuilder.Entity<TenantState>().HasMany(t => t.SalesCategoryList).WithOne(l => l.Tenant).HasForeignKey(t => t.TenantId);
		modelBuilder.Entity<TenantState>().HasMany(t => t.TenantContactList).WithOne(l => l.Tenant).HasForeignKey(t => t.TenantId);
		modelBuilder.Entity<TenantState>().HasMany(t => t.TenantPOSList).WithOne(l => l.Tenant).HasForeignKey(t => t.TenantId);
		
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
		modelBuilder.Entity<ApproverSetupState>().HasIndex(e => new { e.TableName, e.Entity }).IsUnique();
		modelBuilder.Entity<ApproverAssignmentState>().Property(e => e.ApproverUserId).HasMaxLength(450);
		modelBuilder.Entity<ApproverAssignmentState>().HasIndex(e => new { e.ApproverSetupId, e.ApproverUserId }).IsUnique();
        base.OnModelCreating(modelBuilder);
    }
}
