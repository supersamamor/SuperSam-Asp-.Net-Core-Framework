using CTI.Common.Web.Utility.Extensions;
using CTI.FAS.Web.Areas.Admin.Commands.Group;
using CTI.FAS.Web.Areas.Admin.Models;
using CTI.FAS.Infrastructure.Data;
using CTI.FAS.Web.Models;
using LanguageExt.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;
using CTI.FAS.Core.Identity;

namespace CTI.FAS.Web.Areas.Admin.Pages.Group;

[Authorize(Policy = Permission.Group.Create)]
public class AddModel : BasePageModel<AddModel>
{
    private readonly IdentityContext _context;

    public AddModel(IdentityContext context)
    {
        _context = context;
    }

    [BindProperty]
    public GroupViewModel Group { get; set; } = new();

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
        return await Optional(await _context.Group.FirstOrDefaultAsync(e => e.Name == Group.Name))
            .MatchAsync(
            group => Fail<Error, Core.Identity.Group>($"Group with name {group!.Name} already exists"),
            async () => await CreateGroup())
            .ToActionResult(
            success: group =>
            {
                NotyfService.Success(Localizer["Record saved successfully"]);
                Logger.LogInformation("Added Record. ID: {ID}, Record: {Record}", group.Id, group.ToString());
                return RedirectToPage("View", new { id = group.Id });
            },
            fail: errors =>
            {
                errors.Iter(error => ModelState.AddModelError("", error.ToString()));
                Logger.LogError("Error in OnPost. Errors: {Errors}", string.Join(",", errors));
                return Page();
            });
    }

    async Task<LanguageExt.Validation<Error, Core.Identity.Group>> CreateGroup()
    {
        return await TryAsync(async () => await Mediatr.Send(Mapper.Map<AddOrEditGroupCommand>(Group)))
                        .IfFail(ex =>
                        {
                            Logger.LogError(ex, "Exception in OnPost");
                            return Fail<Error, Core.Identity.Group>(Localizer[$"Something went wrong. Please contact the system administrator."] + $" TraceId = {HttpContext.TraceIdentifier}");
                        });
    }
}
