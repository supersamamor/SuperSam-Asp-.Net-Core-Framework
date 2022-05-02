using LanguageExt;
using static LanguageExt.Prelude;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Common.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Common.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Extensions
{
    public static class DbContextExtensions
    {
        public static async Task<Option<T>> GetSingle<T>(this DbContext context, Expression<Func<T, bool>> predicate, CancellationToken cancellationToken, bool tracking = false) 
            where T : class
        {
            var query = tracking ? context.Set<T>() : context.Set<T>().AsNoTracking();
            return await query.FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public static async Task<T> AddOrUpdate<T>(this DbContext context, T entity, Expression<Func<T, bool>> predicate, CancellationToken cancellationToken) 
            where T : class =>
            (await context.GetSingle<T>(predicate, cancellationToken))
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

        public static async Task<Validation<Error, TSuccess>> MustExist<TEntity, TSuccess>(this DbContext context, Expression<Func<TEntity, bool>> predicate, TSuccess success, Error fail, CancellationToken cancellationToken)
            where TEntity : class =>
            await context.GetSingle(predicate, cancellationToken).Match(
                Some: _ => success,
                None: () => Fail<Error, TSuccess>(fail));

        public static async Task<Validation<Error, TSuccess>> MustNotExist<TEntity, TSuccess>(this DbContext context, Expression<Func<TEntity, bool>> predicate, TSuccess success, Error fail, CancellationToken cancellationToken)
            where TEntity : class =>
            await context.GetSingle(predicate, cancellationToken).Match(
                Some: _ => Fail<Error, TSuccess>(fail),
                None: () => success);
    }
}
