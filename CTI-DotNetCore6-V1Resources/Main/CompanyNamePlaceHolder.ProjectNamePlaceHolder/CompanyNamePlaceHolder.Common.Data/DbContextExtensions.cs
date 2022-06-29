using LanguageExt;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static LanguageExt.Prelude;

namespace CompanyNamePlaceHolder.Common.Data;

public static class DbContextExtensions
{
    public static async Task<Option<T>> GetSingle<T>(this DbContext context, Expression<Func<T, bool>> predicate, bool tracking = false, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default)
        where T : class
    {
        var query = tracking ? context.Set<T>() : context.Set<T>().AsNoTracking();
        query = ignoreQueryFilters ? query.IgnoreQueryFilters() : query;
        return await query.FirstOrDefaultAsync(predicate, cancellationToken) ?? Option<T>.None;
    }

    public static async Task<T> AddOrUpdate<T>(this DbContext context, T entity, Expression<Func<T, bool>> predicate, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default)
        where T : class =>
        (await context.GetSingle(predicate, false, ignoreQueryFilters, cancellationToken))
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

    public static async Task<Validation<Error, TSuccess>> MustExist<TEntity, TSuccess>(this DbContext context, Expression<Func<TEntity, bool>> predicate, TSuccess success, Error fail, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default)
        where TEntity : class =>
        await context.GetSingle(predicate, false, ignoreQueryFilters, cancellationToken).Match(
            Some: _ => success,
            None: () => Fail<Error, TSuccess>(fail));

    public static async Task<Validation<Error, TSuccess>> MustNotExist<TEntity, TSuccess>(this DbContext context, Expression<Func<TEntity, bool>> predicate, TSuccess success, Error fail, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default)
        where TEntity : class =>
        await context.GetSingle(predicate, false, ignoreQueryFilters, cancellationToken).Match(
            Some: _ => Fail<Error, TSuccess>(fail),
            None: () => success);

    public static async Task<bool> Exists<T>(this DbContext context, Expression<Func<T, bool>> predicate, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default)
        where T : class
    {
        var query = ignoreQueryFilters ? context.Set<T>() : context.Set<T>().IgnoreQueryFilters();
        return await query.AnyAsync(predicate, cancellationToken);
    }

    public static async Task<bool> NotExists<T>(this DbContext context, Expression<Func<T, bool>> predicate, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default)
        where T : class
    {
        var query = ignoreQueryFilters ? context.Set<T>().IgnoreQueryFilters() : context.Set<T>();
        return !await query.AnyAsync(predicate, cancellationToken);
    }
}
