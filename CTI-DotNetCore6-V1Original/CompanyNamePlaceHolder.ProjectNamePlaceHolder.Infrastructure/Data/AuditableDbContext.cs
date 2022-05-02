using CTI.Common.Web.Utility.Identity;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core;
using Microsoft.EntityFrameworkCore;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;

public class AuditableDbContext : AuditableContext
{
    readonly IAuthenticatedUser _authenticatedUser;

    public AuditableDbContext(DbContextOptions<ApplicationContext> options,
                            IAuthenticatedUser authenticatedUser) : base(options)
    {
        _authenticatedUser = authenticatedUser;
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SetBaseFields(_authenticatedUser);
        return base.SaveChangesAsync(_authenticatedUser.UserId, _authenticatedUser.TraceId);
    }

    void SetBaseFields(IAuthenticatedUser authenticatedUser)
    {
        foreach (var entry in this.ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.Entity = authenticatedUser.Entity;
                    entry.Entity.CreatedDate = DateTime.UtcNow;
                    entry.Entity.CreatedBy = authenticatedUser.UserId;
                    entry.Entity.LastModifiedDate = DateTime.UtcNow;
                    entry.Entity.LastModifiedBy = authenticatedUser.UserId;
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedDate = DateTime.UtcNow;
                    entry.Entity.LastModifiedBy = authenticatedUser.UserId;
                    break;
            }
        }
    }
}
