using CTI.DPI.Application.Features.DPI.ReportTableJoinParameter.Queries;
using CTI.DPI.Web.Areas.DPI.Models;
using CTI.DPI.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.DPI.Web.Areas.DPI.Pages.ReportTableJoinParameter;

[Authorize(Policy = Permission.ReportTableJoinParameter.View)]
public class DetailsModel : BasePageModel<DetailsModel>
{
    public ReportTableJoinParameterViewModel ReportTableJoinParameter { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetReportTableJoinParameterByIdQuery(id)), ReportTableJoinParameter);
    }
}
