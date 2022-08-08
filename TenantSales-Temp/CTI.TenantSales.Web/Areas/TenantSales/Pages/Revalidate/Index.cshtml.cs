using CTI.TenantSales.Application.Features.TenantSales.Revalidate.Queries;
using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.Web.Areas.TenantSales.Models;
using CTI.TenantSales.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace CTI.TenantSales.Web.Areas.TenantSales.Pages.Revalidate;

[Authorize(Policy = Permission.Revalidate.View)]
public class IndexModel : BasePageModel<IndexModel>
{
    public RevalidateViewModel Revalidate { get; set; } = new();

    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostListAllAsync()
    {
		
        var result = await Mediatr.Send(DataRequest!.ToQuery<GetRevalidateQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                SalesDate = e.SalesDate.ToString("MMM dd, yyyy"),
				ProjectId = e.Project?.Id,
				TenantId = e.Tenant?.Id,
				e.Status,
						
				
                e.LastModifiedDate
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    } 
	
	public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetRevalidateQuery>(nameof(RevalidateState.Id)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.Id }));
    }
}
