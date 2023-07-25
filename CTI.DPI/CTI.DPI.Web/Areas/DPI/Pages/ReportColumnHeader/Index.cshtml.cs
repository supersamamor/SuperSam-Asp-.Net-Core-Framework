using CTI.DPI.Application.Features.DPI.ReportColumnHeader.Queries;
using CTI.DPI.Core.DPI;
using CTI.DPI.Web.Areas.DPI.Models;
using CTI.DPI.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace CTI.DPI.Web.Areas.DPI.Pages.ReportColumnHeader;

[Authorize(Policy = Permission.ReportColumnHeader.View)]
public class IndexModel : BasePageModel<IndexModel>
{
    public ReportColumnHeaderViewModel ReportColumnHeader { get; set; } = new();

    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostListAllAsync()
    {
		
        var result = await Mediatr.Send(DataRequest!.ToQuery<GetReportColumnHeaderQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                ReportId = e.Report?.ReportName,
				e.Alias,
				e.AggregationOperator,
						
				
                e.LastModifiedDate
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    } 
	
	public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetReportColumnHeaderQuery>(nameof(ReportColumnHeaderState.Id)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.Id }));
    }
}
