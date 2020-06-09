using MediatR;
using Template.WebAPI.Models;

namespace Template.WebAPI.Commands.UpdateTemplate
{
    public class UpdateTemplateRequest : IRequest<TemplateModel>
    {
        public TemplateModel Template { get; set; }
        public string Username { get; set; }        
    }
}
