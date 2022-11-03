using CTI.ELMS.Application.Features.ELMS.UserProjectAssignment.Queries;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Web.Areas.ELMS.Models;
using CTI.ELMS.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace CTI.ELMS.Web.Areas.ELMS.Pages.UserProjectAssignment;

[Authorize(Policy = Permission.UserProjectAssignment.View)]
public class IndexModel : BasePageModel<IndexModel>
{
    public UserProjectAssignmentViewModel UserProjectAssignment { get; set; } = new();

    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostListAllAsync()
    {
		
        var result = await Mediatr.Send(DataRequest!.ToQuery<GetUserProjectAssignmentQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                e.UserId,
				ProjectID = e.Project?.Id,
						
				
                e.LastModifiedDate
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    } 
	
	public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetUserProjectAssignmentQuery>(nameof(UserProjectAssignmentState.Id)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.Id }));
    }
}
