using MediatR;
using ProjectNamePlaceHolder.Application.Models.MainModulePlaceHolder;
using X.PagedList;

namespace ProjectNamePlaceHolder.Application.Queries.MainModulePlaceHolder.GetMainModulePlaceHolderList
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
