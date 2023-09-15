using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.Report.Queries;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.AreaPlaceHolder.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.AreaPlaceHolder.Pages.Dashboard
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
