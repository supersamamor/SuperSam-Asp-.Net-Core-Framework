using LanguageExt;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static CTI.Common.Utility.Helpers.OptionHelper;
using static LanguageExt.Prelude;

namespace CompanyNamePlaceHolder.WebAppTemplate.Infrastructure.Extensions;

public static class DbContextExtensions
{
    public static async Task<bool> Exists<T>(this DbContext context, Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        where T : class =>
        await context.Set<T>().AnyAsync(predicate, cancellationToken);

    public static async Task<bool> NotExists<T>(this DbContext context, Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        where T : class =>
        !await context.Set<T>().AnyAsync(predicate, cancellationToken);

    public static async Task<Option<T>> GetSingle<T>(this DbContext context, Expression<Func<T, bool>> predicate, CancellationToken cancellationToken, bool tracking = false)
        where T : class
    {
        var query = tracking ? context.Set<T>() : context.Set<T>().AsNoTracking();
        return ToOption(await query.FirstOrDefaultAsync(predicate, cancellationToken));
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
