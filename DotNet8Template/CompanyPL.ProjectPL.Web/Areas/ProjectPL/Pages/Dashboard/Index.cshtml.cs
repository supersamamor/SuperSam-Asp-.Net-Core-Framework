using CompanyPL.ProjectPL.Application.Features.ProjectPL.Report.Queries;
using CompanyPL.ProjectPL.Web.Areas.ProjectPL.Models;
using CompanyPL.ProjectPL.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace CompanyPL.ProjectPL.Web.Areas.ProjectPL.Pages.Dashboard
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
