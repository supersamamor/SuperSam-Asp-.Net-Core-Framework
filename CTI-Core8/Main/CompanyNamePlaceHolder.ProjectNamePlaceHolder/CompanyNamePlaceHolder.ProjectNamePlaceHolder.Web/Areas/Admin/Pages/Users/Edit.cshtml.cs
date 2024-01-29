using CompanyNamePlaceHolder.Common.Web.Utility.Extensions;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Queries.Users;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Models;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;
using static CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Identity.IdentityExtensions;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.Identity;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Pages.Users;

[Authorize(Policy = Permission.Users.Edit)]
public class EditModel : BasePageModel<EditModel>
{
    readonly IdentityContext _context;
    readonly UserManager<ApplicationUser> _userManager;
    readonly RoleManager<ApplicationRole> _roleManager;

    public EditModel(IdentityContext context,
                         UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [BindProperty]
    public UserViewModel UserModel { get; set; } = new() { IsEdit = true };
    public async Task<IActionResult> OnGet(string id)
    {
        if (id == null)
        {
            return NotFound();
        }
        return await Mediatr.Send(new GetUserByIdQuery(id))
                            .ToActionResult(async user =>
                            {
                                UserModel = await GetViewModel(user);
                                UserModel.Roles = await GetRolesForUser(user);
                                return Page();
                            }, none: null);
    }

    async Task<UserViewModel> GetViewModel(ApplicationUser user)
    {
        var userModel = new UserViewModel
        {
            Id = user.Id,
            Email = user.Email,
            Name = user.Name!,         
            EntityId = user.EntityId!,
            IsActive = user.IsActive,
            IsEdit = true,
        };
        userModel.Entities = await _context.GetEntitiesList(userModel.EntityId);
        return userModel;
    }

    public async Task<IActionResult> OnPost()
    {
        UserModel.Entities = await _context.GetEntitiesList(UserModel.EntityId);
        if (!ModelState.IsValid)
        {
            return Page();
        }
        return await Mediatr.Send(new GetUserByIdQuery(UserModel.Id))
            .ToActionResult(
            async user =>
            {
                using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
                return await UpdateUser(user).BindT(async u => await UpdateRolesForUser(u))
                .ToActionResult(
                    user =>
                    {
                        scope.Complete();
                        Logger.LogInformation("Updated User. ID: {ID}, User: {User}", user.Id, user.ToString());
                        NotyfService.Success(Localizer["Record saved successfully"]);
                        return RedirectToPage("View", new { id = user.Id });
                    },
                    errors =>
                    {
                        Logger.LogError("Error in OnPost. Error: {Errors}", string.Join(",", errors));
                        errors.Iter(error => ModelState.AddModelError("", error.ToString()));
                        return Page();
                    });
            }, none: null);
    }
    public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();

        return Partial("_InputFieldsPartial", UserModel);
    }
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

    async Task<Validation<Error, ApplicationUser>> UpdateUser(ApplicationUser user)
    {
        user.Name = UserModel.Name;    
        user.EntityId = UserModel.EntityId;
        user.IsActive = UserModel.IsActive;
        return await ToValidation<ApplicationUser>(_userManager.UpdateAsync)(user);
    }

    async Task<Validation<Error, ApplicationUser>> UpdateRolesForUser(ApplicationUser user) =>
        await _userManager.RemoveAllRoles(user)
                          .BindT(async u => await AddRolesToUser(u));

    async Task<Validation<Error, ApplicationUser>> AddRolesToUser(ApplicationUser user)
    {
        var roles = UserModel.Roles.Where(r => r.Selected).Select(r => r.Name);
        return await _userManager.AddRoles(user, roles);
    }
}

