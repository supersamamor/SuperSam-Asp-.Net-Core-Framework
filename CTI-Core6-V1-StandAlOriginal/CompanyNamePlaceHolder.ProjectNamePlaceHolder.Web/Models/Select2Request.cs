using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;


namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Models;

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
    public static T ToQuery<T>(this Select2Request select2Request, string searchColumn) where T : BaseQuery, new()
    {
        T query = new();
        query.PageNumber = select2Request.Page;
        query.SearchColumns = new string[] { searchColumn };
        query.SearchValue = select2Request.Term;
        query.SortColumn = searchColumn;
        query.SortOrder = "asc";
        return query;
    }

    public static Select2Response ToSelect2Response<T>(this PagedListResponse<T> results, Func<T, Select2Result> mapper) =>
        new() { Results = results.Data.Map(e => mapper(e)), Pagination = new() { More = results.MetaData.HasNextPage } };
}
