using CTI.WebAppTemplate.Core;
using CTI.WebAppTemplate.Infrastructure.Extensions;
using LanguageExt;
using Microsoft.EntityFrameworkCore;

namespace CTI.WebAppTemplate.Application.Common;

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

    public virtual async Task<Option<TEntity>> Handle(TQuery request, CancellationToken cancellationToken) =>
        await _context.GetSingle<TEntity>(e => e.Id == request.Id, cancellationToken);
}
