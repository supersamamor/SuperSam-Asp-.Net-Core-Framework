using CompanyPL.ProjectPL.Application.Features.ProjectPL.Employee.Queries;
using CompanyPL.ProjectPL.Core.ProjectPL;
using CompanyPL.ProjectPL.Web.Models;
using CompanyPL.ProjectPL.ExcelProcessor.Services;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CompanyPL.ProjectPL.Web.Helper;


namespace CompanyPL.ProjectPL.Web.Areas.ProjectPL.Pages.Employee;

[Authorize(Policy = Permission.Employee.View)]
public class IndexModel : BasePageModel<IndexModel>
{   
	private readonly string? _uploadPath;
    public IndexModel(IConfiguration configuration)
    {
        _uploadPath = configuration.GetValue<string>("UsersUpload:UploadFilesPath");
    }
    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }
	[BindProperty]
    public BatchUploadModel BatchUpload { get; set; } = new();
    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostListAllAsync()
    {
		var approvalHelper = new ApprovalHelper(Mediatr);
        var result = await Mediatr.Send(DataRequest!.ToQuery<GetEmployeeQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                e.FirstName,
				e.EmployeeCode,
				e.LastName,
				e.MiddleName,
						
				StatusBadge = approvalHelper.GetApprovalStatus(e.Id),
                e.LastModifiedDate
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    } 
	
	public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetEmployeeQuery>(nameof(EmployeeState.EmployeeCode)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.EmployeeCode }));
    }
	public async Task<IActionResult> OnPostBatchUploadAsync()
    {        
        return await BatchUploadAsync<IndexModel, EmployeeState>(BatchUpload.BatchUploadForm, nameof(EmployeeState), "Index");
    }

    public IActionResult OnPostDownloadTemplate()
    {
        ModelState.Clear();
		BatchUpload.BatchUploadFileName = ExcelService.ExportTemplate<EmployeeState>(_uploadPath + "\\" + WebConstants.ExcelTemplateSubFolder);
        NotyfService.Success(Localizer["Successfully downloaded upload template."]);
        return Page();
    }
}
