using CTI.DPI.Application.Features.DPI.ReportColumnFilter.Queries;
using CTI.DPI.Web.Areas.DPI.Models;
using CTI.DPI.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.DPI.Web.Areas.DPI.Pages.ReportColumnFilter;

[Authorize(Policy = Permission.ReportColumnFilter.View)]
public class DetailsModel : BasePageModel<DetailsModel>
{
    public ReportColumnFilterViewModel ReportColumnFilter { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetReportColumnFilterByIdQuery(id)), ReportColumnFilter);
    }
}
