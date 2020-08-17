using MediatR;
using ProjectNamePlaceHolder.Application.Models.User;

namespace ProjectNamePlaceHolder.Application.Queries.User.GetUserItem
{
    public class GetUserItemRequest : IRequest<UserModel>
    {
        public int Id { get; set; }
    }
}
