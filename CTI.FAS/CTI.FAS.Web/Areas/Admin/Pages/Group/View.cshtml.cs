using CTI.Common.Web.Utility.Extensions;
using CTI.FAS.Web.Areas.Admin.Models;
using CTI.FAS.Web.Areas.Admin.Queries.Group;
using CTI.FAS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.FAS.Web.Areas.Admin.Pages.Group;

[Authorize(Policy = Permission.Group.View)]
public class ViewModel : BasePageModel<ViewModel>
{
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
}
