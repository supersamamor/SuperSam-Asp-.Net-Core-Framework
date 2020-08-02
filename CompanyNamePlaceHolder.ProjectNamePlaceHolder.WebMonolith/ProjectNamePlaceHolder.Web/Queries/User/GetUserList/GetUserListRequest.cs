using MediatR;
using ProjectNamePlaceHolder.Web.Models.User;
using X.PagedList;

namespace ProjectNamePlaceHolder.Web.Queries.User.GetUserList
{
    public class GetUserListRequest : IRequest<StaticPagedList<UserModel>>
    {
        public string SearchKey { get; set; }
        public string OrderBy { get; set; }
        public string SortBy { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
