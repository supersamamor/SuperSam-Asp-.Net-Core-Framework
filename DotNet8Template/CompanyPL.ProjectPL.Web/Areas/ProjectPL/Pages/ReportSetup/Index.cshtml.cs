using CompanyPL.ProjectPL.Application.Features.ProjectPL.Report.Queries;
using CompanyPL.ProjectPL.Core.ProjectPL;
using CompanyPL.ProjectPL.Web.Areas.ProjectPL.Models;
using CompanyPL.ProjectPL.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CompanyPL.ProjectPL.Web.Helper;


namespace CompanyPL.ProjectPL.Web.Areas.ProjectPL.Pages.ReportSetup;

[Authorize(Policy = Permission.ReportSetup.View)]
public class IndexModel : BasePageModel<IndexModel>
{
    public ReportViewModel Report { get; set; } = new();

    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostListAllAsync()
    {		
        var result = await Mediatr.Send(DataRequest!.ToQuery<GetReportQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                e.ReportName,
				e.QueryType,
                DisplayOnDashboard = DisplayOnDashboardHelper.GetBadge(e.DisplayOnDashboard),
                e.ReportOrChartType,
                e.Sequence,
                e.LastModifiedDate
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    } 
	
	public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetReportQuery>(nameof(ReportState.ReportName)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.ReportName }));
    }
}
