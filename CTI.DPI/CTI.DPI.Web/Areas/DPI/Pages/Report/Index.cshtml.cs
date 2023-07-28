using CTI.DPI.Application.DTOs;
using CTI.DPI.Application.Features.DPI.Report.Commands;
using CTI.DPI.Application.Features.DPI.Report.Queries;
using CTI.DPI.Web.Areas.DPI.Models;
using CTI.DPI.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.DPI.Web.Areas.DPI.Pages.Report
{
    [Authorize(Policy = Permission.Report.View)]
    public class IndexModel : BasePageModel<IndexModel>
    {
        [BindProperty]
        public ReportResultViewModel Report { get; set; } = new ReportResultViewModel();
        [BindProperty]
        public IList<ReportQueryFilterViewModel> Filters { get; set; } = new List<ReportQueryFilterViewModel>();
        [BindProperty]
        public string ReportId { get; set; } = "";
        public async Task<IActionResult> OnGet(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ReportId = id;
            var reportResult = new ReportResultModel();
            (await Mediatr.Send(new GetReportSetupAndResultByIdQuery(id))).Select(l => reportResult = l);
            Mapper.Map(reportResult, Report);
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            var reportResult = new ReportResultModel();
            var query = new GetReportSetupAndResultByIdQuery(ReportId);
            Mapper.Map(Filters, query.Filters);
            (await Mediatr.Send(query)).Select(l => reportResult = l);
            Mapper.Map(reportResult, Report);
            return Page();
        }
    }
}
