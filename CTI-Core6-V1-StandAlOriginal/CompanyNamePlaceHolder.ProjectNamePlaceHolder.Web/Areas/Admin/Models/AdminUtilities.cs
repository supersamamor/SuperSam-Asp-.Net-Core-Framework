using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Identity.Data;
using LanguageExt;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static CTI.Common.Utility.Helpers.OptionHelper;
using static LanguageExt.Prelude;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.Admin.Models;

public static class AdminUtilities
{
    public static async Task<SelectList> GetEntitiesList(this IdentityContext _context, string selectedEntityId) =>
        new SelectList(await _context.Entities.Select(e => new SelectListItem { Value = e.Id, Text = e.Name })
                                              .ToListAsync(), "Value", "Text", selectedEntityId);

    public static async Task<LanguageExt.Option<string>> GetEntityName(this IdentityContext _context, string entityId) =>
        ToOption(await _context.Entities.Where(e => e.Id == entityId).Select(e => e.Name).FirstOrDefaultAsync());

    public static SelectList GetUserStatusList()
    {
        var statuses = List(
            new SelectListItem() { Value = "true", Text = "Active" },
            new SelectListItem() { Value = "false", Text = "Inactive" });
        return new SelectList(statuses, "Value", "Text");
    }
}

