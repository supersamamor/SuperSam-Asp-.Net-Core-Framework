using CTI.Common.Web.Utility.Extensions;
using CTI.WebAppTemplate.Application.Features.Inventory.Projects.Queries;
using CTI.WebAppTemplate.Web.Areas.Inventory.Models;
using CTI.WebAppTemplate.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.WebAppTemplate.Web.Areas.Inventory.Pages.Projects;

[Authorize(Policy = Permission.Projects.View)]
public class DetailsModel : BasePageModel<DetailsModel>
{
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
}
