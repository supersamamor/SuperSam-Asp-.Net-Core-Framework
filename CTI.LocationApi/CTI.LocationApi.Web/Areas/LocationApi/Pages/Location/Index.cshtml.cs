using CTI.LocationApi.Application.Features.LocationApi.Location.Queries;
using CTI.LocationApi.Core.LocationApi;
using CTI.LocationApi.Web.Areas.LocationApi.Models;
using CTI.LocationApi.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace CTI.LocationApi.Web.Areas.LocationApi.Pages.Location;

[Authorize(Policy = Permission.Location.View)]
public class IndexModel : BasePageModel<IndexModel>
{
    public LocationViewModel Location { get; set; } = new();

    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostListAllAsync()
    {
		
        var result = await Mediatr.Send(DataRequest!.ToQuery<GetLocationQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                e.BarangayCode,
				e.Barangay,
				e.CityCode,
				e.City,
				e.ProvinceCode,
				e.Province,
				e.RegionCode,
				e.Region,
				e.Full,
						
				
                e.LastModifiedDate
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    } 
	
	public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetLocationQuery>(nameof(LocationState.Id)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.Id }));
    }
}
