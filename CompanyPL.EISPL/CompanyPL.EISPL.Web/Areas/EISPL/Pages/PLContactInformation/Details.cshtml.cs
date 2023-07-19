using CompanyPL.EISPL.Application.Features.EISPL.PLContactInformation.Queries;
using CompanyPL.EISPL.Web.Areas.EISPL.Models;
using CompanyPL.EISPL.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyPL.EISPL.Web.Areas.EISPL.Pages.PLContactInformation;

[Authorize(Policy = Permission.PLContactInformation.View)]
public class DetailsModel : BasePageModel<DetailsModel>
{
    public PLContactInformationViewModel PLContactInformation { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetPLContactInformationByIdQuery(id)), PLContactInformation);
    }
}
