using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Common.Models
{
    public record PagedListResponse<T>
    {
        public PagedListMetaData MetaData { get; init; }
        public IPagedList<T> Data { get; init; }
        public int TotalCount { get; set; }

        public PagedListResponse(IPagedList<T> data, int totalCount)
        {
            MetaData = data.GetMetaData();
            Data = data;
            TotalCount = totalCount;
        }
    }
}
