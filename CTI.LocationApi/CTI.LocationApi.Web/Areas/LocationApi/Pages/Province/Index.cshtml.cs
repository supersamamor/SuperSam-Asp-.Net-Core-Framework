using CTI.LocationApi.Application.Features.LocationApi.Province.Queries;
using CTI.LocationApi.Core.LocationApi;
using CTI.LocationApi.Web.Areas.LocationApi.Models;
using CTI.LocationApi.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace CTI.LocationApi.Web.Areas.LocationApi.Pages.Province;

[Authorize(Policy = Permission.Province.View)]
public class IndexModel : BasePageModel<IndexModel>
{
    public ProvinceViewModel Province { get; set; } = new();

    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostListAllAsync()
    {
		
        var result = await Mediatr.Send(DataRequest!.ToQuery<GetProvinceQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                RegionId = e.Region?.Code,
				e.Code,
				e.Name,
						
				
                e.LastModifiedDate
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    } 
	
	public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetProvinceQuery>(nameof(ProvinceState.Code)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.Code }));
    }
}
