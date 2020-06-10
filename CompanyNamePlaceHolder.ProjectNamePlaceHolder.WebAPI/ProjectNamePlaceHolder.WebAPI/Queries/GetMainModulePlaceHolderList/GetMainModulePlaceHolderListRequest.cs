using MediatR;
using ProjectNamePlaceHolder.WebAPI.Models;

namespace ProjectNamePlaceHolder.WebAPI.Queries.GetMainModulePlaceHolderList
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
