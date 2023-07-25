using CTI.DPI.Application.Features.DPI.ReportTable.Queries;
using CTI.DPI.Core.DPI;
using CTI.DPI.Web.Areas.DPI.Models;
using CTI.DPI.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace CTI.DPI.Web.Areas.DPI.Pages.ReportTable;

[Authorize(Policy = Permission.ReportTable.View)]
public class IndexModel : BasePageModel<IndexModel>
{
    public ReportTableViewModel ReportTable { get; set; } = new();

    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostListAllAsync()
    {
		
        var result = await Mediatr.Send(DataRequest!.ToQuery<GetReportTableQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                ReportId = e.Report?.ReportName,
				e.TableName,
				e.Alias,
				e.JoinType,
				Sequence = e.Sequence.ToString("##,##"),
						
				
                e.LastModifiedDate
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    } 
	
	public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetReportTableQuery>(nameof(ReportTableState.Id)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.Id }));
    }
}
