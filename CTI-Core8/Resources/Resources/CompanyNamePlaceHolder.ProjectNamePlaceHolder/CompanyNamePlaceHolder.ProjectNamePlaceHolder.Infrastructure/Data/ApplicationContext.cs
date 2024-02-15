using CompanyNamePlaceHolder.Common.Data;
using CompanyNamePlaceHolder.Common.Identity.Abstractions;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder;
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
    public DbSet<ReportState> Report { get; set; } = default!;  
    public DbSet<ReportQueryFilterState> ReportQueryFilter { get; set; } = default!;
    public DbSet<ReportRoleAssignmentState> ReportRoleAssignment { get; set; } = default!;
	public DbSet<UploadProcessorState> UploadProcessor { get; set; } = default!;
    public DbSet<UploadStagingState> UploadStaging { get; set; } = default!;
	
    Template:[InsertNewDataModelContextPropertyTextHere]
	Template:[ApprovalContext]
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
                                               || p.Name.Equals("LastModifiedDate", StringComparison.OrdinalIgnoreCase)
                                               || p.Name.Equals("Id", StringComparison.OrdinalIgnoreCase)))
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
		modelBuilder.Entity<Audit>().HasIndex(p => p.TraceId);
        modelBuilder.Entity<Audit>().HasIndex(p => p.DateTime);
		modelBuilder.Entity<UploadProcessorState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
        modelBuilder.Entity<UploadStagingState>().HasQueryFilter(e => _authenticatedUser.Entity == Core.Constants.Entities.Default.ToUpper() || e.Entity == _authenticatedUser.Entity);
        // NOTE: DO NOT CREATE EXTENSION METHOD FOR QUERY FILTER!!!
        // It causes filter to be evaluated before user has signed in
        Template:[InsertNewEFFluentAttributesTextHere]
        Template:[InsertNewEFFluentAttributesUniqueTextHere]
        Template:[InsertNewEFFluentAttributesStringLengthTextHere]
        Template:[InsertNewSubCollectionFluentApiTextHere]
		Template:[ApprovalFluentEF]
		modelBuilder.Entity<UploadProcessorState>().Property(e => e.FileType).HasMaxLength(20);
        modelBuilder.Entity<UploadProcessorState>().Property(e => e.Path).HasMaxLength(450);
        modelBuilder.Entity<UploadProcessorState>().Property(e => e.Status).HasMaxLength(20);
        modelBuilder.Entity<UploadProcessorState>().Property(e => e.Module).HasMaxLength(50);
        modelBuilder.Entity<UploadProcessorState>().Property(e => e.UploadType).HasMaxLength(50);
        modelBuilder.Entity<UploadStagingState>().Property(e => e.Status).HasMaxLength(50);

        modelBuilder.Entity<UploadProcessorState>().HasMany(t => t.UploadStagingList).WithOne(l => l.UploadProcessor).HasForeignKey(t => t.UploadProcessorId);
        base.OnModelCreating(modelBuilder);
    }
}
