using X.PagedList;

namespace CompanyNamePlaceHolder.Common.Utility.Models
{
    public class PagedListResponse<T>
    {
        public PagedListMetaData MetaData { get; private set; }
        public IPagedList<T> Data { get; private set; }
        public int TotalCount { get; private set; }

        public PagedListResponse(IPagedList<T> data, int totalCount)
        {
            MetaData = data.GetMetaData();
            Data = data;
            TotalCount = totalCount;
        }
    }
}