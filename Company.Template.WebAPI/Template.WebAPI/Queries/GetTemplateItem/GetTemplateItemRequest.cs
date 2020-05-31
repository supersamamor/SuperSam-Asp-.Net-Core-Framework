using MediatR;
using Template.WebAPI.Models;

namespace Template.WebAPI.Queries.GetTemplateItem
{
    public class GetTemplateItemRequest : IRequest<TemplateModel>
    {
        public int Id { get; set; }
    }
}
