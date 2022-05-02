using CTI.Common.Utility.Extensions;
using CTI.Common.Web.Utility.Extensions;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.Inventory.Projects.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.Inventory.Projects.Queries;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.Inventory;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Inventory.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Models;
using LanguageExt.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static LanguageExt.Prelude;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Inventory.Pages.Projects;

[Authorize(Policy = Permission.Projects.Delete)]
public class DeleteModel : BasePageModel<DeleteModel>
{
    [BindProperty]
    public ProjectViewModel Project { get; set; } = new();

    public async Task<IActionResult> OnGet(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        return await Mediatr.Send(new GetProjectByIdQuery(id)).ToActionResult(
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
        return await TryAsync(async () => await Mediatr.Send(new DeleteProjectCommand { Id = Project.Id }))
            .IfFail(ex =>
            {
                Logger.LogError(ex, "Exception in OnPost");
                return Fail<Error, ProjectState>(Localizer[$"Something went wrong. Please contact the system administrator."] + $" TraceId = {HttpContext.TraceIdentifier}");
            }).ToActionResult(
            success: succ =>
            {
                NotyfService.Success(Localizer["Record deleted successfully"]);
                Logger.LogInformation("Deleted Record. ID: {ID}, Record: {Record}", succ.Id, succ.ToString());
                return RedirectToPage("Index");
            },
            fail: errors =>
            {
                errors.Iter(error => ModelState.AddModelError("", error.ToString()));
                Logger.LogError("Error in OnPost. Errors: {Errors}", errors.Join().ToString());
                return Page();
            });
    }
}
