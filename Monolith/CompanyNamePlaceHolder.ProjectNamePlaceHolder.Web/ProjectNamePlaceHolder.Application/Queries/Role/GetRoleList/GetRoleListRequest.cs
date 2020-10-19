using MediatR;
using ProjectNamePlaceHolder.Application.Models.Role;
using X.PagedList;

namespace ProjectNamePlaceHolder.Application.Queries.Role.GetRoleList
{
    public class GetRoleListRequest : IRequest<StaticPagedList<RoleModel>>
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
