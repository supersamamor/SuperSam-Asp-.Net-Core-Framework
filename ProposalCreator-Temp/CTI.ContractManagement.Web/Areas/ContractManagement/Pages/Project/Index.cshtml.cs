using CTI.ContractManagement.Application.Features.ContractManagement.Project.Queries;
using CTI.ContractManagement.Core.ContractManagement;
using CTI.ContractManagement.Web.Areas.ContractManagement.Models;
using CTI.ContractManagement.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CTI.ContractManagement.Web.Helper;


namespace CTI.ContractManagement.Web.Areas.ContractManagement.Pages.Project;

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
		var approvalHelper = new ApprovalHelper(Mediatr);
        var result = await Mediatr.Send(DataRequest!.ToQuery<GetProjectQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                ClientId = e.Client?.ContactPersonName,
				e.ProjectName,
				e.ProjectDescription,
				e.DocumentCode,
						
				StatusBadge = approvalHelper.GetApprovalStatus(e.Id),
                e.LastModifiedDate
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    } 
	
	public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetProjectQuery>(nameof(ProjectState.DocumentCode)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.DocumentCode }));
    }
}
