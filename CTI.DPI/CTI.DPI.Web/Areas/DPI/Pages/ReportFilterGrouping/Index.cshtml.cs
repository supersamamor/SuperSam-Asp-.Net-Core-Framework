using CTI.DPI.Application.Features.DPI.ReportFilterGrouping.Queries;
using CTI.DPI.Core.DPI;
using CTI.DPI.Web.Areas.DPI.Models;
using CTI.DPI.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace CTI.DPI.Web.Areas.DPI.Pages.ReportFilterGrouping;

[Authorize(Policy = Permission.ReportFilterGrouping.View)]
public class IndexModel : BasePageModel<IndexModel>
{
    public ReportFilterGroupingViewModel ReportFilterGrouping { get; set; } = new();

    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostListAllAsync()
    {
		
        var result = await Mediatr.Send(DataRequest!.ToQuery<GetReportFilterGroupingQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                ReportId = e.Report?.ReportName,
				e.LogicalOperator,
				GroupLevel = e.GroupLevel?.ToString("##,##"),
				Sequence = e.Sequence?.ToString("##,##"),
						
				
                e.LastModifiedDate
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    } 
	
	public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetReportFilterGroupingQuery>(nameof(ReportFilterGroupingState.Id)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.Id }));
    }
}
