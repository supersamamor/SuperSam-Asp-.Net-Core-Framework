using CTI.DSF.Application.Features.DSF.DeliveryApprovalHistory.Queries;
using CTI.DSF.Core.DSF;
using CTI.DSF.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace CTI.DSF.Web.Areas.DSF.Pages.DeliveryApprovalHistory;

[Authorize(Policy = Permission.DeliveryApprovalHistory.View)]
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
		
        var result = await Mediatr.Send(DataRequest!.ToQuery<GetDeliveryApprovalHistoryQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                DeliveryId = e.Delivery?.Id,
				TransactionDateTime = e.TransactionDateTime?.ToString("MMM dd, yyyy HH:mm"),
				e.Status,
						
				
                e.LastModifiedDate
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    } 
	
	public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetDeliveryApprovalHistoryQuery>(nameof(DeliveryApprovalHistoryState.Id)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.Id }));
    }
	public async Task<IActionResult> OnPostBatchUploadAsync()
    {        
        return await BatchUploadAsync<IndexModel, DeliveryApprovalHistoryState>(BatchUpload.BatchUploadForm, nameof(DeliveryApprovalHistoryState));
    }

    public IActionResult OnPostDownloadTemplate()
    {
        ModelState.Clear();
        NotyfService.Success(Localizer["Successfully downloaded upload template."]);
        return Page();
    }
}
