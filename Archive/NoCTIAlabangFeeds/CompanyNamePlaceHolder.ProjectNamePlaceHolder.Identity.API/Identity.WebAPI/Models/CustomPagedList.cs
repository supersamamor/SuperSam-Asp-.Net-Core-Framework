using System.Collections.Generic;
using System.Linq;
using X.PagedList;
namespace Identity.WebAPI.Models
{
    public class CustomPagedList<T>
    {
        public CustomPagedList() { }
        public CustomPagedList(IEnumerable<T> items, int pageIndex, int pageSize)
        {
            pageIndex = pageIndex == 0 ? 1 : pageIndex;
            if (pageSize == 0)
            {
                var recordCount = items.Count();
                pageSize = recordCount == 0 ? 1 : recordCount;
            }
            pageSize = pageSize == 0 ? items.Count() == 0 ? 1 : items.Count() : pageSize;
            var pagedQuery = items.ToPagedList(pageIndex, pageSize);
            PagedListMetaData = pagedQuery.GetMetaData();
            Items = pagedQuery.ToList();
        }
        public CustomPagedList(IEnumerable<T> items, PagedListMetaData pagedListMetaData)
        {
            Items = items;
            PagedListMetaData = pagedListMetaData;
        }
        public IEnumerable<T> Items { get; set; }
        public PagedListMetaData PagedListMetaData { get; set; }   
    }
}
