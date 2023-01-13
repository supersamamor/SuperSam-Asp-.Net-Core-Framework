using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using LanguageExt;

namespace CTI.LocationApi.Web.Models;

public record Select2Request
{
    public string Term { get; init; } = "";
    public int Page { get; init; } = 1;
}

public record Select2Result
{
    public string Id { get; init; } = "";
    public string Text { get; init; } = "";
}

public record Select2Pagination
{
    public bool More { get; init; }
}

public record Select2Response
{
    public IEnumerable<Select2Result> Results { get; set; } = new List<Select2Result>();
    public Select2Pagination Pagination { get; init; } = new();
}

public static class Select2RequestExtensions
{
    /// <summary>
    /// Converts <see cref="Select2Request"/> to <typeparamref name="T"/>.
    /// Results can be filtered by <paramref name="searchColumn"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="select2Request"></param>
    /// <param name="searchColumn"></param>
    /// <returns></returns>
    public static T ToQuery<T>(this Select2Request select2Request, string searchColumn) where T : BaseQuery, new()
    {
        T query = new()
        {
            PageNumber = select2Request.Page,
            SearchColumns = new string[] { searchColumn },
            SearchValue = select2Request.Term,
            SortColumn = searchColumn,
            SortOrder = "asc"
        };
        return query;
    }

    /// <summary>
    /// Converts <see cref="PagedListResponse{T}"/> to <see cref="Select2Response"/>.
    /// Data in <see cref="PagedListResponse{T}"/> is converted to <see cref="Select2Result"/>
    /// using the provided mapper function.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="results"></param>
    /// <param name="mapper">The function to convert <typeparamref name="T"/> to <see cref="Select2Result"/></param>
    /// <returns></returns>
    public static Select2Response ToSelect2Response<T>(this PagedListResponse<T> results, Func<T, Select2Result> mapper) =>
        new() { Results = results.Data.Map(e => mapper(e)), Pagination = new() { More = results.MetaData.HasNextPage } };

    /// <summary>
    /// Converts <see cref="PagedListResponse{T}"/> to <see cref="Select2Response"/>.
    /// Data in <see cref="PagedListResponse{T}"/> is converted to <see cref="Select2Result"/>
    /// using the provided mapper function.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="results"></param>
    /// <param name="mapper">The function to convert <typeparamref name="T"/> to <see cref="Select2Result"/></param>
    /// <returns></returns>
    public static async Task<Select2Response> ToSelect2ResponseAsync<T>(this Task<PagedListResponse<T>> results, Func<T, Select2Result> mapper) =>
        await results.Map(r => r.ToSelect2Response(mapper));
}
