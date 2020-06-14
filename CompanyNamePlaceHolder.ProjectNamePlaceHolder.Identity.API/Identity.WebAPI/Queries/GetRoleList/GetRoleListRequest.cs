using MediatR;
using Identity.WebAPI.Models;

namespace Identity.WebAPI.Queries.GetRoleList
{
    public class GetRoleListRequest : IRequest<CustomPagedList<RoleModel>>
    {
        public string SearchKey { get; set; }
        public string OrderBy { get; set; }
        public string SortBy { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string FilterBy { get; set; }
        public int UserId { get; set; }
    }
}
