using MediatR;

namespace Identity.WebAPI.Commands.DeleteUser
{
    public class DeleteUserRequest : IRequest
    {
        public string Id { get; set; }
    }
}
