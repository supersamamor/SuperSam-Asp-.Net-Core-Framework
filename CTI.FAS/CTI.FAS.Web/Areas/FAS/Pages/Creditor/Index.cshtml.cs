using CTI.FAS.Application.Features.FAS.Creditor.Queries;
using CTI.FAS.Core.FAS;
using CTI.FAS.Web.Areas.FAS.Models;
using CTI.FAS.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace CTI.FAS.Web.Areas.FAS.Pages.Creditor;

[Authorize(Policy = Permission.Creditor.View)]
public class IndexModel : BasePageModel<IndexModel>
{
    public CreditorViewModel Creditor { get; set; } = new();

    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostListAllAsync()
    {
		
        var result = await Mediatr.Send(DataRequest!.ToQuery<GetCreditorQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                CreditorAccount = e.CreditorDisplayDescription,
				DatabaseConnectionSetupId = e.DatabaseConnectionSetup?.Name,	
                e.LastModifiedDate,
                e.PayeeAccountName,
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    } 
	
	public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetCreditorQuery>(nameof(CreditorState.Id)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.Id }));
    }
}
