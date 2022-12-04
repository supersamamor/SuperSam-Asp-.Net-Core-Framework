using CTI.Common.Web.Utility.Extensions;
using CTI.FAS.Web.Areas.Admin.Models;
using CTI.FAS.Web.Areas.Admin.Queries.Users;
using CTI.FAS.Infrastructure.Data;
using CTI.FAS.Web.Models;
using LanguageExt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CTI.FAS.Core.Identity;

namespace CTI.FAS.Web.Areas.Admin.Pages.Users;

[Authorize(Policy = Permission.Users.View)]
public class ViewModel : BasePageModel<ViewModel>
{
    readonly IdentityContext _context;
    readonly RoleManager<IdentityRole> _roleManager;
    readonly UserManager<ApplicationUser> _userManager;

    public ViewModel(IdentityContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public UserDetailsViewModel UserDetails { get; set; } = new();
    public IList<UserRoleViewModel> Roles { get; set; } = new List<UserRoleViewModel>();

    public async Task<IActionResult> OnGet(string id)
    {
        if (id == null)
        {
            return NotFound();
        }
        return await Mediatr.Send(new GetUserByIdQuery(id))
                            .ToActionResult(async user =>
                            {
                                UserDetails = await GetViewModel(user);
                                UserDetails = await GetGroup(UserDetails);
                                Roles = await GetRolesForUser(user);
                                return Page();
                            }, none: null);
    }

    async Task<UserDetailsViewModel> GetViewModel(ApplicationUser user) =>
        await _context.GetEntityName(user.EntityId!).Match(
            entity => new UserDetailsViewModel
            {
                Id = user.Id,
                Name = user.Name ?? "",
                BirthDate = user.BirthDate!.Value.ToString("MMMM d, yyyy"),
                Email = user.Email,
                Entity = entity,
                GroupId = user.GroupId,
            },
            () => new UserDetailsViewModel
            {
                Id = user.Id,
                Name = user.Name ?? "",
                BirthDate = user.BirthDate!.Value.ToString("MMMM d, yyyy"),
                Email = user.Email,
                Entity =  Core.Constants.Entities.Default,
                IsActive = user.IsActive,
            });
    async Task<UserDetailsViewModel> GetGroup(UserDetailsViewModel user) =>
        await _context.GetGroupName(user.GroupId!).Match(
            group => new UserDetailsViewModel
            {
                Id = user.Id,
                Name = user.Name ?? "",
                BirthDate = user.BirthDate,
                Email = user.Email,
                Entity = user.Entity,
                Group = group,
            },
            () => new UserDetailsViewModel
            {
                Id = user.Id,
                Name = user.Name ?? "",
                BirthDate = user.BirthDate,
                Email = user.Email,
                Entity = Core.Constants.Entities.Default,
                IsActive = user.IsActive,
            });
    async Task<IList<UserRoleViewModel>> GetRolesForUser(ApplicationUser user)
    {
        var userRoles = await _userManager.GetRolesAsync(user);
        return _roleManager.Roles.Map(r => new UserRoleViewModel
        {
            Id = r.Id,
            Name = r.Name,
            Selected = userRoles.Any(c => c == r.Name)
        }).ToList();
    }
}

public record UserDetailsViewModel
{
    public string? Id { get; set; }

    [Display(Name = "Email")]
    public string Email { get; set; } = "";

    [Display(Name = "Full Name")]
    public string Name { get; set; } = "";

    [Display(Name = "Birth Date")]
    public string BirthDate { get; set; } = "";

    [Display(Name = "Entity")]
    public string Entity { get; set; } = "";

    [Display(Name = "Status")]
    public bool IsActive { get; set; }
    [Display(Name = "Group")]
    public string? Group { get; set; } = "";
    public string? GroupId { get; set; } = "";
}
