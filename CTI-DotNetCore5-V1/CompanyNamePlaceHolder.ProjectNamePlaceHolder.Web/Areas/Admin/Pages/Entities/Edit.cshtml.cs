using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Common.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Commands.Entities;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Queries.Entities;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Identity.Data;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Common.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static LanguageExt.Prelude;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Pages.Entities
{
    [Authorize(Policy = Permission.Entities.Edit)]
    public class EditModel : BasePageModel<EditModel>
    {
        [BindProperty]
        public EntityViewModel? Entity { get; set; }

        public async Task<IActionResult> OnGetDetailsAsync(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return await Mediatr.Send(new GetEntityByIdQuery(id))
                                .ToActionResult(e => Partial("_EditEntityDetails",
                                                             Mapper.Map<EntityViewModel>(e)), none: null);
        }

        public async Task<IActionResult> OnPostEditAsync()
        {
            if (!ModelState.IsValid)
            {
                return Partial("_EditEntityDetails", Entity);
            }
            var result = await TryAsync(async () => await Mediatr.Send(Mapper.Map<AddOrEditEntityCommand>(Entity)))
                .IfFail(ex =>
                {
                    Logger.LogError(ex, "Exception in OnPostEditAsync");
                    return Fail<Error, Entity>(Localizer[$"Something went wrong. Please contact the system administrator."] + $" TraceId = {HttpContext.TraceIdentifier}");
                });
            result.Match(
                Succ: succ =>
                {
                    NotyfService.Success(Localizer["Record saved successfully"]);
                    Logger.LogInformation("Updated Record. ID: {ID}, Record: {Record}", succ.Id, succ.ToString());
                },
                Fail: errors =>
                {
                    errors.Iter(errors => ModelState.AddModelError("", (string)errors));
                    Logger.LogError("Error in OnPostEditAsync. Errors: {Errors}", string.Join(",", errors));
                });
            return Partial("_EditEntityDetails", Entity);
        }
    }
}
