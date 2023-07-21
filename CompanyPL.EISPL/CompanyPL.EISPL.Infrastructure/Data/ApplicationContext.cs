using CompanyPL.Common.Data;
using CompanyPL.Common.Identity.Abstractions;
using CompanyPL.EISPL.Core.EISPL;
using Microsoft.EntityFrameworkCore;

namespace CompanyPL.EISPL.Infrastructure.Data;

public class ApplicationContext : AuditableDbContext<ApplicationContext>
{
    private readonly IAuthenticatedUser _authenticatedUser;

    public ApplicationContext(DbContextOptions<ApplicationContext> options,
                              IAuthenticatedUser authenticatedUser) : base(options, authenticatedUser)
    {
        _authenticatedUser = authenticatedUser;
    }

    public DbSet<PLEmployeeState> PLEmployee { get; set; } = default!;
	public DbSet<PLContactInformationState> PLContactInformation { get; set; } = default!;
	public DbSet<PLHealthDeclarationState> PLHealthDeclaration { get; set; } = default!;
	public DbSet<TestState> Test { get; set; } = default!;
	
	
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
        modelBuilder.Entity<PLEmployeeState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<PLEmployeeState>().HasIndex(p => p.Entity);modelBuilder.Entity<PLEmployeeState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<PLContactInformationState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<PLContactInformationState>().HasIndex(p => p.Entity);modelBuilder.Entity<PLContactInformationState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<PLHealthDeclarationState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<PLHealthDeclarationState>().HasIndex(p => p.Entity);modelBuilder.Entity<PLHealthDeclarationState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		modelBuilder.Entity<TestState>().HasIndex(p => p.LastModifiedDate);modelBuilder.Entity<TestState>().HasIndex(p => p.Entity);modelBuilder.Entity<TestState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
		
        modelBuilder.Entity<PLEmployeeState>().HasIndex(p => p.PLEmployeeCode).IsUnique();
		
        modelBuilder.Entity<PLEmployeeState>().Property(e => e.PLFirstName).HasMaxLength(255);
		modelBuilder.Entity<PLEmployeeState>().Property(e => e.PLMiddleName).HasMaxLength(255);
		modelBuilder.Entity<PLEmployeeState>().Property(e => e.PLEmployeeCode).HasMaxLength(255);
		modelBuilder.Entity<PLEmployeeState>().Property(e => e.PLLastName).HasMaxLength(255);
		modelBuilder.Entity<PLContactInformationState>().Property(e => e.PLContactDetails).HasMaxLength(255);
		modelBuilder.Entity<PLHealthDeclarationState>().Property(e => e.PLVaccine).HasMaxLength(255);
		
        modelBuilder.Entity<PLEmployeeState>().HasMany(t => t.PLContactInformationList).WithOne(l => l.PLEmployee).HasForeignKey(t => t.PLEmployeeId);
		modelBuilder.Entity<PLEmployeeState>().HasMany(t => t.PLHealthDeclarationList).WithOne(l => l.PLEmployee).HasForeignKey(t => t.PLEmployeeId);
		modelBuilder.Entity<PLEmployeeState>().HasMany(t => t.TestList).WithOne(l => l.PLEmployee).HasForeignKey(t => t.PLEmployeeId);
		
		
        base.OnModelCreating(modelBuilder);
    }
}
