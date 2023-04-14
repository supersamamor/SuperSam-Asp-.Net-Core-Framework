using CelerSoft.Common.Core.Base.Models;
using CelerSoft.Common.Data;
using LanguageExt;
using Microsoft.EntityFrameworkCore;

namespace CelerSoft.Common.Core.Queries;

/// <summary>
/// A base class for queries that retrieves a single
/// record by <paramref name="Id"/>.
/// </summary>
/// <param name="Id">The Id of the record to retrieve</param>
public abstract record BaseQueryById(string Id);

/// <summary>
/// A base class for handlers that queries the database
/// for a single record based on the primary key.
/// </summary>
/// <typeparam name="TContext"></typeparam>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TQuery"></typeparam>
public abstract class BaseQueryByIdHandler<TContext, TEntity, TQuery>
    where TContext : DbContext
    where TEntity : BaseEntity
    where TQuery : BaseQueryById
{
    /// <summary>
    /// An instance of TContext.
    /// </summary>
    protected readonly TContext Context;

    /// <summary>
    /// Initializes an instance of <see cref="BaseQueryByIdHandler{TContext, TEntity, TQuery}"/>.
    /// </summary>
    /// <param name="context"></param>
    public BaseQueryByIdHandler(TContext context)
    {
        Context = context;
    }

    /// <summary>
    /// Retrieves a single record from the database based
    /// on the specified <paramref name="request"/>.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual async Task<Option<TEntity>> Handle(TQuery request, CancellationToken cancellationToken = default) =>
        await Context.GetSingle<TEntity>(e => e.Id == request.Id, cancellationToken: cancellationToken);
}
