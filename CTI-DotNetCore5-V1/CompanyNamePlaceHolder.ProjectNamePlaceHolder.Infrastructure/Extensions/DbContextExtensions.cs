using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Services;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Extensions
{
    public static class DbContextExtensions
    {
        public static async Task<Option<T>> GetSingle<T>(this DbContext context, Expression<Func<T, bool>> predicate, CancellationToken cancellationToken) where T : BaseEntity =>
            await context.Set<T>().FirstOrDefaultAsync(predicate, cancellationToken);

        public static async Task<T> AddOrUpdate<T>(this DbContext context, T entity, CancellationToken cancellationToken) where T : BaseEntity =>
            (await context.GetSingle<T>(e => e.Id == entity.Id, cancellationToken))
            .Match(
                Some: _ =>
                {
                    context.Update(entity);
                    return entity;
                },
                None: () =>
                {
                    context.Add(entity);
                    return entity;
                });

        public static void SetBaseEntityFields(this DbContext context, IAuthenticatedUserService authenticatedUser)
        {
            foreach (var entry in context.ChangeTracker.Entries<BaseEntity>())
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
