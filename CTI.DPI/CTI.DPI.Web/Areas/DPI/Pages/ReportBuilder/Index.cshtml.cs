using CTI.DPI.Application.Features.DPI.Report.Queries;
using CTI.DPI.Web.Areas.DPI.Models;
using CTI.DPI.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CTI.DPI.Web.Areas.DPI.Pages.ReportBuilder
{
    public class IndexModel : BasePageModel<IndexModel>
    {
        [BindProperty]
        public ReportViewModel Report { get; set; } = new();
        public async Task<IActionResult> OnGet(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return await PageFrom(async () => await Mediatr.Send(new GetReportByIdQuery(id)), Report);
        }
    }
}
