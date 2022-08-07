using CTI.TenantSales.Application.Features.Reports.SalesGrowthPerformance.Queries;
using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.PdfGenerator.Helper;
using CTI.TenantSales.Web.Areas.Reports.Models;
using CTI.TenantSales.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.TenantSales.Web.Areas.Reports.Pages.SalesGrowth
{
    [Authorize(Policy = Permission.Reports.SalesGrowth)]
    public class IndexModel : BasePageModel<IndexModel>
    {
        private readonly string _uploadPath;
        public IndexModel(IConfiguration configuration)
        {
            _uploadPath = configuration.GetValue<string>("UsersUpload:UploadFilesPath");
        }
        [BindProperty]
        public SalesGrowthModel Input { get; set; } = new();
        public async Task<IActionResult> OnPost()
        {
            ModelState.Clear();
            var salesGrowthData = Mapper.Map<IList<ThemeState>, IList<PdfGenerator.Models.Theme>>(await Mediatr.Send(new GetSalesGrowthPerformanceReportQuery(Input.ProjectId, Input.Year, Input.Month)));
            Input.FilePath = await SalesGrowthReportToPdf.GeneratePdf(WebConstants.UploadFilesPath + "\\" + WebConstants.ReportFolder,
               _uploadPath + "\\" + WebConstants.ReportFolder, new PdfGenerator.Models.SalesGrowthDataModel(Input.Year, Input.Month, salesGrowthData), PageContext);
            return Page();
        }
        public IActionResult OnGet()
        {
            return Page();
        }
    }
}
