using CTI.Common.Utility.Extensions;
using CTI.Common.Utility.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Common;

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

    public virtual async Task<PagedListResponse<TEntity>> Handle(TQuery request, CancellationToken cancellationToken) =>
        await _context.Set<TEntity>().AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
                                                                     request.SortColumn, request.SortOrder,
                                                                     request.PageNumber, request.PageSize,
                                                                     cancellationToken);
}
