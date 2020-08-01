using MediatR;
using ProjectNamePlaceHolder.Web.Models;
using ProjectNamePlaceHolder.Web.Models.MainModulePlaceHolder;

namespace ProjectNamePlaceHolder.Web.Queries.GetMainModulePlaceHolderList
{
    public class GetMainModulePlaceHolderListRequest : IRequest<CustomPagedList<MainModulePlaceHolderModel>>
    {
        public string SearchKey { get; set; }
        public string OrderBy { get; set; }
        public string SortBy { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
