using CNPlaceHolder.PNPlaceHolder.Application.Features.PNPlaceHolder.TnamePlaceHolder.Queries;
using CNPlaceHolder.PNPlaceHolder.Web.Areas.PNPlaceHolder.Models;
using CNPlaceHolder.PNPlaceHolder.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CNPlaceHolder.PNPlaceHolder.Web.Areas.PNPlaceHolder.Pages.TnamePlaceHolder;

[Authorize(Policy = Permission.TnamePlaceHolder.View)]
public class DetailsModel : BasePageModel<DetailsModel>
{
    public TnamePlaceHolderViewModel TnamePlaceHolder { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetTnamePlaceHolderByIdQuery(id)), TnamePlaceHolder);
    }
}
