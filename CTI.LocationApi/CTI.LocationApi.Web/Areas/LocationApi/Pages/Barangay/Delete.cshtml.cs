using CTI.LocationApi.Application.Features.LocationApi.Barangay.Commands;
using CTI.LocationApi.Application.Features.LocationApi.Barangay.Queries;
using CTI.LocationApi.Web.Areas.LocationApi.Models;
using CTI.LocationApi.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.LocationApi.Web.Areas.LocationApi.Pages.Barangay;

[Authorize(Policy = Permission.Barangay.Delete)]
public class DeleteModel : BasePageModel<DeleteModel>
{
    [BindProperty]
    public BarangayViewModel Barangay { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetBarangayByIdQuery(id)), Barangay);
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        return await TryThenRedirectToPage(async () => await Mediatr.Send(new DeleteBarangayCommand { Id = Barangay.Id }), "Index");
    }
}
