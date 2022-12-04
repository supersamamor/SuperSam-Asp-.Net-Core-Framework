using CTI.FAS.Application.Features.FAS.EnrolledPayeeEmail.Queries;
using CTI.FAS.Core.FAS;
using CTI.FAS.Web.Areas.FAS.Models;
using CTI.FAS.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace CTI.FAS.Web.Areas.FAS.Pages.EnrolledPayeeEmail;

[Authorize(Policy = Permission.EnrolledPayeeEmail.View)]
public class IndexModel : BasePageModel<IndexModel>
{
    public EnrolledPayeeEmailViewModel EnrolledPayeeEmail { get; set; } = new();

    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostListAllAsync()
    {
		
        var result = await Mediatr.Send(DataRequest!.ToQuery<GetEnrolledPayeeEmailQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                e.Email,
						
				
                e.LastModifiedDate
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    } 
	
	public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetEnrolledPayeeEmailQuery>(nameof(EnrolledPayeeEmailState.Id)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.Id }));
    }
}
