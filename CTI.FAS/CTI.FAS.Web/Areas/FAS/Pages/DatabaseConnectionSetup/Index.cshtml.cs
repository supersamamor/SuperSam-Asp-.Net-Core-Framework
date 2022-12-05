using CTI.FAS.Application.Features.FAS.DatabaseConnectionSetup.Queries;
using CTI.FAS.Core.FAS;
using CTI.FAS.Web.Areas.FAS.Models;
using CTI.FAS.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CTI.FAS.Web.Helper;


namespace CTI.FAS.Web.Areas.FAS.Pages.DatabaseConnectionSetup;

[Authorize(Policy = Permission.DatabaseConnectionSetup.View)]
public class IndexModel : BasePageModel<IndexModel>
{
    public DatabaseConnectionSetupViewModel DatabaseConnectionSetup { get; set; } = new();

    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostListAllAsync()
    {
		var approvalHelper = new ApprovalHelper(Mediatr);
        var result = await Mediatr.Send(DataRequest!.ToQuery<GetDatabaseConnectionSetupQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                e.Code,
				e.Name,
				e.DatabaseAndServerName,
						
				StatusBadge = approvalHelper.GetApprovalStatus(e.Id),
                e.LastModifiedDate
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    }
    public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetDatabaseConnectionSetupQuery>(nameof(DatabaseConnectionSetupState.Name)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.Name }));
    }
}
