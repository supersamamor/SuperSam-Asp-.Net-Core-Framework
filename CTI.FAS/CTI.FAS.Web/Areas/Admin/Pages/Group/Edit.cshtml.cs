using CTI.Common.Web.Utility.Extensions;
using CTI.FAS.Web.Areas.Admin.Commands.Group;
using CTI.FAS.Web.Areas.Admin.Models;
using CTI.FAS.Web.Areas.Admin.Queries.Group;
using CTI.FAS.Web.Models;
using LanguageExt.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static LanguageExt.Prelude;
using CTI.FAS.Core.Identity;

namespace CTI.FAS.Web.Areas.Admin.Pages.Group;

[Authorize(Policy = Permission.Group.Edit)]
public class EditModel : BasePageModel<EditModel>
{
    [BindProperty]
    public GroupViewModel Group { get; set; } = new();

    public async Task<IActionResult> OnGet(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        return await Mediatr.Send(new GetGroupByIdQuery(id)).ToActionResult(
            e =>
            {
                Mapper.Map(e, Group);
                return Page();
            }, none: null);
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        return await TryAsync(async () => await Mediatr.Send(Mapper.Map<AddOrEditGroupCommand>(Group)))
            .IfFail(ex =>
            {
                Logger.LogError(ex, "Exception in OnPost");
                return Fail<Error, Core.Identity.Group>(Localizer[$"Something went wrong. Please contact the system administrator."] + $" TraceId = {HttpContext.TraceIdentifier}");
            }).ToActionResult(
            success: group =>
            {
                NotyfService.Success(Localizer["Record saved successfully"]);
                Logger.LogInformation("Updated Record. ID: {ID}, Record: {Record}", group.Id, group.ToString());
                return RedirectToPage("View", new { id = group.Id });
            },
            fail: errors =>
            {
                errors.Iter(error => ModelState.AddModelError("", error.ToString()));
                Logger.LogError("Error in OnPost. Errors: {Errors}", string.Join(",", errors));
                return Page();
            });
    }
}
