using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.Assignment.Queries;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.ProjectNamePlaceHolder.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.ProjectNamePlaceHolder.Pages.Assignment;

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
				TaskListCode = e.TaskList?.TaskListCode,
				e.PrimaryAsignee,
				e.AlternateAsignee,
				StartDate = e.StartDate.ToString("MMM dd, yyyy HH:mm"),
				EndDate = e.EndDate.ToString("MMM dd, yyyy HH:mm"),
						
				
                e.LastModifiedDate
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    } 
	
	public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetAssignmentQuery>(nameof(AssignmentState.AssignmentCode)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.AssignmentCode }));
    }
}
