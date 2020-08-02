using MediatR;
using ProjectNamePlaceHolder.Web.Models.User;

namespace ProjectNamePlaceHolder.Web.Commands.UpdateUser
{
    public class UpdateUserRequest : IRequest<UserModel>
    {
        public UserModel User { get; set; }
        public string Username { get; set; }        
    }
}
