using CTI.DPI.Application.Features.DPI.ReportQueryFilter.Queries;
using CTI.DPI.Core.DPI;
using CTI.DPI.Web.Areas.DPI.Models;
using CTI.DPI.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace CTI.DPI.Web.Areas.DPI.Pages.ReportQueryFilter;

[Authorize(Policy = Permission.ReportQueryFilter.View)]
public class IndexModel : BasePageModel<IndexModel>
{
    public ReportQueryFilterViewModel ReportQueryFilter { get; set; } = new();

    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostListAllAsync()
    {
		
        var result = await Mediatr.Send(DataRequest!.ToQuery<GetReportQueryFilterQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                ReportId = e.Report?.ReportName,
				e.FieldName,
				e.ComparisonOperator,
						
				
                e.LastModifiedDate
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    } 
	
	public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetReportQueryFilterQuery>(nameof(ReportQueryFilterState.Id)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.Id }));
    }
}
