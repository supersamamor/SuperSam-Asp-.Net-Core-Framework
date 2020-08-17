using MediatR;
using ProjectNamePlaceHolder.Application.Models.User;

namespace ProjectNamePlaceHolder.Application.Commands.User.ActivateUser
{
    public class ActivateUserRequest : IRequest<UserModel>
    {
        public int Id { get; set; }
        public string Username { get; set; }        
    }
}
