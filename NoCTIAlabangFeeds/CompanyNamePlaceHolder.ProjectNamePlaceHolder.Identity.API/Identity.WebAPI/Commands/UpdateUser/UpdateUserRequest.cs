using MediatR;
using Identity.WebAPI.Models;

namespace Identity.WebAPI.Commands.UpdateUser
{
    public class UpdateUserRequest : IRequest<UserModel>
    {
        public UserModel User { get; set; }
        public string Username { get; set; }        
    }
}
