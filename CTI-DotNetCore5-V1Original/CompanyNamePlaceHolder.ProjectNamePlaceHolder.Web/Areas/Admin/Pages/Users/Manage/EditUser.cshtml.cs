using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Queries.Users;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Identity.Data;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Common.Extensions;
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

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Pages.Users.Manage
{
    [Authorize(Policy = Permission.Users.Edit)]
    public class EditUserModel : BasePageModel<EditUserModel>
    {
        private readonly IdentityContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public EditUserModel(IdentityContext context,
                             UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public UserEditViewModel? Input { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Id { get; set; }

        public SelectList? Entities { get; set; }
        public SelectList? Statuses { get; set; }

        public async Task<IActionResult> OnGet()
        {
            if (Id == null)
            {
                return NotFound();
            }
            var entities = await GetEntities();
            var statuses = new List<SelectListItem>
            {
                new() { Value = "true", Text = "Active" } ,
                new() { Value = "false", Text = "Deactivated" }
            };
            return await Mediatr.Send(new GetUserByIdQuery(Id))
                                .ToActionResult(user =>
                                {
                                    Entities = new SelectList(entities, "Value", "Text", user.EntityId);
                                    Statuses = new SelectList(statuses, "Value", "Text", user.IsActive);
                                    Input = new UserEditViewModel
                                    {
                                        Id = user.Id,
                                        Name = user.Name,
                                        EntityId = user.EntityId,
                                        IsActive = user.IsActive,
                                    };
                                    return Page();
                                }, none: null);
        }


        public async Task<IActionResult> OnPostAsync()
        {
            var entities = await GetEntities();
            var statuses = new List<SelectListItem>
            {
                new() { Value = "true", Text = "Active" } ,
                new() { Value = "false", Text = "Deactivated" }
            };
            Entities = new SelectList(entities, "Value", "Text", Input!.EntityId);
            Statuses = new SelectList(statuses, "Value", "Text", Input!.IsActive);

            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (Input!.Id == null)
            {
                return NotFound();
            }

            return await Mediatr.Send(new GetUserByIdQuery(Input.Id))
                                .ToActionResult(async user => await UpdateUser(user), none: null);
        }

        async Task<IList<SelectListItem>> GetEntities() =>
            await _context.Entities.Select(e => new SelectListItem { Value = e.Id, Text = e.Name })
                                   .ToListAsync();


        Func<ApplicationUser, Task<IActionResult>> UpdateUser => async user =>
        {
            user.IsActive = Input!.IsActive;
            user.EntityId = Input!.EntityId;
            return (await ToValidation<ApplicationUser>(_userManager.UpdateAsync)(user))
            .Match<IActionResult>(user =>
            {
                NotyfService.Success(Localizer["Record saved successfully"]);
                Logger.LogInformation("Updated User. ID: {ID}, User: {User}", user.Id, user.ToString());
                return Redirect($"/Admin/Users/{Input!.Id}");
            },
            errors =>
            {
                ModelState.AddModelError("", Localizer[$"Something went wrong. Please contact the system administrator."] + $" TraceId = {HttpContext.TraceIdentifier}");
                Logger.LogError("Error in OnPostAsync. Error: {Errors}", string.Join(",", errors));
                return Page();
            });
        };
    }

    public record UserEditViewModel
    {
        public string? Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string? Name { get; set; }

        [Required]
        [Display(Name = "Entity")]
        public string? EntityId { get; set; }

        [Required]
        [Display(Name = "Status")]
        public bool IsActive { get; set; }
    }
}
