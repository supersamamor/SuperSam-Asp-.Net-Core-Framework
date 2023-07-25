using CTI.DPI.Application.Features.DPI.ReportColumnDetail.Commands;
using CTI.DPI.Application.Features.DPI.ReportColumnDetail.Queries;
using CTI.DPI.Web.Areas.DPI.Models;
using CTI.DPI.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.DPI.Web.Areas.DPI.Pages.ReportColumnDetail;

[Authorize(Policy = Permission.ReportColumnDetail.Delete)]
public class DeleteModel : BasePageModel<DeleteModel>
{
    [BindProperty]
    public ReportColumnDetailViewModel ReportColumnDetail { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetReportColumnDetailByIdQuery(id)), ReportColumnDetail);
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        return await TryThenRedirectToPage(async () => await Mediatr.Send(new DeleteReportColumnDetailCommand { Id = ReportColumnDetail.Id }), "Index");
    }
}
