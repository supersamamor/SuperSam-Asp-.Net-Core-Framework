using CTI.Common.Utility.Extensions;
using CTI.Common.Utility.Models;
using Microsoft.EntityFrameworkCore;

namespace CTI.Common.Core.Queries;

/// <summary>
/// A base class for query handlers that retrieve
/// records from a database.
/// </summary>
/// <typeparam name="TContext"></typeparam>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TQuery"></typeparam>
public abstract class BaseQueryHandler<TContext, TEntity, TQuery>
    where TContext : DbContext
    where TEntity : class
    where TQuery : BaseQuery
{
    /// <summary>
    /// An instance of TContext.
    /// </summary>
    protected readonly TContext Context;

    /// <summary>
    /// Initializes an instance of <see cref="BaseQueryHandler{TContext, TEntity, TQuery}"/>.
    /// </summary>
    /// <param name="context"></param>
    public BaseQueryHandler(TContext context)
    {
        Context = context;
    }

    /// <summary>
    /// Retrieves records from the database based on the
    /// specified <paramref name="request"/>.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual async Task<PagedListResponse<TEntity>> Handle(TQuery request, CancellationToken cancellationToken = default) =>
        await Context.Set<TEntity>().AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
                                                                     request.SortColumn, request.SortOrder,
                                                                     request.PageNumber, request.PageSize,
                                                                     cancellationToken);
}
