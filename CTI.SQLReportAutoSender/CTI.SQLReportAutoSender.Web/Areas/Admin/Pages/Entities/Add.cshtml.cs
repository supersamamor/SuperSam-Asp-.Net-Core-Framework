using CTI.Common.Web.Utility.Extensions;
using CTI.SQLReportAutoSender.Web.Areas.Admin.Commands.Entities;
using CTI.SQLReportAutoSender.Web.Areas.Admin.Models;
using CTI.SQLReportAutoSender.Infrastructure.Data;
using CTI.SQLReportAutoSender.Web.Models;
using LanguageExt.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;
using CTI.SQLReportAutoSender.Core.Identity;

namespace CTI.SQLReportAutoSender.Web.Areas.Admin.Pages.Entities;

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
        return await Optional(await _context.Entities.FirstOrDefaultAsync(e => e.Name == Entity.Name))
            .MatchAsync(
            entity => Fail<Error, Entity>($"Entity with name {entity!.Name} already exists"),
            async () => await CreateEntity())
            .ToActionResult(
            success: entity =>
            {
                NotyfService.Success(Localizer["Record saved successfully"]);
                Logger.LogInformation("Added Record. ID: {ID}, Record: {Record}", entity.Id, entity.ToString());
                return RedirectToPage("View", new { id = entity.Id });
            },
            fail: errors =>
            {
                errors.Iter(error => ModelState.AddModelError("", error.ToString()));
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
