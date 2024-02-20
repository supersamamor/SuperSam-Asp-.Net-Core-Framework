using X.PagedList;

namespace CompanyNamePlaceHolder.Common.Utility.Models;

/// <summary>
/// A class representing a paged list.
/// </summary>
/// <typeparam name="T"></typeparam>
public class PagedListResponse<T>
{
    /// <summary>
    /// Metadata about the paged list.
    /// </summary>
    public PagedListMetaData MetaData { get; private set; }

    /// <summary>
    /// The data.
    /// </summary>
    public IPagedList<T> Data { get; private set; }

    /// <summary>
    /// Total no of records in the list.
    /// </summary>
    public int TotalCount { get; private set; }

    /// <summary>
    /// Creates an instance of <see cref="PagedListResponse{T}"/>.
    /// </summary>
    /// <param name="data">An instance of <see cref="IPagedList{T}"/></param>
    /// <param name="totalCount">The total no of records in the list.</param>
    public PagedListResponse(IPagedList<T> data, int totalCount)
    {
        MetaData = data.GetMetaData();
        Data = data;
        TotalCount = totalCount;
    }
}