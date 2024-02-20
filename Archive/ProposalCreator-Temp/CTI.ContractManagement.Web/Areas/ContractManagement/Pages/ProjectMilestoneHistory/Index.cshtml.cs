using CTI.ContractManagement.Application.Features.ContractManagement.ProjectMilestoneHistory.Queries;
using CTI.ContractManagement.Core.ContractManagement;
using CTI.ContractManagement.Web.Areas.ContractManagement.Models;
using CTI.ContractManagement.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace CTI.ContractManagement.Web.Areas.ContractManagement.Pages.ProjectMilestoneHistory;

[Authorize(Policy = Permission.ProjectMilestoneHistory.View)]
public class IndexModel : BasePageModel<IndexModel>
{
    public ProjectMilestoneHistoryViewModel ProjectMilestoneHistory { get; set; } = new();

    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostListAllAsync()
    {
		
        var result = await Mediatr.Send(DataRequest!.ToQuery<GetProjectMilestoneHistoryQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                ProjectHistoryId = e.ProjectHistory?.Id,
				MilestoneStageId = e.MilestoneStage?.Name,
				FrequencyId = e.Frequency?.Name,
				FrequencyQuantity = e.FrequencyQuantity?.ToString("##,##"),
						
				
                e.LastModifiedDate
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    } 
	
	public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetProjectMilestoneHistoryQuery>(nameof(ProjectMilestoneHistoryState.Id)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.Id }));
    }
}
