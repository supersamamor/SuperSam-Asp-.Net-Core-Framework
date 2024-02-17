﻿using ProjectNamePlaceHolder.Application.Responses.Identity;
using System.Collections.Generic;

namespace ProjectNamePlaceHolder.Application.Requests.Identity
{
    public class UpdateUserRolesRequest
    {
        public string UserId { get; set; }
        public IList<UserRoleModel> UserRoles { get; set; }
    }
}