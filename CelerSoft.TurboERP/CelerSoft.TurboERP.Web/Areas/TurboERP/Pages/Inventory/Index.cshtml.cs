using CelerSoft.TurboERP.Application.Features.TurboERP.Inventory.Queries;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Web.Areas.TurboERP.Models;
using CelerSoft.TurboERP.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CelerSoft.TurboERP.Web.Helper;


namespace CelerSoft.TurboERP.Web.Areas.TurboERP.Pages.Inventory;

[Authorize(Policy = Permission.Inventory.View)]
public class IndexModel : BasePageModel<IndexModel>
{
    public InventoryViewModel Inventory { get; set; } = new();

    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostListAllAsync()
    {
		var approvalHelper = new ApprovalHelper(Mediatr);
        var result = await Mediatr.Send(DataRequest!.ToQuery<GetInventoryQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                PurchaseItemId = e.PurchaseItem?.Id,
				ProductId = e.Product?.BarcodeNumber,
				Quantity = e.Quantity.ToString("##,##.00"),
				Amount = e.Amount.ToString("##,##.00"),
						
				StatusBadge = approvalHelper.GetApprovalStatus(e.Id),
                e.LastModifiedDate
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    } 
	
	public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetInventoryQuery>(nameof(InventoryState.Id)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.Id }));
    }
}
