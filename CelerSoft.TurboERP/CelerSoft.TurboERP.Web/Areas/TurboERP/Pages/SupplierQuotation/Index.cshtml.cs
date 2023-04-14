using CelerSoft.TurboERP.Application.Features.TurboERP.SupplierQuotation.Queries;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Web.Areas.TurboERP.Models;
using CelerSoft.TurboERP.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CelerSoft.TurboERP.Web.Helper;


namespace CelerSoft.TurboERP.Web.Areas.TurboERP.Pages.SupplierQuotation;

[Authorize(Policy = Permission.SupplierQuotation.View)]
public class IndexModel : BasePageModel<IndexModel>
{
    public SupplierQuotationViewModel SupplierQuotation { get; set; } = new();

    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostListAllAsync()
    {
		var approvalHelper = new ApprovalHelper(Mediatr);
        var result = await Mediatr.Send(DataRequest!.ToQuery<GetSupplierQuotationQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                PurchaseRequisitionId = e.PurchaseRequisition?.Id,
				SupplierId = e.Supplier?.Company,
				e.Canvasser,
				e.Status,
						
				StatusBadge = approvalHelper.GetApprovalStatus(e.Id),
                e.LastModifiedDate
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    } 
	
	public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetSupplierQuotationQuery>(nameof(SupplierQuotationState.Id)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.Id }));
    }
}
