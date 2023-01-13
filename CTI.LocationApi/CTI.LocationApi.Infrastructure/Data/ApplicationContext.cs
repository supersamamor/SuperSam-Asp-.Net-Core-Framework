using CTI.Common.Data;
using CTI.Common.Identity.Abstractions;
using CTI.LocationApi.Core.LocationApi;
using Microsoft.EntityFrameworkCore;

namespace CTI.LocationApi.Infrastructure.Data;

public class ApplicationContext : AuditableDbContext<ApplicationContext>
{
    private readonly IAuthenticatedUser _authenticatedUser;

    public ApplicationContext(DbContextOptions<ApplicationContext> options,
                              IAuthenticatedUser authenticatedUser) : base(options, authenticatedUser)
    {
        _authenticatedUser = authenticatedUser;
    }

    public DbSet<BarangayState> Barangay { get; set; } = default!;
	public DbSet<CityState> City { get; set; } = default!;
	public DbSet<LocationState> Location { get; set; } = default!;
	public DbSet<ProvinceState> Province { get; set; } = default!;
	public DbSet<RegionState> Region { get; set; } = default!;
	public DbSet<CountryState> Country { get; set; } = default!;
	
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
        modelBuilder.Entity<BarangayState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<BarangayState>().HasIndex(p => p.Entity);modelBuilder.Entity<BarangayState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<CityState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<CityState>().HasIndex(p => p.Entity);modelBuilder.Entity<CityState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<LocationState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<LocationState>().HasIndex(p => p.Entity);modelBuilder.Entity<LocationState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<ProvinceState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<ProvinceState>().HasIndex(p => p.Entity);modelBuilder.Entity<ProvinceState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<RegionState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<RegionState>().HasIndex(p => p.Entity);modelBuilder.Entity<RegionState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<CountryState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<CountryState>().HasIndex(p => p.Entity);modelBuilder.Entity<CountryState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		
        modelBuilder.Entity<BarangayState>().HasIndex(p => p.Code).IsUnique();
		modelBuilder.Entity<CityState>().HasIndex(p => p.Code).IsUnique();
		modelBuilder.Entity<ProvinceState>().HasIndex(p => p.Code).IsUnique();
		modelBuilder.Entity<RegionState>().HasIndex(p => p.Code).IsUnique();
		modelBuilder.Entity<CountryState>().HasIndex(p => p.Name).IsUnique();
		modelBuilder.Entity<CountryState>().HasIndex(p => p.Code).IsUnique();
		
        modelBuilder.Entity<BarangayState>().Property(e => e.Code).HasMaxLength(50);
		modelBuilder.Entity<BarangayState>().Property(e => e.Name).HasMaxLength(255);
		modelBuilder.Entity<CityState>().Property(e => e.Code).HasMaxLength(50);
		modelBuilder.Entity<CityState>().Property(e => e.Name).HasMaxLength(255);
		modelBuilder.Entity<LocationState>().Property(e => e.BarangayCode).HasMaxLength(50);
		modelBuilder.Entity<LocationState>().Property(e => e.Barangay).HasMaxLength(255);
		modelBuilder.Entity<LocationState>().Property(e => e.CityCode).HasMaxLength(50);
		modelBuilder.Entity<LocationState>().Property(e => e.City).HasMaxLength(255);
		modelBuilder.Entity<LocationState>().Property(e => e.ProvinceCode).HasMaxLength(50);
		modelBuilder.Entity<LocationState>().Property(e => e.Province).HasMaxLength(255);
		modelBuilder.Entity<LocationState>().Property(e => e.RegionCode).HasMaxLength(50);
		modelBuilder.Entity<LocationState>().Property(e => e.Region).HasMaxLength(255);
		modelBuilder.Entity<ProvinceState>().Property(e => e.Code).HasMaxLength(50);
		modelBuilder.Entity<ProvinceState>().Property(e => e.Name).HasMaxLength(255);
		modelBuilder.Entity<RegionState>().Property(e => e.Code).HasMaxLength(50);
		modelBuilder.Entity<RegionState>().Property(e => e.Name).HasMaxLength(255);
		modelBuilder.Entity<CountryState>().Property(e => e.Name).HasMaxLength(255);
		modelBuilder.Entity<CountryState>().Property(e => e.Code).HasMaxLength(50);
		
        modelBuilder.Entity<CityState>().HasMany(t => t.BarangayList).WithOne(l => l.City).HasForeignKey(t => t.CityId);
		modelBuilder.Entity<ProvinceState>().HasMany(t => t.CityList).WithOne(l => l.Province).HasForeignKey(t => t.ProvinceId);
		modelBuilder.Entity<RegionState>().HasMany(t => t.ProvinceList).WithOne(l => l.Region).HasForeignKey(t => t.RegionId);
		modelBuilder.Entity<CountryState>().HasMany(t => t.RegionList).WithOne(l => l.Country).HasForeignKey(t => t.CountryId);
		
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
