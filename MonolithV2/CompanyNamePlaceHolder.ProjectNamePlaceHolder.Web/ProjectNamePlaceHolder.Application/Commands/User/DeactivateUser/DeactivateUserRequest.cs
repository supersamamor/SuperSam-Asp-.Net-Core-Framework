using MediatR;
using ProjectNamePlaceHolder.Application.Models.User;

namespace ProjectNamePlaceHolder.Application.Commands.User.DeactivateUser
{
    public class DeactivateUserRequest : IRequest<UserModel>
    {
        public int Id { get; set; }
        public string Username { get; set; }        
    }
}
