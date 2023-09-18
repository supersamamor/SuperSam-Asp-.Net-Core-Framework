using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.TaskList.Queries;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.ProjectNamePlaceHolder.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Helper;


namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.ProjectNamePlaceHolder.Pages.TaskList;

[Authorize(Policy = Permission.TaskList.View)]
public class IndexModel : BasePageModel<IndexModel>
{
    public TaskListViewModel TaskList { get; set; } = new();

    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostListAllAsync()
    {
		var approvalHelper = new ApprovalHelper(Mediatr);
        var result = await Mediatr.Send(DataRequest!.ToQuery<GetTaskListQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                e.TaskListCode,
				e.TaskDescription,
				e.TaskType,
				e.TaskFrequency,
				TaskDueDay = e.TaskDueDay.ToString("##,##"),
				TargetDueDate = e.TargetDueDate.ToString("MMM dd, yyyy HH:mm"),
				e.PrimaryEndorser,
				e.PrimaryApprover,
				e.AlternateEndorser,
				e.AlternateApprover,
						
				StatusBadge = approvalHelper.GetApprovalStatus(e.Id),
                e.LastModifiedDate
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    } 
	
	public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetTaskListQuery>(nameof(TaskListState.TaskListCode)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.TaskListCode }));
    }
}
