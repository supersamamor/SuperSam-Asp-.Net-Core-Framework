using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Common.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Identity.Data;
using LanguageExt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Identity.IdentityExtensions;
using static LanguageExt.Prelude;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Pages.Users
{
    [Authorize(Policy = Permission.Users.Create)]
    public class AddModel : BasePageModel<AddModel>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IdentityContext _context;

        public AddModel(UserManager<ApplicationUser> userManager, IdentityContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [BindProperty]
        public AddViewModel Input { get; set; } = new AddViewModel();

        public SelectList? Entities { get; set; }

        public async Task<IActionResult> OnGetAddAsync()
        {
            var items = await GetEntities();
            Entities = new SelectList(items, "Value", "Text");
            return Page();
        }

        public async Task<IActionResult> OnPostAddAsync()
        {
            var items = await GetEntities();
            Entities = new SelectList(items, "Value", "Text");
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var user = await Optional(await _userManager.FindByEmailAsync(Input!.Email))
                .MatchAsync(
                    Some: user => Fail<Error, ApplicationUser>($"User with email {user.Email} already exists"),
                    None: async () => await CreateUserAsync());
            return user.Match<IActionResult>(
                Succ: user =>
                {
                    NotyfService.Success(Localizer["Record saved successfully"]);
                    Logger.LogInformation("Created User. Email: {Email}, User: {User}", user.Email, user.ToString());
                    return RedirectToPage("Manage/Index", new { id = user.Id });
                },
                Fail: errors =>
                {
                    errors.Iter(errors => ModelState.AddModelError("", (string)errors));
                    Logger.LogError("Error in OnPostAddAsync. Error: {Errors}", string.Join(",", errors));
                    return Page();
                });
        }

        async Task<IList<SelectListItem>> GetEntities() =>
            await _context.Entities.Select(e => new SelectListItem { Value = e.Id, Text = e.Name })
                                   .ToListAsync();

        async Task<Validation<Error, ApplicationUser>> CreateUserAsync()
        {
            return await TryAsync(async () =>
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
                    Logger.LogError("Error in OnPostAddAsync. Error: {Errors}", string.Join(",", result.Errors.Select(e => e.Description)));
                    return Fail<Error, ApplicationUser>(Localizer[$"Something went wrong. Please contact the system administrator."] + $" TraceId = {HttpContext.TraceIdentifier}");
                }
                return user;
            }).IfFail(ex =>
            {
                Logger.LogError(ex, "Exception in OnPostAddAsync");
                return Fail<Error, ApplicationUser>(Localizer[$"Something went wrong. Please contact the system administrator."] + $" TraceId = {HttpContext.TraceIdentifier}");
            });
        }
    }

    public record AddViewModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Full name")]
        public string? Name { get; set; }

        [Required]
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        [Required]
        [Display(Name = "Entity")]
        public string? EntityId { get; set; }
    }
}
