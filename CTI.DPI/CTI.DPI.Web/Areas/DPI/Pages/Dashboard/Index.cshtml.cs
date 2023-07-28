using CTI.DPI.Application.Features.DPI.Report.Queries;
using CTI.DPI.Web.Areas.DPI.Models;
using CTI.DPI.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace CTI.DPI.Web.Areas.DPI.Pages.Dashboard
{
    public class IndexModel : BasePageModel<IndexModel>
    {
        [BindProperty]
        public IList<ReportResultViewModel> ReportList { get; set; } = new List<ReportResultViewModel>();
        public async Task<IActionResult> OnGet()
        {
            Mapper.Map(await Mediatr.Send(new GetDashboardReportsQuery()), ReportList);
            return Page();
        }
    }
}
