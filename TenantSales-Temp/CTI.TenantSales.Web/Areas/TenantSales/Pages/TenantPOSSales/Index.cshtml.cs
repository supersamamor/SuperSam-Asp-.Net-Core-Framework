using CTI.TenantSales.Application.Features.TenantSales.TenantPOSSales.Queries;
using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.Web.Areas.TenantSales.Models;
using CTI.TenantSales.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace CTI.TenantSales.Web.Areas.TenantSales.Pages.TenantPOSSales;

[Authorize(Policy = Permission.TenantPOSSales.View)]
public class IndexModel : BasePageModel<IndexModel>
{
    public TenantPOSSalesViewModel TenantPOSSales { get; set; } = new();

    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostListAllAsync()
    {
		
        var result = await Mediatr.Send(DataRequest!.ToQuery<GetTenantPOSSalesQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                e.SalesCategory,
				SalesDate = e.SalesDate.ToString("MMM dd, yyyy"),
				IsAutoCompute =  e.IsAutoCompute == true ? "Yes" : "No",
				SalesAmount = e.SalesAmount.ToString("##,##.00"),
				OldAccumulatedTotal = e.OldAccumulatedTotal.ToString("##,##.00"),
				NewAccumulatedTotal = e.NewAccumulatedTotal.ToString("##,##.00"),
				TaxableSalesAmount = e.TaxableSalesAmount.ToString("##,##.00"),
				NonTaxableSalesAmount = e.NonTaxableSalesAmount.ToString("##,##.00"),
				SeniorCitizenDiscount = e.SeniorCitizenDiscount.ToString("##,##.00"),
				PromoDiscount = e.PromoDiscount.ToString("##,##.00"),
				OtherDiscount = e.OtherDiscount.ToString("##,##.00"),
				RefundDiscount = e.RefundDiscount.ToString("##,##.00"),
				VoidAmount = e.VoidAmount.ToString("##,##.00"),
				AdjustmentAmount = e.AdjustmentAmount.ToString("##,##.00"),
				TotalServiceCharge = e.TotalServiceCharge.ToString("##,##.00"),
				TotalTax = e.TotalTax.ToString("##,##.00"),
				NoOfSalesTransactions = e.NoOfSalesTransactions.ToString("##,##.00"),
				NoOfTransactions = e.NoOfTransactions.ToString("##,##.00"),
				TotalNetSales = e.TotalNetSales.ToString("##,##.00"),
				ControlNumber = e.ControlNumber.ToString("##,##.00"),
				ValidationStatus = e.ValidationStatus.ToString("##,##.00"),
				e.ValidationRemarks,
				AutocalculatedNewAccumulatedTotal = e.AutocalculatedNewAccumulatedTotal.ToString("##,##.00"),
				AutocalculatedOldAccumulatedTotal = e.AutocalculatedOldAccumulatedTotal.ToString("##,##.00"),
						
				
                e.LastModifiedDate
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    } 
	
	public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetTenantPOSSalesQuery>(nameof(TenantPOSSalesState.Id)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.Id }));
    }
}
