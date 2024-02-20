using CTI.DSF.Application.Features.DSF.DeliveryApprovalHistory.Commands;
using CTI.DSF.Application.Features.DSF.DeliveryApprovalHistory.Queries;
using CTI.DSF.Web.Areas.DSF.Models;
using CTI.DSF.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.DSF.Web.Areas.DSF.Pages.DeliveryApprovalHistory;

[Authorize(Policy = Permission.DeliveryApprovalHistory.Delete)]
public class DeleteModel : BasePageModel<DeleteModel>
{
    [BindProperty]
    public DeliveryApprovalHistoryViewModel DeliveryApprovalHistory { get; set; } = new();
	[BindProperty]
    public string? RemoveSubDetailId { get; set; }
    [BindProperty]
    public string? AsyncAction { get; set; }
    public async Task<IActionResult> OnGet(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        return await PageFrom(async () => await Mediatr.Send(new GetDeliveryApprovalHistoryByIdQuery(id)), DeliveryApprovalHistory);
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        return await TryThenRedirectToPage(async () => await Mediatr.Send(new DeleteDeliveryApprovalHistoryCommand { Id = DeliveryApprovalHistory.Id }), "Index");
    }
}
