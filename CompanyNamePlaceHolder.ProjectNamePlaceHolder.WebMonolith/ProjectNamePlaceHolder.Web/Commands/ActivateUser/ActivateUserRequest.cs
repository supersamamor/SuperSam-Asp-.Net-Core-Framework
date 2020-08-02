using MediatR;
using ProjectNamePlaceHolder.Web.Models.User;

namespace ProjectNamePlaceHolder.Web.Commands.ActivateUser
{
    public class ActivateUserRequest : IRequest<UserModel>
    {
        public int Id { get; set; }
        public string Username { get; set; }        
    }
}
