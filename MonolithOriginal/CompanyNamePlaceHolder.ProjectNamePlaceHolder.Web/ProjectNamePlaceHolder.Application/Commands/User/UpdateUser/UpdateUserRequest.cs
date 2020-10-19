using MediatR;
using ProjectNamePlaceHolder.Application.Models.User;

namespace ProjectNamePlaceHolder.Application.Commands.User.UpdateUser
{
    public class UpdateUserRequest : IRequest<UserModel>
    {
        public UserModel User { get; set; }
        public string Username { get; set; }        
    }
}
