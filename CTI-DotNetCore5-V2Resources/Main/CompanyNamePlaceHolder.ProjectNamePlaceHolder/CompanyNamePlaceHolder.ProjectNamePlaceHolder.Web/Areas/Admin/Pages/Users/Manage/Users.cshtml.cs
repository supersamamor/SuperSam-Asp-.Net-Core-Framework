using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Queries.Users;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Identity.Data;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Common.Extensions;
using LanguageExt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static LanguageExt.Prelude;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Pages.Users.Manage
{
    [Authorize(Policy = Permission.Users.View)]
    public class UsersModel : BasePageModel<UsersModel>
    {
        private readonly IdentityContext _context;

        public UsersModel(IdentityContext context)
        {
            _context = context;
        }

        [BindProperty]
        public UserDetailsViewModel? UserDetails { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Id { get; set; }

        public async Task<IActionResult> OnGet()
        {
            if (Id == null)
            {
                return NotFound();
            }
            return await Mediatr.Send(new GetUserByIdQuery(Id))
                                .MapAsync(async u => await ToViewModel(u))
                                .ToOption()
                                .ToActionResult(m =>
                                {
                                    UserDetails = m;
                                    return Page();
                                }, none: null);
        }

        Func<ApplicationUser, Task<UserDetailsViewModel>> ToViewModel => async user =>
           {
               return await GetEntityName(user.EntityId!).Match(
                   entity => new UserDetailsViewModel
                   {
                       Id = user.Id,
                       Name = user.Name,
                       BirthDate = user.BirthDate!.Value.ToString("MMMM d, yyyy"),
                       Email = user.Email,
                       Entity = entity,
                       IsActive = user.IsActive,
                   },
                   () => new UserDetailsViewModel
                   {
                       Id = user.Id,
                       Name = user.Name,
                       BirthDate = user.BirthDate!.Value.ToString("MMMM d, yyyy"),
                       Email = user.Email,
                       Entity = "Default",
                       IsActive = user.IsActive,
                   });
           };

        async Task<Option<string>> GetEntityName(string id) =>
            await _context.Entities.Where(e => e.Id == id).Select(e => e.Name).FirstOrDefaultAsync();
    }

    public record UserDetailsViewModel
    {
        public string? Id { get; set; }

        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Display(Name = "Full Name")]
        public string? Name { get; set; }

        [Display(Name = "Birth Date")]
        public string? BirthDate { get; set; }

        [Display(Name = "Entity")]
        public string? Entity { get; set; }

        [Display(Name = "Status")]
        public bool IsActive { get; set; }
    }
}
