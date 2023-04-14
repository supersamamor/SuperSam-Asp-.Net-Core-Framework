using CelerSoft.TurboERP.Application.Features.TurboERP.PurchaseRequisitionItem.Queries;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Web.Areas.TurboERP.Models;
using CelerSoft.TurboERP.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace CelerSoft.TurboERP.Web.Areas.TurboERP.Pages.PurchaseRequisitionItem;

[Authorize(Policy = Permission.PurchaseRequisitionItem.View)]
public class IndexModel : BasePageModel<IndexModel>
{
    public PurchaseRequisitionItemViewModel PurchaseRequisitionItem { get; set; } = new();

    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostListAllAsync()
    {
		
        var result = await Mediatr.Send(DataRequest!.ToQuery<GetPurchaseRequisitionItemQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                PurchaseRequisitionId = e.PurchaseRequisition?.Id,
				ProductId = e.Product?.BarcodeNumber,
				Quantity = e.Quantity.ToString("##,##.00"),
				e.Remarks,
						
				
                e.LastModifiedDate
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    } 
	
	public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetPurchaseRequisitionItemQuery>(nameof(PurchaseRequisitionItemState.Id)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.Id }));
    }
}
