using CTI.WebAppTemplate.Application.Common;
using DataTables.AspNetCore.Mvc.Binder;

namespace CTI.WebAppTemplate.Web.Models;

public static class DataTablesRequestExtensions
{
    public static T ToQuery<T>(this DataTablesRequest dataTablesRequest) where T : BaseQuery, new()
    {
        var columns = dataTablesRequest.Columns;
        var search = dataTablesRequest.Search;
        var sort = dataTablesRequest.Orders.ElementAt(0);

        T query = new();
        query.PageNumber = (dataTablesRequest.Start / dataTablesRequest.Length) + 1;
        query.PageSize = dataTablesRequest.Length;
        query.SearchColumns = columns.Where(c => c.Searchable).Select(c => c.Name).ToArray();
        query.SearchValue = search?.Value;
        query.SortColumn = columns.ElementAt(sort.Column).Name;
        query.SortOrder = sort.Dir;
        return query;
    }
}
