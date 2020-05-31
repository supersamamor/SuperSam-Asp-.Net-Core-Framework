using MediatR;

namespace Template.WebAPI.Commands.DeleteTemplate
{
    public class DeleteTemplateRequest : IRequest
    {
        public int Id { get; set; }
    }
}
