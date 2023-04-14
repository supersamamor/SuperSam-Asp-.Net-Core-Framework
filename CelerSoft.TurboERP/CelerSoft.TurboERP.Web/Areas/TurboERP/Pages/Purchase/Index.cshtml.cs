using CelerSoft.TurboERP.Application.Features.TurboERP.Purchase.Queries;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Web.Areas.TurboERP.Models;
using CelerSoft.TurboERP.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace CelerSoft.TurboERP.Web.Areas.TurboERP.Pages.Purchase;

[Authorize(Policy = Permission.Purchase.View)]
public class IndexModel : BasePageModel<IndexModel>
{
    public PurchaseViewModel Purchase { get; set; } = new();

    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostListAllAsync()
    {
		
        var result = await Mediatr.Send(DataRequest!.ToQuery<GetPurchaseQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                e.Code,
				e.NotedByUsername,
				PurchaseRequisitionId = e.PurchaseRequisition?.Id,
				SupplierQuotationId = e.SupplierQuotation?.Id,
				e.ReferenceInvoiceNumber,
						
				
                e.LastModifiedDate
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    } 
	
	public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetPurchaseQuery>(nameof(PurchaseState.Code)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.Code }));
    }
}
