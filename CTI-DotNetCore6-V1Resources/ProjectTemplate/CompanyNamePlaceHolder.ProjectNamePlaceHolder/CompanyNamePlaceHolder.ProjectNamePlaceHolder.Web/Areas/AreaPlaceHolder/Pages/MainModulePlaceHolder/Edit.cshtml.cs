using CTI.Common.Utility.Extensions;
using CTI.Common.Web.Utility.Extensions;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.MainModulePlaceHolder.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.MainModulePlaceHolder.Queries;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.AreaPlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.AreaPlaceHolder.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Models;
using LanguageExt.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static LanguageExt.Prelude;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.AreaPlaceHolder.Pages.MainModulePlaceHolder;

[Authorize(Policy = Permission.MainModulePlaceHolder.Edit)]
public class EditModel : BasePageModel<EditModel>
{
    [BindProperty]
    public MainModulePlaceHolderViewModel Project { get; set; } = new();

    public async Task<IActionResult> OnGet(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        return await Mediatr.Send(new GetMainModulePlaceHolderByIdQuery(id)).ToActionResult(
            e =>
            {
                Mapper.Map(e, Project);
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
        return await TryAsync(async () => await Mediatr.Send(Mapper.Map<EditMainModulePlaceHolderCommand>(Project)))
            .IfFail(ex =>
            {
                Logger.LogError(ex, "Exception in OnPost");
                return Fail<Error, MainModulePlaceHolderState>(Localizer[$"Something went wrong. Please contact the system administrator."] + $" TraceId = {HttpContext.TraceIdentifier}");
            }).ToActionResult(
            success: project =>
            {
                NotyfService.Success(Localizer["Record saved successfully"]);
                Logger.LogInformation("Edited Record. ID: {ID}, Record: {Record}", project.Id, project.ToString());
                return RedirectToPage("Details", new { id = project.Id });
            },
            fail: errors =>
            {
                errors.Iter(errors => ModelState.AddModelError("", errors.ToString()));
                Logger.LogError("Error in OnPost. Errors: {Errors}", errors.Join().ToString());
                return Page();
            });
    }
}
