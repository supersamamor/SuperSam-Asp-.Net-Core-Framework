using CTI.ELMS.Application.Features.ELMS.IFCATenantInformation.Queries;
using CTI.ELMS.Web.Areas.ELMS.Models;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.ELMS.Web.Areas.ELMS.Pages.IFCATenantInformation;

[Authorize(Policy = Permission.IFCATenantInformation.View)]
public class DetailsModel : BasePageModel<DetailsModel>
{
    public IFCATenantInformationViewModel IFCATenantInformation { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetIFCATenantInformationByIdQuery(id)), IFCATenantInformation);
    }
}
