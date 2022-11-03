using CTI.ELMS.Application.Features.ELMS.IFCAUnitInformation.Commands;
using CTI.ELMS.Application.Features.ELMS.IFCAUnitInformation.Queries;
using CTI.ELMS.Web.Areas.ELMS.Models;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.ELMS.Web.Areas.ELMS.Pages.IFCAUnitInformation;

[Authorize(Policy = Permission.IFCAUnitInformation.Delete)]
public class DeleteModel : BasePageModel<DeleteModel>
{
    [BindProperty]
    public IFCAUnitInformationViewModel IFCAUnitInformation { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetIFCAUnitInformationByIdQuery(id)), IFCAUnitInformation);
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        return await TryThenRedirectToPage(async () => await Mediatr.Send(new DeleteIFCAUnitInformationCommand { Id = IFCAUnitInformation.Id }), "Index");
    }
}
