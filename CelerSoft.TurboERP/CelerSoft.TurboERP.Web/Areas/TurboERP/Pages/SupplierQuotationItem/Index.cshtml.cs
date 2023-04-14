using CelerSoft.TurboERP.Application.Features.TurboERP.SupplierQuotationItem.Queries;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Web.Areas.TurboERP.Models;
using CelerSoft.TurboERP.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace CelerSoft.TurboERP.Web.Areas.TurboERP.Pages.SupplierQuotationItem;

[Authorize(Policy = Permission.SupplierQuotationItem.View)]
public class IndexModel : BasePageModel<IndexModel>
{
    public SupplierQuotationItemViewModel SupplierQuotationItem { get; set; } = new();

    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostListAllAsync()
    {
		
        var result = await Mediatr.Send(DataRequest!.ToQuery<GetSupplierQuotationItemQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                SupplierQuotationId = e.SupplierQuotation?.Id,
				ProductId = e.Product?.BarcodeNumber,
				Quantity = e.Quantity.ToString("##,##.00"),
				Amount = e.Amount.ToString("##,##.00"),
				e.Remarks,
						
				
                e.LastModifiedDate
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    } 
	
	public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetSupplierQuotationItemQuery>(nameof(SupplierQuotationItemState.Id)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.Id }));
    }
}
