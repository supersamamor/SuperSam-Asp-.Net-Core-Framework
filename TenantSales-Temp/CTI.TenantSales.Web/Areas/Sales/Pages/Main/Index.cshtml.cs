using CTI.TenantSales.Application.Features.TenantSales.TenantPOSSales.Queries;
using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.ExcelProcessor.Helper;
using CTI.TenantSales.ExcelProcessor.Models;
using CTI.TenantSales.Web.Areas.Sales.Models;
using CTI.TenantSales.Web.Areas.TenantSales.Models;
using CTI.TenantSales.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace CTI.TenantSales.Web.Areas.Sales.Pages.Main
{
    [Authorize(Policy = Permission.TenantPOSSales.View)]
    public class IndexModel : BasePageModel<IndexModel>
    {
        private readonly int _cutOffFrom;
        private readonly int _cutOffTo;
        private readonly string _uploadPath;
        public IndexModel(IConfiguration configuration)
        {
            _cutOffFrom = configuration.GetValue<int>("CutOff:From");
            _cutOffTo = configuration.GetValue<int>("CutOff:To");
            _uploadPath = configuration.GetValue<string>("UsersUpload:UploadFilesPath");
        }
        [BindProperty]
        public TenantSalesModel Input { get; set; } = new();
        public IList<TenantViewModel> TenantSalesList { get; set; } = new List<TenantViewModel>();
        public IActionResult OnGet()
        {
            //Set Current Month as Default Date Filter
            Input.DateFrom = new DateTime(DateTime.Now.AddMonths(-1).Year, DateTime.Now.AddMonths(-1).Month, _cutOffFrom);
            Input.DateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, _cutOffTo);
            return Page();
        }
        public IActionResult OnPostChangeFormValue()
        {
            ModelState.Clear();
            return Partial("_InputFieldsPartial", Input);
        }
        public async Task<IActionResult> OnPost(string? handler)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            ModelState.Clear();
            var tenantSales = await Mediatr.Send(new GetSalesReportQuery(Input.DateFrom, Input.DateTo, Input.ProjectId, Input.LevelId, Input.TenantId, Input.SalesCategoryCode));
            TenantSalesList = Mapper.Map<IList<TenantState>, IList<TenantViewModel>>(tenantSales);
            if (handler == "Export")
            {        
                Input.FilePath = ExportSalesToCSVReportSummaryForIFCAFileHelper.Export(WebConstants.UploadFilesPath + "\\" + WebConstants.ReportFolder,
                   _uploadPath + "\\" + WebConstants.ReportFolder, Input.DateFrom, Input.DateTo, tenantSales);              
            }
            return Page();
        }
    }
}
