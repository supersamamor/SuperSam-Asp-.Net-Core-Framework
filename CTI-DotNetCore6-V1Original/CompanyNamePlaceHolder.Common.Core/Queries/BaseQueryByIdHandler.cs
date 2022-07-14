using CompanyNamePlaceHolder.Common.Core.Base.Models;
using CompanyNamePlaceHolder.Common.Data;
using LanguageExt;
using Microsoft.EntityFrameworkCore;

namespace CompanyNamePlaceHolder.Common.Core.Queries;

public record BaseQueryById(string Id);

public class BaseQueryByIdHandler<TContext, TEntity, TQuery>
    where TContext : DbContext
    where TEntity : BaseEntity
    where TQuery : BaseQueryById
{
    protected readonly TContext _context;

    public BaseQueryByIdHandler(TContext context)
    {
        _context = context;
    }

    public virtual async Task<Option<TEntity>> Handle(TQuery request, CancellationToken cancellationToken = default) =>
        await _context.GetSingle<TEntity>(e => e.Id == request.Id, cancellationToken: cancellationToken);
}
