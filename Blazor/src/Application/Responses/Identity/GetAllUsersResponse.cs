using System.Collections.Generic;

namespace ProjectNamePlaceHolder.Application.Responses.Identity
{
    public class GetAllUsersResponse
    {
        public IEnumerable<UserResponse> Users { get; set; }
    }
}