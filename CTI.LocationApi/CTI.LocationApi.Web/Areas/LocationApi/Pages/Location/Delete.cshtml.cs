using CTI.LocationApi.Application.Features.LocationApi.Location.Commands;
using CTI.LocationApi.Application.Features.LocationApi.Location.Queries;
using CTI.LocationApi.Web.Areas.LocationApi.Models;
using CTI.LocationApi.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.LocationApi.Web.Areas.LocationApi.Pages.Location;

[Authorize(Policy = Permission.Location.Delete)]
public class DeleteModel : BasePageModel<DeleteModel>
{
    [BindProperty]
    public LocationViewModel Location { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetLocationByIdQuery(id)), Location);
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        return await TryThenRedirectToPage(async () => await Mediatr.Send(new DeleteLocationCommand { Id = Location.Id }), "Index");
    }
}
