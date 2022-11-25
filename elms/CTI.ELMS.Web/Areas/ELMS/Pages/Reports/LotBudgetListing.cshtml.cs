using CTI.ELMS.Application.Features.ELMS.Reports.Queries;
using CTI.ELMS.Web.Areas.ELMS.Models;
using CTI.ELMS.Web.Models;
using CTI.ELMS.Web.Service;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.ELMS.Web.Areas.ELMS.Pages.Reports;

[Authorize(Policy = Permission.Reports.LotBudgetListing)]
public class LotBudgetListingModel : BasePageModel<LotBudgetListingModel>
{
    private readonly string _staticFolderPath;
    public LotBudgetListingModel(IConfiguration configuration)
    {
        _staticFolderPath = configuration.GetValue<string>("UsersUpload:UploadFilesPath");
    }
    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }
    [BindProperty]
    public RotativaDocumentModel Document { get; set; } = new();
    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostListAllAsync(string projectId)
    {
        var query = DataRequest!.ToQuery<GetLotBudgetListingQuery>();
        query.ProjectId = projectId;
        var result = await Mediatr.Send(query);
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                e.Project?.ProjectName,
                e.Unit?.UnitNo,
                e.January,
                e.February,
                e.March,
                e.April,
                e.May,
                e.June,
                e.July,
                e.August,
                e.September,
                e.October,
                e.November,
                e.December,
                e.Unit?.LotArea,
                e.Year,
                e.Unit?.Location,
                e.LastModifiedDate
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    }
    public async Task<IActionResult> OnGetPreviewReport(string projectId)
    {
        var query = new GetLotBudgetListingQuery();
        query.ProjectId = projectId;
        var result = await Mediatr.Send(query);
        List<UnitBudgetViewModel> unitBudgetList = Mapper.Map<List<UnitBudgetViewModel>>(result.Data.ToList());
        var rotativaService = new RotativaService<List<UnitBudgetViewModel>>(unitBudgetList, "Reports\\Pdf\\LotBudgetListingReport", $"LotBudgetListingReport.pdf",
                                                            WebConstants.UploadFilesPath, _staticFolderPath, "Reports",
                                                            orientation: Rotativa.AspNetCore.Options.Orientation.Landscape,
                                                            size: Rotativa.AspNetCore.Options.Size.A3);
        Document = await rotativaService.GeneratePDFAsync(PageContext);
        return Partial("ReportViewer", Document);
    }
}
