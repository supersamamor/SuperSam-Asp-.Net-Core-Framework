using CTI.Common.Web.Utility.Extensions;
using CTI.TenantSales.Web.Areas.Admin.Models;
using CTI.TenantSales.Web.Areas.Admin.Queries.Users;
using CTI.TenantSales.Web.Areas.Identity.Data;
using CTI.TenantSales.Web.Models;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Transactions;
using static CTI.TenantSales.Web.Areas.Identity.IdentityExtensions;
using CTI.TenantSales.Core.Identity;

namespace CTI.TenantSales.Web.Areas.Admin.Pages.Users;

[Authorize(Policy = Permission.Users.Edit)]
public class EditModel : BasePageModel<EditModel>
{
    readonly IdentityContext _context;
    readonly UserManager<ApplicationUser> _userManager;
    readonly RoleManager<IdentityRole> _roleManager;

    public EditModel(IdentityContext context,
                         UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [BindProperty]
    public UserEditViewModel Input { get; set; } = new();

    [BindProperty]
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
                                Input = await GetViewModel(user);
                                Roles = await GetRolesForUser(user);
                                return Page();
                            }, none: null);
    }

    async Task<UserEditViewModel> GetViewModel(ApplicationUser user)
    {
        var userModel = new UserEditViewModel
        {
            Id = user.Id,
            Email = user.Email,
            Name = user.Name!,
            BirthDate = user.BirthDate,
            EntityId = user.EntityId!,
            IsActive = user.IsActive,
        };
        userModel.Entities = await _context.GetEntitiesList(userModel.EntityId);
        return userModel;
    }

    public async Task<IActionResult> OnPost()
    {
        Input.Entities = await _context.GetEntitiesList(Input.EntityId);
        if (!ModelState.IsValid)
        {
            return Page();
        }
        return await Mediatr.Send(new GetUserByIdQuery(Input.Id))
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
                        ModelState.AddModelError("", Localizer[$"Something went wrong. Please contact the system administrator."] + $" TraceId = {HttpContext.TraceIdentifier}");
                        return Page();
                    });
            }, none: null);
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
        user.Name = Input.Name;
        user.BirthDate = Input.BirthDate;
        user.EntityId = Input.EntityId;
        user.IsActive = Input.IsActive;
        return await ToValidation<ApplicationUser>(_userManager.UpdateAsync)(user);
    }

    async Task<Validation<Error, ApplicationUser>> UpdateRolesForUser(ApplicationUser user) =>
        await _userManager.RemoveAllRoles(user)
                          .BindT(async u => await AddRolesToUser(u));

    async Task<Validation<Error, ApplicationUser>> AddRolesToUser(ApplicationUser user)
    {
        var roles = Roles.Where(r => r.Selected).Select(r => r.Name);
        return await _userManager.AddRoles(user, roles);
    }
}

public record UserEditViewModel
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Email { get; set; } = "";

    [Required]
    [DataType(DataType.Text)]
    [Display(Name = "Full name")]
    public string Name { get; set; } = "";

    [Required]
    [Display(Name = "Birth Date")]
    [DataType(DataType.Date)]
    public DateTime? BirthDate { get; set; }

    [Required]
    [Display(Name = "Entity")]
    public string EntityId { get; set; } = "";

    [Required]
    [Display(Name = "Status")]
    public bool IsActive { get; set; }

    public SelectList Entities { get; set; } = new(new List<SelectListItem>());
    public SelectList Statuses { get; set; } = AdminUtilities.GetUserStatusList();
}
