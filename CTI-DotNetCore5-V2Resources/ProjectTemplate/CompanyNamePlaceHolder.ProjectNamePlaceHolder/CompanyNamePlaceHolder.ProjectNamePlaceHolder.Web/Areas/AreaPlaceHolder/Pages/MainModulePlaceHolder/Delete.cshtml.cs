using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Common.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Commands.Entities;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Queries.Entities;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Identity.Data;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.AreaPlaceHolder.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static LanguageExt.Prelude;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.MainModulePlaceHolder.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.MainModulePlaceHolder.Queries;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.AreaPlaceHolder.Pages.MainModulePlaceHolder
{
    [Authorize(Policy = Permission.MainModulePlaceHolder.Delete)]
    public class DeleteModel : BaseAreaPlaceHolderPageModel<DeleteModel>
    {
        [BindProperty]
        public MainModulePlaceHolderViewModel MainModulePlaceHolder { get; set; } = new();

        public async Task<IActionResult> OnGet(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return await Mediatr.Send(new GetMainModulePlaceHolderByIdQuery(id)).ToActionResult(
                p =>
                {
                    Mapper.Map(p, MainModulePlaceHolder);
                    return Page();
                },
                none: null);
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var result = await TryAsync(async () => await Mediatr.Send(new DeleteMainModulePlaceHolderCommand { Id = MainModulePlaceHolder.Id }))
                .IfFail(ex =>
                {
                    Logger.LogError(ex, "Exception in OnPostDeleteAsync");
                    return Fail<Error, Core.AreaPlaceHolder.MainModulePlaceHolder>(Localizer[$"Something went wrong. Please contact the system administrator."] + $" TraceId = {HttpContext.TraceIdentifier}");
                });
            return result.Match<IActionResult>(
                Succ: succ =>
                {
                    NotyfService.Success(Localizer["Record deleted successfully"]);
                    Logger.LogInformation("Deleted Record. ID: {ID}, Record: {Record}", succ.Id, succ.ToString());
                    return RedirectToPage("Index");
                },
                Fail: errors =>
                {
                    errors.Iter(errors => ModelState.AddModelError("", (string)errors));
                    Logger.LogError("Error in OnPostDeleteAsync. Errors: {Errors}", errors.Join());
                    return Page();
                });
        }
    }
}
