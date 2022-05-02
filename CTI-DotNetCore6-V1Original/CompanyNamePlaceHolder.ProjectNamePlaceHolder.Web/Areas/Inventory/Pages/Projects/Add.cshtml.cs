using CTI.Common.Utility.Extensions;
using CTI.Common.Web.Utility.Extensions;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.Inventory.Projects.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.Inventory;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Inventory.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Models;
using LanguageExt.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static LanguageExt.Prelude;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Inventory.Pages.Projects;

[Authorize(Policy = Permission.Projects.Create)]
public class AddModel : BasePageModel<AddModel>
{
    [BindProperty]
    public ProjectViewModel Project { get; set; } = new();

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
        return await TryAsync(async () => await Mediatr.Send(Mapper.Map<AddProjectCommand>(Project)))
            .IfFail(ex =>
            {
                Logger.LogError(ex, "Exception in OnPost");
                return Fail<Error, ProjectState>(Localizer[$"Something went wrong. Please contact the system administrator."] + $" TraceId = {HttpContext.TraceIdentifier}");
            }).ToActionResult(
            success: project =>
            {
                NotyfService.Success(Localizer["Record saved successfully"]);
                Logger.LogInformation("Added Record. ID: {ID}, Record: {Record}", project.Id, project.ToString());
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
