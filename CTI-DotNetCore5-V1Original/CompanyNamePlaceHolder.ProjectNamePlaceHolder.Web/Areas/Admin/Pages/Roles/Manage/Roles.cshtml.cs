using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Queries.Roles;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Common.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Pages.Roles.Manage
{
    [Authorize(Policy = Permission.Roles.View)]
    public class RolesModel : BasePageModel<RolesModel>
    {
        [BindProperty(SupportsGet = true)]
        public string? Id { get; set; }

        public RoleViewModel? Role { get; set; }

        public async Task<IActionResult> OnGet()
        {
            if (Id == null)
            {
                return NotFound();
            }
            return await Mediatr.Send(new GetRoleByIdQuery(Id))
                                .ToActionResult(e =>
                                {
                                    Role = Mapper.Map<RoleViewModel>(e);
                                    return Page();
                                }, none: null);
        }
    }
}
