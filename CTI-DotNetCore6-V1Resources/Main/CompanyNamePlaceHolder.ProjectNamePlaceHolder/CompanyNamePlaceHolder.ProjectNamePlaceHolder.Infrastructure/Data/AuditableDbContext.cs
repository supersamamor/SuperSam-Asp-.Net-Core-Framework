using AspNetCoreHero.EntityFrameworkCore.AuditTrail;
using Microsoft.EntityFrameworkCore;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Common.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data
{
    public class AuditableDbContext : AuditableContext
    {
        readonly AuthenticatedUser _authenticatedUser;

        public AuditableDbContext(DbContextOptions<ApplicationContext> options,
                                AuthenticatedUser authenticatedUser) : base(options)
        {
            _authenticatedUser = authenticatedUser;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetBaseFields(_authenticatedUser);
            return base.SaveChangesAsync(_authenticatedUser.UserId);
        }

        void SetBaseFields(AuthenticatedUser authenticatedUser)
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
}
