using CTI.DSF.Application.Features.DSF.TaskTag.Queries;
using CTI.DSF.Core.DSF;
using CTI.DSF.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace CTI.DSF.Web.Areas.DSF.Pages.TaskTag;

[Authorize(Policy = Permission.TaskTag.View)]
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
		
        var result = await Mediatr.Send(DataRequest!.ToQuery<GetTaskTagQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                TaskMasterId = e.TaskMaster?.Id,
				TagId = e.Tags?.Name,
						
				
                e.LastModifiedDate
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    } 
	
	public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetTaskTagQuery>(nameof(TaskTagState.Id)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.Id }));
    }
	public async Task<IActionResult> OnPostBatchUploadAsync()
    {        
        return await BatchUploadAsync<IndexModel, TaskTagState>(BatchUpload.BatchUploadForm, nameof(TaskTagState));
    }

    public IActionResult OnPostDownloadTemplate()
    {
        ModelState.Clear();
        BatchUpload.BatchUploadFileName = GetTemplatePath<TaskTagState>(_uploadPath);
        NotyfService.Success(Localizer["Successfully downloaded upload template."]);
        return Page();
    }
}
