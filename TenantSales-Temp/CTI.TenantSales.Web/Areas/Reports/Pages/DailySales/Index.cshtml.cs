using CTI.TenantSales.ExcelProcessor.Services;
using CTI.TenantSales.Web.Areas.Reports.Models;
using CTI.TenantSales.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace CTI.TenantSales.Web.Areas.Reports.Pages.DailySales
{
    [Authorize(Policy = Permission.Reports.DailySales)]
    public class IndexModel : BasePageModel<IndexModel>
    {
		private readonly ExportDailySalesReportService _exportDailySalesReportService;
		private readonly int _cutOffFrom;
		private readonly int _cutOffTo;
		public IndexModel(ExportDailySalesReportService exportDailySalesReportService, IConfiguration configuration)
		{
			_exportDailySalesReportService = exportDailySalesReportService;
			_cutOffFrom = configuration.GetValue<int>("CutOff:From");
			_cutOffTo = configuration.GetValue<int>("CutOff:To");
		}
		[BindProperty]
        public DailySalesModel Input { get; set; } = new();
		public IActionResult OnGet()
        {
            return Page();
        }
		public IActionResult OnPost()
		{
			ModelState.Clear();
			var reportDate = new DateTime(Input.Year, Input.Month + 1, 1);
			DateTime dateFrom = new DateTime(reportDate.AddMonths(-1).Year, reportDate.AddMonths(-1).Month, _cutOffFrom);
			DateTime dateTo = new DateTime(reportDate.Year, reportDate.Month, _cutOffTo);
			_exportDailySalesReportService.Export(dateFrom, dateTo, null);
			return Partial("_InputFieldsPartial", Input);
		}
		public IActionResult OnPostChangeFormValue()
		{
			ModelState.Clear();
			return Partial("_InputFieldsPartial", Input);
		}
	}
}
