using CTI.DSF.Application.Features.DSF.TaskMaster.Queries;
using CTI.DSF.Core.DSF;
using CTI.DSF.Web.Models;
using CTI.DSF.ExcelProcessor.Services;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace CTI.DSF.Web.Areas.DSF.Pages.TaskMaster;

[Authorize(Policy = Permission.TaskMaster.View)]
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
		
        var result = await Mediatr.Send(DataRequest!.ToQuery<GetTaskMasterQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                TaskNo = e.TaskNo.ToString("##,##"),
				e.TaskDescription,
				e.TaskClassification,
				e.TaskFrequency,
				TaskDueDay = e.TaskDueDay?.ToString("##,##"),
				TargetDueDate = e.TargetDueDate?.ToString("MMM dd, yyyy HH:mm"),
				Active =  e.Active == true ? "Yes" : "No",
						
				
                e.LastModifiedDate
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    } 
	
	public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetTaskMasterQuery>(nameof(TaskMasterState.Id)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.Id }));
    }
	public async Task<IActionResult> OnPostBatchUploadAsync()
    {        
        return await BatchUploadAsync<IndexModel, TaskMasterState>(BatchUpload.BatchUploadForm, nameof(TaskMasterState));
    }

    public IActionResult OnPostDownloadTemplate()
    {
        ModelState.Clear();
		BatchUpload.BatchUploadFileName = ExcelService.ExportTemplate<TaskMasterState>(_uploadPath + "\\" + WebConstants.ExcelTemplateSubFolder);
        NotyfService.Success(Localizer["Successfully downloaded upload template."]);
        return Page();
    }
}
