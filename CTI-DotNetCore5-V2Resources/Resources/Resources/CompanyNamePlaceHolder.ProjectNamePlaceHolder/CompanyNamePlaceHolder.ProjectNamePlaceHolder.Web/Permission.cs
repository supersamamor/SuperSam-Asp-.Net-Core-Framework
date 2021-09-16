using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web
{
    public static class Permission
    {
        public static IEnumerable<string> GenerateAllPermissions()
        {
            return GeneratePermissionsForModule("Admin")
                .Concat(GeneratePermissionsForModule("Entities"))
                .Concat(GeneratePermissionsForModule("Roles"))
                .Concat(GeneratePermissionsForModule("Users"))
                .Concat(GeneratePermissionsForModule("Apis"))
                .Concat(GeneratePermissionsForModule("Applications"))
                .Concat(GeneratePermissionsForModule("Projects"))
				Template:[InsertNewPermissionGenerator];
        }

        public static IEnumerable<string> GeneratePermissionsForModule(string module)
        {
            return new List<string>()
            {
                $"Permission.{module}.Create",
                $"Permission.{module}.View",
                $"Permission.{module}.Edit",
                $"Permission.{module}.Delete",
            };
        }

        public static class Admin
        {
            public const string View = "Permission.Admin.View";
            public const string Create = "Permission.Admin.Create";
            public const string Edit = "Permission.Admin.Edit";
            public const string Delete = "Permission.Admin.Delete";
        }

        public static class Entities
        {
            public const string View = "Permission.Entities.View";
            public const string Create = "Permission.Entities.Create";
            public const string Edit = "Permission.Entities.Edit";
            public const string Delete = "Permission.Entities.Delete";
        }

        public static class Roles
        {
            public const string View = "Permission.Roles.View";
            public const string Create = "Permission.Roles.Create";
            public const string Edit = "Permission.Roles.Edit";
            public const string Delete = "Permission.Roles.Delete";
        }

        public static class Users
        {
            public const string View = "Permission.Users.View";
            public const string Create = "Permission.Users.Create";
            public const string Edit = "Permission.Users.Edit";
            public const string Delete = "Permission.Users.Delete";
        }

        public static class Apis
        {
            public const string View = "Permission.Apis.View";
            public const string Create = "Permission.Apis.Create";
            public const string Edit = "Permission.Apis.Edit";
            public const string Delete = "Permission.Apis.Delete";
        }

        public static class Applications
        {
            public const string View = "Permission.Applications.View";
            public const string Create = "Permission.Applications.Create";
            public const string Edit = "Permission.Applications.Edit";
            public const string Delete = "Permission.Applications.Delete";
        }

        Template:[InsertNewPermissionTextHere]
    }
}
