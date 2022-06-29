using CompanyNamePlaceHolder.Common.Utility.Models;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using X.PagedList;

namespace CompanyNamePlaceHolder.Common.Utility.Extensions
{
    public static class QueryableExtensions
    {
        public static async Task<PagedListResponse<T>> ToPagedResponse<T>(this IQueryable<T> query,
                                                                          string[]? searchColumns, string? searchValue,
                                                                          string? sortColumn, string? sortOrder,
                                                                          int pageNumber, int pageSize,
                                                                          CancellationToken cancellationToken = default)
        {
            if (!string.IsNullOrWhiteSpace(searchValue) && searchColumns != null)
            {
                var filter = string.Join(" OR ", searchColumns.Select(x => $"{x}.Contains(\"{searchValue}\")"));
                if (!string.IsNullOrWhiteSpace(filter))
                {
                    query = query.Where(filter);
                }
            }
            if (!string.IsNullOrWhiteSpace(sortColumn) && !string.IsNullOrWhiteSpace(sortOrder))
            {
                query = query.OrderBy($"{sortColumn} {sortOrder}");
            }
            var data = await query.ToPagedListAsync(pageNumber, pageSize, cancellationToken);
            return new PagedListResponse<T>(data, query.Count());
        }
    }
}