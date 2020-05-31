using MediatR;
using Template.WebAPI.Models;

namespace Template.WebAPI.Queries.GetTemplateList
{
    public class GetTemplateListRequest : IRequest<CustomPagedList<TemplateModel>>
    {
        public string SearchKey { get; set; }
        public string OrderBy { get; set; }
        public string SortBy { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
