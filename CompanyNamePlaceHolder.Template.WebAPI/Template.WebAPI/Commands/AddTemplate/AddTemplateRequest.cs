using MediatR;
using Template.WebAPI.Models;

namespace Template.WebAPI.Commands.AddTemplate
{
    public class AddTemplateRequest : IRequest<TemplateModel>
    {
        public TemplateModel Template { get; set; }
        public string Username { get; set; }
    }
}
