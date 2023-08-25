using CTI.DSF.Application.Features.DSF.Assignment.Queries;
using CTI.DSF.Core.DSF;
using CTI.DSF.Web.Areas.DSF.Models;
using CTI.DSF.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace CTI.DSF.Web.Areas.DSF.Pages.Assignment;

[Authorize(Policy = Permission.Assignment.View)]
public class IndexModel : BasePageModel<IndexModel>
{
    public AssignmentViewModel Assignment { get; set; } = new();

    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostListAllAsync()
    {
		
        var result = await Mediatr.Send(DataRequest!.ToQuery<GetAssignmentQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                e.AssignmentCode,
				TaskListId = e.TaskList?.Id,
				e.PrimaryAssignee,
				e.AlternateAssignee,
				StartDate = e.StartDate?.ToString("MMM dd, yyyy HH:mm"),
				EndDate = e.EndDate?.ToString("MMM dd, yyyy HH:mm"),
						
				
                e.LastModifiedDate
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    } 
	
	public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetAssignmentQuery>(nameof(AssignmentState.Id)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.Id }));
    }
}
