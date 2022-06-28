using CompanyNamePlaceHolder.Common.Core.Base.Models;
using CompanyNamePlaceHolder.Common.Identity.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace CompanyNamePlaceHolder.Common.Data;

public class AuditableDbContext<T> : AuditableContext where T : DbContext
{
    readonly IAuthenticatedUser _authenticatedUser;

    public AuditableDbContext(DbContextOptions<T> options,
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
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
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
