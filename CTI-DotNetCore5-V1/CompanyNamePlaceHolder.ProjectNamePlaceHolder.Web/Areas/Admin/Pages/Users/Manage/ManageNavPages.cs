﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Pages.Users.Manage
{
    public static class ManageNavPages
    {
        public static string Index => "Index";

        public static string Roles => "Roles";

        public static string? IndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, Index);

        public static string? RolesNavClass(ViewContext viewContext) => PageNavClass(viewContext, Roles);

        private static string? PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}
