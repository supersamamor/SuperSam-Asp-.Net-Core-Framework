using CTI.DSF.Application.Features.DSF.Delivery.Queries;
using CTI.DSF.Core.DSF;
using CTI.DSF.Web.Areas.DSF.Models;
using CTI.DSF.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace CTI.DSF.Web.Areas.DSF.Pages.Delivery;

[Authorize(Policy = Permission.Delivery.View)]
public class IndexModel : BasePageModel<IndexModel>
{
    public DeliveryViewModel Delivery { get; set; } = new();

    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostListAllAsync()
    {
		
        var result = await Mediatr.Send(DataRequest!.ToQuery<GetDeliveryQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                e.DeliveryCode,
				e.AssignmentCode,
                e.TaskDescription,
				DueDate = e.DueDate?.ToString("MMM dd, yyyy HH:mm"),
				e.DeliveryAttachment,
				e.Status,
                e.Remarks,
						
				
                e.LastModifiedDate
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    } 
	
	public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetDeliveryQuery>(nameof(DeliveryState.DeliveryCode)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.DeliveryCode }));
    }
}
