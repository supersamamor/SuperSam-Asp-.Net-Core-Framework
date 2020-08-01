using MediatR;
using ProjectNamePlaceHolder.Web.Models.MainModulePlaceHolder;
using X.PagedList;

namespace ProjectNamePlaceHolder.Web.Queries.GetMainModulePlaceHolderList
{
    public class GetMainModulePlaceHolderListRequest : IRequest<StaticPagedList<MainModulePlaceHolderModel>>
    {
        public string SearchKey { get; set; }
        public string OrderBy { get; set; }
        public string SortBy { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
