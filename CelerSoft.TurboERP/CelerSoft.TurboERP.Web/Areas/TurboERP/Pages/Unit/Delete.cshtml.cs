using CelerSoft.TurboERP.Application.Features.TurboERP.Unit.Commands;
using CelerSoft.TurboERP.Application.Features.TurboERP.Unit.Queries;
using CelerSoft.TurboERP.Web.Areas.TurboERP.Models;
using CelerSoft.TurboERP.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CelerSoft.TurboERP.Web.Areas.TurboERP.Pages.Unit;

[Authorize(Policy = Permission.Unit.Delete)]
public class DeleteModel : BasePageModel<DeleteModel>
{
    [BindProperty]
    public UnitViewModel Unit { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetUnitByIdQuery(id)), Unit);
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        return await TryThenRedirectToPage(async () => await Mediatr.Send(new DeleteUnitCommand { Id = Unit.Id }), "Index");
    }
}
