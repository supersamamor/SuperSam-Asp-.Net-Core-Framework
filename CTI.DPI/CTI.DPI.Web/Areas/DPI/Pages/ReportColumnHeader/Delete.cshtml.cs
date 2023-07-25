using CTI.DPI.Application.Features.DPI.ReportColumnHeader.Commands;
using CTI.DPI.Application.Features.DPI.ReportColumnHeader.Queries;
using CTI.DPI.Web.Areas.DPI.Models;
using CTI.DPI.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.DPI.Web.Areas.DPI.Pages.ReportColumnHeader;

[Authorize(Policy = Permission.ReportColumnHeader.Delete)]
public class DeleteModel : BasePageModel<DeleteModel>
{
    [BindProperty]
    public ReportColumnHeaderViewModel ReportColumnHeader { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetReportColumnHeaderByIdQuery(id)), ReportColumnHeader);
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        return await TryThenRedirectToPage(async () => await Mediatr.Send(new DeleteReportColumnHeaderCommand { Id = ReportColumnHeader.Id }), "Index");
    }
}
