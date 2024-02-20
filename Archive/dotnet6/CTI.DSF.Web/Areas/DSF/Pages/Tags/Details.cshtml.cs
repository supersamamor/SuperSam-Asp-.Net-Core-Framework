using CTI.DSF.Application.Features.DSF.Tags.Queries;
using CTI.DSF.Web.Areas.DSF.Models;
using CTI.DSF.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.DSF.Web.Areas.DSF.Pages.Tags;

[Authorize(Policy = Permission.Tags.View)]
public class DetailsModel : BasePageModel<DetailsModel>
{
    public TagsViewModel Tags { get; set; } = new();
	[BindProperty]
    public string? RemoveSubDetailId { get; set; }
    [BindProperty]
    public string? AsyncAction { get; set; }
    public async Task<IActionResult> OnGet(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        return await PageFrom(async () => await Mediatr.Send(new GetTagsByIdQuery(id)), Tags);
    }
}
