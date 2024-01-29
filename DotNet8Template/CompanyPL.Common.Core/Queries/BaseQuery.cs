namespace CompanyPL.Common.Core.Queries;

/// <summary>
/// A base class for queries.
/// </summary>
public abstract record BaseQuery
{
    /// <summary>
    /// The page to retrieve.
    /// </summary>
    public int PageNumber { get; set; }

    /// <summary>
    /// The number of records to retrieve.
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// The column by which to sort the records with.
    /// </summary>
    public string? SortColumn { get; set; }

    /// <summary>
    /// ASC or DESC
    /// </summary>
    public string? SortOrder { get; set; }

    /// <summary>
    /// Columns to use for filtering the results.
    /// </summary>
    public string[]? SearchColumns { get; set; }

    /// <summary>
    /// The value to search for.
    /// </summary>
    public string? SearchValue { get; set; }

    /// <summary>
    /// Initializes an instance of <see cref="BaseQuery"/>
    /// with default page number and page size.
    /// </summary>
    public BaseQuery()
    {
        PageNumber = 1;
        PageSize = 10;
    }

    /// <summary>
    /// Initializes an instance of <see cref="BaseQuery"/>
    /// with the specified page number and page size.
    /// </summary>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    public BaseQuery(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber < 1 ? 1 : pageNumber;
        PageSize = pageSize > 10 ? 10 : pageSize;
    }
}
