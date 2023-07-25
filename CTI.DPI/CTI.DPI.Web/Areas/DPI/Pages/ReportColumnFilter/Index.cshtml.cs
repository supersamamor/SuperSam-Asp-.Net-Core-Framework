using CTI.DPI.Application.Features.DPI.ReportColumnFilter.Queries;
using CTI.DPI.Core.DPI;
using CTI.DPI.Web.Areas.DPI.Models;
using CTI.DPI.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace CTI.DPI.Web.Areas.DPI.Pages.ReportColumnFilter;

[Authorize(Policy = Permission.ReportColumnFilter.View)]
public class IndexModel : BasePageModel<IndexModel>
{
    public ReportColumnFilterViewModel ReportColumnFilter { get; set; } = new();

    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostListAllAsync()
    {
		
        var result = await Mediatr.Send(DataRequest!.ToQuery<GetReportColumnFilterQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                ReportFilterGroupingId = e.ReportFilterGrouping?.Id,
				e.LogicalOperator,
				TableId = e.ReportTable?.Id,
				e.FieldName,
				e.ComparisonOperator,
				IsString =  e.IsString == true ? "Yes" : "No",
						
				
                e.LastModifiedDate
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    } 
	
	public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetReportColumnFilterQuery>(nameof(ReportColumnFilterState.Id)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.Id }));
    }
}
