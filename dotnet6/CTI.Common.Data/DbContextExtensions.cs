using LanguageExt;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static LanguageExt.Prelude;

namespace CTI.Common.Data;

/// <summary>
/// Extension methods for <see cref="DbContext"/>.
/// </summary>
public static class DbContextExtensions
{
    /// <summary>
    /// Gets the first record that satisfies the specified condition
    /// wrapped in an <see cref="Option{A}"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="context"></param>
    /// <param name="predicate">A function to test each element for a condition</param>
    /// <param name="tracking">Set to true to enable change tracking</param>
    /// <param name="ignoreQueryFilters">Set to true to disable global query filters</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static async Task<Option<T>> GetSingle<T>(this DbContext context, Expression<Func<T, bool>> predicate, bool tracking = false, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default)
        where T : class
    {
        var query = tracking ? context.Set<T>() : context.Set<T>().AsNoTracking();
        query = ignoreQueryFilters ? query.IgnoreQueryFilters() : query;
        return await query.FirstOrDefaultAsync(predicate, cancellationToken) ?? Option<T>.None;
    }

    /// <summary>
    /// Add or Update a record based on whether the record exists
    /// according to the specified condition. To be used for
    /// entities that are not using auto-generated keys. For entities
    /// that has an auto-generated key, you can just use the Update
    /// method.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="context"></param>
    /// <param name="entity">The entity to Add or Update</param>
    /// <param name="predicate">A function to test each element for a condition</param>
    /// <param name="ignoreQueryFilters">Set to true to disable global query filters</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Validates that a record satisfying the specified condition exists.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TSuccess"></typeparam>
    /// <param name="context"></param>
    /// <param name="predicate">A function to test each element for a condition</param>
    /// <param name="success">The object to return if the record exists</param>
    /// <param name="fail">The <see cref="Error"/> to return if the record does not exist</param>
    /// <param name="ignoreQueryFilters">Set to true to disable global query filters</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static async Task<Validation<Error, TSuccess>> MustExist<TEntity, TSuccess>(this DbContext context, Expression<Func<TEntity, bool>> predicate, TSuccess success, Error fail, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default)
        where TEntity : class =>
        await context.GetSingle(predicate, false, ignoreQueryFilters, cancellationToken).Match(
            Some: _ => success,
            None: () => Fail<Error, TSuccess>(fail));

    /// <summary>
    /// Validates that a record satisfying the specified condition does not exist.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TSuccess"></typeparam>
    /// <param name="context"></param>
    /// <param name="predicate">A function to test each element for a condition</param>
    /// <param name="success">The object to return if the record does not exist</param>
    /// <param name="fail">The <see cref="Error"/> to return if the record exists</param>
    /// <param name="ignoreQueryFilters">Set to true to disable global query filters</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static async Task<Validation<Error, TSuccess>> MustNotExist<TEntity, TSuccess>(this DbContext context, Expression<Func<TEntity, bool>> predicate, TSuccess success, Error fail, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default)
        where TEntity : class =>
        await context.GetSingle(predicate, false, ignoreQueryFilters, cancellationToken).Match(
            Some: _ => Fail<Error, TSuccess>(fail),
            None: () => success);

    /// <summary>
    /// Determines if a record satisfying the specified condition exists.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="context"></param>
    /// <param name="predicate">A function to test each element for a condition</param>
    /// <param name="ignoreQueryFilters">Set to true to disable global query filters</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static async Task<bool> Exists<T>(this DbContext context, Expression<Func<T, bool>> predicate, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default)
        where T : class
    {
        var query = ignoreQueryFilters ? context.Set<T>().IgnoreQueryFilters() : context.Set<T>();
        return await query.AnyAsync(predicate, cancellationToken);
    }

    /// <summary>
    /// Determines if a record satisfying the specified condition does not exist.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="context"></param>
    /// <param name="predicate">A function to test each element for a condition</param>
    /// <param name="ignoreQueryFilters">Set to true to disable global query filters</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static async Task<bool> NotExists<T>(this DbContext context, Expression<Func<T, bool>> predicate, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default)
        where T : class
    {
        var query = ignoreQueryFilters ? context.Set<T>().IgnoreQueryFilters() : context.Set<T>();
        return !await query.AnyAsync(predicate, cancellationToken);
    }
}
