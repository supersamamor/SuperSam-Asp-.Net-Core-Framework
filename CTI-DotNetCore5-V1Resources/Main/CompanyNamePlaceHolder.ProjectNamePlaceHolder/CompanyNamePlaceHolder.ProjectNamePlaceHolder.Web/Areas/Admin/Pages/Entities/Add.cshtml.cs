using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Common.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Commands.Entities;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Queries.Entities;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
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
        public EntityViewModel? Entity { get; set; }

        public IActionResult OnGetAdd()
        {
            return Partial("_AddEntityDetails", new EntityViewModel());
        }

        public async Task<IActionResult> OnPostAddAsync()
        {
            if (!ModelState.IsValid)
            {
                return Partial("_AddEntityDetails", Entity);
            }
            var result = await Optional(await _context.Entities.FirstOrDefaultAsync(e => e.Name == Entity!.Name))
                .MatchAsync(
                scope => Fail<Error, Entity>($"Entity with name {scope!.Name} already exists"),
                async () => await CreateEntity()
                );
            result.Match(
                Succ: succ =>
                {
                    NotyfService.Success(Localizer["Record saved successfully"]);
                    Logger.LogInformation("Added Record. ID: {ID}, Record: {Record}", succ.Id, succ.ToString());
                },
                Fail: errors =>
                {
                    errors.Iter(errors => ModelState.AddModelError("", (string)errors));
                    Logger.LogError("Error in OnPostAddAsync. Errors: {Errors}", string.Join(",", errors));
                });
            return Partial("_AddEntityDetails", Entity);
        }

        async Task<LanguageExt.Validation<Error, Entity>> CreateEntity()
        {
            return await TryAsync(async () => await Mediatr.Send(Mapper.Map<AddOrEditEntityCommand>(Entity)))
                            .IfFail(ex =>
                            {
                                Logger.LogError(ex, "Exception in OnPostAddAsync");
                                return Fail<Error, Entity>(Localizer[$"Something went wrong. Please contact the system administrator."] + $" TraceId = {HttpContext.TraceIdentifier}");
                            });
        }
    }
}
