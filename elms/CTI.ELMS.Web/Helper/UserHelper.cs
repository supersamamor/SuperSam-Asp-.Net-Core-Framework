using CTI.ELMS.Web.Areas.Admin.Queries.Users;
using MediatR;

namespace CTI.ELMS.Web.Helper
{
    public class UserHelper
    {
        private readonly IMediator _mediator;
        public UserHelper(IMediator mediator)
        {
            _mediator = mediator;
        }
        public string GetUserName(string userId)
        {
            string userName = "[User Not Found]";
            _ = _mediator.Send(new GetUserByIdQuery(userId)).Result.Select(l => userName = l.Name!);      
            return userName;
        }
    }
}
