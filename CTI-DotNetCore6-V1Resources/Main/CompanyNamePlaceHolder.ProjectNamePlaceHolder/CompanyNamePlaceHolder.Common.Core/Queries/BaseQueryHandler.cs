using CompanyNamePlaceHolder.Common.Utility.Extensions;
using CompanyNamePlaceHolder.Common.Utility.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyNamePlaceHolder.Common.Core.Queries;

public class BaseQueryHandler<TContext, TEntity, TQuery>
    where TContext : DbContext
    where TEntity : class
    where TQuery : BaseQuery
{
    protected readonly TContext _context;

    public BaseQueryHandler(TContext context)
    {
        _context = context;
    }

    public virtual async Task<PagedListResponse<TEntity>> Handle(TQuery request, CancellationToken cancellationToken = default) =>
        await _context.Set<TEntity>().AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
                                                                     request.SortColumn, request.SortOrder,
                                                                     request.PageNumber, request.PageSize,
                                                                     cancellationToken);
}
