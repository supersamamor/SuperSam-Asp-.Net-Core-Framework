using CTI.LocationApi.Application.Features.LocationApi.Approval.Queries;
using CTI.LocationApi.Core.LocationApi;
using CTI.LocationApi.Web.Areas.LocationApi.Models;
using CTI.LocationApi.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.LocationApi.Web.Areas.LocationApi.Pages.ApproverSetup;

[Authorize(Policy = Permission.ApproverSetup.View)]
public class IndexModel : BasePageModel<IndexModel>
{
    public ApproverSetupViewModel ApproverSetup { get; set; } = new();

    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostListAllAsync()
    {
        var result = await Mediatr.Send(DataRequest!.ToQuery<GetApproverSetupQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                e.TableName,
                e.ApprovalType,
                e.Entity,
                e.LastModifiedDate
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    }

    public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetApproverSetupQuery>(nameof(ApproverSetupState.TableName)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.TableName! }));
    }
}
