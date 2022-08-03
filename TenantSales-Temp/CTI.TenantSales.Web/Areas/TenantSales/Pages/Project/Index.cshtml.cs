using CTI.TenantSales.Application.Features.TenantSales.Project.Queries;
using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.Web.Areas.TenantSales.Models;
using CTI.TenantSales.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace CTI.TenantSales.Web.Areas.TenantSales.Pages.Project;

[Authorize(Policy = Permission.Project.View)]
public class IndexModel : BasePageModel<IndexModel>
{
    public ProjectViewModel Project { get; set; } = new();

    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostListAllAsync()
    {		
        var result = await Mediatr.Send(DataRequest!.ToQuery<GetProjectQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                e.Name,
				e.Code,
				LandArea = e.LandArea.ToString("##,##.00"),
				GLA = e.GLA.ToString("##,##.00"),
				e.ProjectShortName,
						
				
                e.LastModifiedDate
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    } 
	
	public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var query = request.ToQuery<GetProjectQuery>(nameof(ProjectState.Name));
        query.IsActiveOnly = true;
        var result = await Mediatr.Send(query);
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.Name }));
    }
}
