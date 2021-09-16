using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Common.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Commands.Entities;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Queries.Entities;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static LanguageExt.Prelude;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Pages.Entities
{
    [Authorize(Policy = Permission.Entities.Create)]
    public class AddModel : BasePageModel<AddModel>
    {
        private readonly IdentityContext _context;

        public AddModel(IdentityContext context)
        {
            _context = context;
        }

        [BindProperty]
        public EntityViewModel Entity { get; set; } = new();

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var result = await Optional(await _context.Entities.FirstOrDefaultAsync(e => e.Name == Entity.Name))
                .MatchAsync(
                entity => Fail<Error, Entity>($"Entity with name {entity!.Name} already exists"),
                async () => await CreateEntity()
                );
            return result.Match<IActionResult>(
                Succ: entity =>
                {
                    NotyfService.Success(Localizer["Record saved successfully"]);
                    Logger.LogInformation("Added Record. ID: {ID}, Record: {Record}", entity.Id, entity.ToString());
                    return RedirectToPage("View", new { id = entity.Id });
                },
                Fail: errors =>
                {
                    errors.Iter(errors => ModelState.AddModelError("", (string)errors));
                    Logger.LogError("Error in OnPost. Errors: {Errors}", string.Join(",", errors));
                    return Page();
                });
        }

        async Task<LanguageExt.Validation<Error, Entity>> CreateEntity()
        {
            return await TryAsync(async () => await Mediatr.Send(Mapper.Map<AddOrEditEntityCommand>(Entity)))
                            .IfFail(ex =>
                            {
                                Logger.LogError(ex, "Exception in OnPost");
                                return Fail<Error, Entity>(Localizer[$"Something went wrong. Please contact the system administrator."] + $" TraceId = {HttpContext.TraceIdentifier}");
                            });
        }
    }
}
