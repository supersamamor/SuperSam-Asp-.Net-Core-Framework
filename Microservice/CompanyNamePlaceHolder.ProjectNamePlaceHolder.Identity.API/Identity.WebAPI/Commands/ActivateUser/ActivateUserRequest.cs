using MediatR;
using Identity.WebAPI.Models;

namespace Identity.WebAPI.Commands.ActivateUser
{
    public class ActivateUserRequest : IRequest<UserModel>
    {
        public int Id { get; set; }
        public string Username { get; set; }        
    }
}
