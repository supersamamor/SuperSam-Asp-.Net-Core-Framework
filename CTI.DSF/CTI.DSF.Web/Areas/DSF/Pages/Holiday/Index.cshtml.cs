using CTI.DSF.Application.Features.DSF.Holiday.Queries;
using CTI.DSF.Core.DSF;
using CTI.DSF.Web.Areas.DSF.Models;
using CTI.DSF.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace CTI.DSF.Web.Areas.DSF.Pages.Holiday;

[Authorize(Policy = Permission.Holiday.View)]
public class IndexModel : BasePageModel<IndexModel>
{
    public HolidayViewModel Holiday { get; set; } = new();

    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostListAllAsync()
    {
		
        var result = await Mediatr.Send(DataRequest!.ToQuery<GetHolidayQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                e.HolidayName,
				HolidayDate = e.HolidayDate.ToString("MMM dd, yyyy"),
						
				
                e.LastModifiedDate
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    } 
	
	public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetHolidayQuery>(nameof(HolidayState.Id)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.Id }));
    }
}
