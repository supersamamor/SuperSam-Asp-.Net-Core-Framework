using MediatR;
using Identity.WebAPI.Models;

namespace Identity.WebAPI.Commands.AddUser
{
    public class AddUserRequest : IRequest<UserModel>
    {
        public UserModel User { get; set; }
        public string Username { get; set; }
    }
}
