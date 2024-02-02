using CompanyPL.ProjectPL.Application.Features.ProjectPL.SampleParent.Queries;
using CompanyPL.ProjectPL.Web.Areas.ProjectPL.Models;
using CompanyPL.ProjectPL.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyPL.ProjectPL.Web.Areas.ProjectPL.Pages.SampleParent;

[Authorize(Policy = Permission.SampleParent.View)]
public class DetailsModel : BasePageModel<DetailsModel>
{
    public SampleParentViewModel SampleParent { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetSampleParentByIdQuery(id)), SampleParent);
    }
}
