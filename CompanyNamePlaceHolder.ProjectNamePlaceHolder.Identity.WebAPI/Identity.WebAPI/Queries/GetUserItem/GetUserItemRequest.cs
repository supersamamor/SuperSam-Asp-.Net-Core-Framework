using MediatR;
using Identity.WebAPI.Models;

namespace Identity.WebAPI.Queries.GetUserItem
{
    public class GetUserItemRequest : IRequest<UserModel>
    {
        public string Id { get; set; }
    }
}
