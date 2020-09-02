﻿using System.ComponentModel.DataAnnotations;

namespace ProjectNamePlaceHolder.Application.Models.Role
{
    public class RoleModel
    {
        public string Id { get; set; }
        [Display(Name = "LabelRoleName", ResourceType = typeof(Resource))]
        public string Name { get; set; }
    }
}
