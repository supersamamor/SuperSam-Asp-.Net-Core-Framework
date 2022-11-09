using System.Collections.Generic;
namespace CTI.ELMS.LocationApi.Models
{
    public class PagedListApiResponse<T>
    {
        public MetaData MetaData { get; set; } = new MetaData();
        public IList<T> Data { get; set; } = new List<T>();
        public int TotalCount
        {
            get
            {
                return Data.Count;
            }
        }
    }
    public class MetaData
    {
        public int PageCount { get; set; }
        public int TotalItemCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
        public bool IsFirstPage { get; set; }
        public bool IsLastPage { get; set; }
        public int FirstItemOnPage { get; set; }
        public int LastItemOnPage { get; set; }
    }
}
