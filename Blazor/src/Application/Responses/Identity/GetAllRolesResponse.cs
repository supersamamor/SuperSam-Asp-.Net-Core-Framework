using System.Collections.Generic;

namespace ProjectNamePlaceHolder.Application.Responses.Identity
{
    public class GetAllRolesResponse
    {
        public IEnumerable<RoleResponse> Roles { get; set; }
    }
}