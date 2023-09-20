using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.Delivery.Queries;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.ProjectNamePlaceHolder.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Models;
using DataTables.AspNetCore.Mvc.Binder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Helper;


namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.ProjectNamePlaceHolder.Pages.Delivery;

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
		var approvalHelper = new ApprovalHelper(Mediatr);
        var result = await Mediatr.Send(DataRequest!.ToQuery<GetDeliveryQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                e.DeliveryCode,
				e.ApprovedTag,
				e.AssignmentCode,
				e.DeliveryAttachment,
				EndorsedDate = e.EndorsedDate?.ToString("MMM dd, yyyy HH:mm"),
				e.ActualDeliveryRemarks,
				e.ApproverRemarks,
				e.EndorsedTag,
				ActualDeliveryDate = e.ActualDeliveryDate.ToString("MMM dd, yyyy HH:mm"),
				e.Status,
				DueDate = e.DueDate?.ToString("MMM dd, yyyy HH:mm"),
				e.EndorserRemarks,
						
				StatusBadge = approvalHelper.GetApprovalStatus(e.Id),
                e.LastModifiedDate
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.MetaData.TotalItemCount));
    } 
	
	public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetDeliveryQuery>(nameof(DeliveryState.Id)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.Id }));
    }
}
