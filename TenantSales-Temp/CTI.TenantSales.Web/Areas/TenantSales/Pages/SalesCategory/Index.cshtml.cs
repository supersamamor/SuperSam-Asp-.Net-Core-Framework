using CTI.TenantSales.Application.Features.TenantSales.SalesCategory.Queries;
using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.Web.Areas.TenantSales.Models;
using CTI.TenantSales.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace CTI.TenantSales.Web.Areas.TenantSales.Pages.SalesCategory;

[Authorize(Policy = Permission.SalesCategory.View)]
public class IndexModel : BasePageModel<IndexModel>
{
    public SalesCategoryViewModel SalesCategory { get; set; } = new();

    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostListAllAsync()
    {
		
        var result = await Mediatr.Send(DataRequest!.ToQuery<GetSalesCategoryQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                e.Code,
				e.Name,
						
				
                e.LastModifiedDate
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    } 
	
	public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request, string? tenantId)
    {
        var query = request.ToQuery<GetSalesCategoryQuery>(nameof(SalesCategoryState.Name));
        query.TenantId = tenantId;
        var result = await Mediatr.Send(query);
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Code, Text = e.Code }));
    }
}
