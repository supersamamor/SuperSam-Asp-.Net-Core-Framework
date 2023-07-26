using CTI.DPI.Application.DTOs;
using CTI.DPI.Application.Features.DPI.Report.Queries;
using CTI.DPI.Web.Areas.DPI.Models;
using CTI.DPI.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CTI.DPI.Web.Areas.DPI.Pages.Dashboard
{
    public class IndexModel : BasePageModel<IndexModel>
    {
        [BindProperty]
        public ReportResultViewModel Report { get; set; } = new();
        public async Task<IActionResult> OnGet(string? id)
        {
            id = "1d73b1cf-a55a-43b4-8043-bcf7e7678f2a";
            if (id == null)
            {
                return NotFound();
            }
            _ = (await Mediatr.Send(new GetReportBuilderByIdQuery(id)
                    { Filters = Mapper.Map<IList<ReportQueryFilterModel>>(Report.Filters) })).Select(l => Mapper.Map(l, Report));          
            return Page();
        }
    }
}
