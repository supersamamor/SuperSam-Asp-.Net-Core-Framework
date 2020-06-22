using MediatR;
using Identity.WebAPI.Models;

namespace Identity.WebAPI.Queries.GetUserList
{
    public class GetUserListRequest : IRequest<CustomPagedList<UserModel>>
    {
        public string SearchKey { get; set; }
        public string OrderBy { get; set; }
        public string SortBy { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
