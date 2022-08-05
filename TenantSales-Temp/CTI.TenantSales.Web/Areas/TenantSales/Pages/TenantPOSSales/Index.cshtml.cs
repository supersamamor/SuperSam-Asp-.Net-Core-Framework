using CTI.TenantSales.Application.Features.TenantSales.Tenant.Queries;
using CTI.TenantSales.Application.Features.TenantSales.TenantPOSSales.Queries;
using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.Web.Areas.TenantSales.Models;
using CTI.TenantSales.Web.Helper;
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
    public string Tenant { get; set; } = "";
    public string TenantId { get; set; } = "";
    public async Task<IActionResult> OnGet(string tenantId)
    {
        TenantState tenant = new();
        _ = (await Mediatr.Send(new GetTenantByIdQuery(tenantId))).Select(l => tenant = l);
        Tenant = tenant.Name + " - " + tenant.Code;
        TenantId = tenant.Id;
        return Page();
    }

    public async Task<IActionResult> OnPostListAllAsync(string tenantId, DateTime dateFrom, DateTime dateTo)
    {
        var getTableQuery = DataRequest!.ToQuery<GetTenantPOSSalesQuery>();
        getTableQuery.TenantId = tenantId;
        getTableQuery.DateFrom = dateFrom;
        getTableQuery.DateTo = dateTo;
        var result = await Mediatr.Send(getTableQuery);
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                e.SalesCategory,
                SalesDate = e.SalesDate.ToString("MMM dd, yyyy"),
                IsAutoCompute = e.IsAutoCompute == true ? "Yes" : "No",
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
                ControlNumber = e.ControlNumber.ToString("##,##"),
                ValidationStatus = e.ValidationStatus.ToString("##,##"),
                e.ValidationRemarks,
                AutocalculatedNewAccumulatedTotal = e.AutocalculatedNewAccumulatedTotal.ToString("##,##.00"),
                AutocalculatedOldAccumulatedTotal = e.AutocalculatedOldAccumulatedTotal.ToString("##,##.00"),
                TenantCode = e.TenantPOS?.Tenant?.Code,
                TenantName = e.TenantPOS?.Tenant?.Name,
                PosCode = e.TenantPOS?.Code,
                e.LastModifiedDate,
                ValidationStatusBadge = SalesStatusHelper.GetSalesStatusBadge((Core.Constants.ValidationStatusEnum)e.ValidationStatus, e?.ValidationRemarks),
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    }

    public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetTenantPOSSalesQuery>(nameof(TenantPOSSalesState.Id)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.Id }));
    }
}
