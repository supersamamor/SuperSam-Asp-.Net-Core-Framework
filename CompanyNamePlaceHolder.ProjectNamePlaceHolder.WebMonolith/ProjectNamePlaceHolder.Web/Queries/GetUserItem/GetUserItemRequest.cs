using MediatR;
using ProjectNamePlaceHolder.Web.Models.User;

namespace ProjectNamePlaceHolder.Web.Queries.GetUserItem
{
    public class GetUserItemRequest : IRequest<UserModel>
    {
        public int Id { get; set; }
    }
}
