using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Queries.Users;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Identity.Data;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Common.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Pages.Users.Manage
{
    [Authorize(Policy = Permission.Users.View)]
    public class RolesModel : BasePageModel<RolesModel>
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RolesModel(RoleManager<IdentityRole> roleManager,
                          UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [BindProperty(SupportsGet = true)]
        public string? Id { get; set; }

        [BindProperty]
        public UserViewModel? UserModel { get; set; }

        public IList<RoleViewModel>? Roles { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (Id == null)
            {
                return NotFound();
            }
            return await Mediatr.Send(new GetUserByIdQuery(Id))
                                .ToActionResult(async user =>
                                {
                                    UserModel = new() { Id = user.Id, Name = user.Name };
                                    var userRoles = await _userManager.GetRolesAsync(user);
                                    Roles = _roleManager.Roles.Map(r => new RoleViewModel
                                    {
                                        Id = r.Id,
                                        Name = r.Name,
                                        Selected = userRoles.Any(c => c == r.Name)
                                    }).ToList();
                                    return Page();
                                }, none: null);
        }
    }

    public record UserViewModel
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
    }

    public record RoleViewModel
    {
        public string? Id { get; set; }
        [Display(Name = "Role")]
        public string? Name { get; set; }
        [Display(Name = "Status")]
        public bool Selected { get; set; }
    }
}
