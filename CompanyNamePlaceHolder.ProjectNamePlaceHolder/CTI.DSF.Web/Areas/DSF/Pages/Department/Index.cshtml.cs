using CTI.DSF.Application.Features.DSF.Department.Queries;
using CTI.DSF.Core.DSF;
using CTI.DSF.Web.Models;
using CTI.DSF.ExcelProcessor.Services;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace CTI.DSF.Web.Areas.DSF.Pages.Department;

[Authorize(Policy = Permission.Department.View)]
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
		
        var result = await Mediatr.Send(DataRequest!.ToQuery<GetDepartmentQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                CompanyCode = e.Company?.CompanyCode,
				e.DepartmentCode,
				e.DepartmentName,
				Active =  e.Active == true ? "Yes" : "No",
						
				
                e.LastModifiedDate
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    } 
	
	public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetDepartmentQuery>(nameof(DepartmentState.Id)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.Id }));
    }
	public async Task<IActionResult> OnPostBatchUploadAsync()
    {        
        return await BatchUploadAsync<IndexModel, DepartmentState>(BatchUpload.BatchUploadForm, nameof(DepartmentState), "Index");
    }

    public IActionResult OnPostDownloadTemplate()
    {
        ModelState.Clear();
		BatchUpload.BatchUploadFileName = ExcelService.ExportTemplate<DepartmentState>(_uploadPath + "\\" + WebConstants.ExcelTemplateSubFolder);
        NotyfService.Success(Localizer["Successfully downloaded upload template."]);
        return Page();
    }
}
