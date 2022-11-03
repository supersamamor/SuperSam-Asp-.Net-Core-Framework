using CTI.Common.Web.Utility.Extensions;
using CTI.ELMS.Web.Areas.Admin.Models;
using CTI.ELMS.Web.Areas.Admin.Queries.Entities;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.ELMS.Web.Areas.Admin.Pages.Entities;

[Authorize(Policy = Permission.Entities.View)]
public class ViewModel : BasePageModel<ViewModel>
{
    public EntityViewModel Entity { get; set; } = new();

    public async Task<IActionResult> OnGet(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        return await Mediatr.Send(new GetEntityByIdQuery(id)).ToActionResult(
            e =>
            {
                Mapper.Map(e, Entity);
                return Page();
            }, none: null);
    }
}
