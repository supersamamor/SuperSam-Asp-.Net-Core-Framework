using Microsoft.EntityFrameworkCore;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Common.Extensions;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Common.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Models
{
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
}
