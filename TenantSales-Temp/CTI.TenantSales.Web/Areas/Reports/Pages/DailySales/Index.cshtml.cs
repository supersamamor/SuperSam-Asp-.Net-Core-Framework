using CTI.TenantSales.Application.Features.TenantSales.TenantPOSSales.Queries;
using CTI.TenantSales.ExcelProcessor.Helpers;
using CTI.TenantSales.Web.Areas.Reports.Models;
using CTI.TenantSales.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace CTI.TenantSales.Web.Areas.Reports.Pages.DailySales
{
    [Authorize(Policy = Permission.Reports.DailySales)]
    public class IndexModel : BasePageModel<IndexModel>
    {
        private readonly int _cutOffFrom;
        private readonly int _cutOffTo;
        public IndexModel(IConfiguration configuration)
        {      
            _cutOffFrom = configuration.GetValue<int>("CutOff:From");
            _cutOffTo = configuration.GetValue<int>("CutOff:To");
        }
        [BindProperty]
        public DailySalesModel Input { get; set; } = new();
        public IActionResult OnGet()
        {
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            ModelState.Clear();
            var reportDate = new DateTime(Input.Year, Input.Month + 1, 1);
            DateTime dateFrom = new(reportDate.AddMonths(-1).Year, reportDate.AddMonths(-1).Month, _cutOffFrom);
            DateTime dateTo = new(reportDate.Year, reportDate.Month, _cutOffTo);
            var tenantPOSList = await Mediatr.Send(new GetDailySalesReportQuery(dateFrom, dateTo, Input.TenantId, Input.LevelId, Input.ProjectId));
            Input.FilePath = ExportDailySalesReportHelper.Export(dateFrom, dateTo, tenantPOSList);
            return Partial("_InputFieldsPartial", Input);
        }
        public IActionResult OnPostChangeFormValue()
        {
            ModelState.Clear();
            return Partial("_InputFieldsPartial", Input);
        }
    }
}
