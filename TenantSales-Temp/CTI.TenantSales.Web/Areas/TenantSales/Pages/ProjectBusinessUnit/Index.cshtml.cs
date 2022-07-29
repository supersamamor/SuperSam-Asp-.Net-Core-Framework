using CTI.TenantSales.Application.Features.TenantSales.ProjectBusinessUnit.Queries;
using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.Web.Areas.TenantSales.Models;
using CTI.TenantSales.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace CTI.TenantSales.Web.Areas.TenantSales.Pages.ProjectBusinessUnit;

[Authorize(Policy = Permission.ProjectBusinessUnit.View)]
public class IndexModel : BasePageModel<IndexModel>
{
    public ProjectBusinessUnitViewModel ProjectBusinessUnit { get; set; } = new();

    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostListAllAsync()
    {
		
        var result = await Mediatr.Send(DataRequest!.ToQuery<GetProjectBusinessUnitQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                BusinessUnitId = e.BusinessUnit?.Name,
				ProjectId = e.Project?.Id,
						
				
                e.LastModifiedDate
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    } 
	
	public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetProjectBusinessUnitQuery>(nameof(ProjectBusinessUnitState.Id)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.Id }));
    }
}
