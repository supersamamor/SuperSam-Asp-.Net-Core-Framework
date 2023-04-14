using CelerSoft.Common.Web.Utility.Extensions;
using CelerSoft.TurboERP.Web.Areas.Admin.Models;
using CelerSoft.TurboERP.Infrastructure.Data;
using CelerSoft.TurboERP.Web.Models;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Transactions;
using static CelerSoft.TurboERP.Web.Areas.Identity.IdentityExtensions;
using static LanguageExt.Prelude;
using CelerSoft.TurboERP.Core.Identity;

namespace CelerSoft.TurboERP.Web.Areas.Admin.Pages.Users;

[Authorize(Policy = Permission.Users.Create)]
public class AddModel : BasePageModel<AddModel>
{
    readonly UserManager<ApplicationUser> _userManager;
    readonly IdentityContext _context;
    readonly RoleManager<IdentityRole> _roleManager;

    public AddModel(UserManager<ApplicationUser> userManager, IdentityContext context, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _context = context;
        _roleManager = roleManager;
    }

    [BindProperty]
    public AddViewModel Input { get; set; } = new();

    [BindProperty]
    public IList<UserRoleViewModel> Roles { get; set; } = new List<UserRoleViewModel>();

    public async Task<IActionResult> OnGetAsync()
    {
        Input.Entities = await _context.GetEntitiesList(Input.EntityId);
        Roles = GetRoles();
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        Input.Entities = await _context.GetEntitiesList(Input.EntityId);
        if (!ModelState.IsValid)
        {
            return Page();
        }
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        return await Optional(await _userManager.FindByEmailAsync(Input.Email))
            .MatchAsync(
                Some: user => Fail<Error, ApplicationUser>($"User with email {user.Email} already exists"),
                None: async () => await CreateUserAsync()).BindT(async u => await AddRolesToUser(u))
            .ToActionResult(
            success: user =>
            {
                scope.Complete();
                Logger.LogInformation("Created User. Email: {Email}, User: {User}", user.Email, user.ToString());
                NotyfService.Success(Localizer["Record saved successfully"]);
                return RedirectToPage("View", new { id = user.Id });
            },
            fail: errors =>
            {
                Logger.LogError("Error in OnPost. Error: {Errors}", string.Join(",", errors));
                ModelState.AddModelError("", Localizer[$"Something went wrong. Please contact the system administrator."] + $" TraceId = {HttpContext.TraceIdentifier}");
                return Page();
            });
    }

    IList<UserRoleViewModel> GetRoles()
    {
        return _roleManager.Roles.Map(r => new UserRoleViewModel
        {
            Id = r.Id,
            Name = r.Name,
        }).ToList();
    }

    async Task<Validation<Error, ApplicationUser>> CreateUserAsync()
    {
        return await TryAsync<Validation<Error, ApplicationUser>>(async () =>
        {
            var user = new ApplicationUser
            {
                UserName = Input.Email,
                Email = Input.Email,
                Name = Input.Name,
                BirthDate = Input.BirthDate,
                EntityId = Input.EntityId,
                IsActive = true,
                EmailConfirmed = true,
            };
            var result = await _userManager.CreateAsync(user, Input.Password);
            if (!result.Succeeded)
            {
                return result.Errors.Select(e => e.Description).Map(e => Error.New(e)).ToSeq();
            }
            return user;
        }).IfFail(ex =>
        {
            Logger.LogError(ex, "Exception in OnPost");
            return Error.New(ex.Message);
        });
    }

    async Task<Validation<Error, ApplicationUser>> AddRolesToUser(ApplicationUser user)
    {
        var roles = Roles.Where(r => r.Selected).Select(r => r.Name);
        return await _userManager.AddRoles(user, roles);
    }
}

public record AddViewModel
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; } = "";

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; } = "";

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; } = "";

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
    public string EntityId { get; set; } = Guid.NewGuid().ToString();

    public SelectList Entities { get; set; } = new(new List<SelectListItem>());
}
