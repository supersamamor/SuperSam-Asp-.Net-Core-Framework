using MediatR;

namespace Identity.WebAPI.Commands.DeleteUser
{
    public class DeleteUserRequest : IRequest
    {
        public int Id { get; set; }
    }
}
