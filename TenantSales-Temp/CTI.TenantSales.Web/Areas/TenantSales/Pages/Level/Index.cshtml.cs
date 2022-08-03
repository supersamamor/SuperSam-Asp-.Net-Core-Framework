using CTI.TenantSales.Application.Features.TenantSales.Level.Queries;
using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.Web.Areas.TenantSales.Models;
using CTI.TenantSales.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace CTI.TenantSales.Web.Areas.TenantSales.Pages.Level;

[Authorize(Policy = Permission.Level.View)]
public class IndexModel : BasePageModel<IndexModel>
{
    public LevelViewModel Level { get; set; } = new();

    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostListAllAsync()
    {
		
        var result = await Mediatr.Send(DataRequest!.ToQuery<GetLevelQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                e.Name,
						
				
                e.LastModifiedDate
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    } 
	
	public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request, string? projectId)
    {
        var query = request.ToQuery<GetLevelQuery>(nameof(LevelState.Name));
        query.ProjectId = projectId;
        var result = await Mediatr.Send(query);
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.Name }));
    }
}
