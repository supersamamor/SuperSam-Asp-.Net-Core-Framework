using CTI.DSF.Application.Features.DSF.Delivery.Commands;
using CTI.DSF.Web.Areas.DSF.Models;
using CTI.DSF.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.DSF.Web.Areas.DSF.Pages.Delivery;

[Authorize(Policy = Permission.Delivery.Create)]
public class AddModel : BasePageModel<AddModel>
{
    [BindProperty]
    public DeliveryViewModel Delivery { get; set; } = new();
    [BindProperty]
    public string? RemoveSubDetailId { get; set; }
    [BindProperty]
    public string? AsyncAction { get; set; }
    public IActionResult OnGet()
    {
		
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
		
        if (!ModelState.IsValid)
        {
            return Page();
        }
		if (Delivery.DeliveryAttachmentForm != null && await UploadFile<DeliveryViewModel>(WebConstants.Delivery, nameof(Delivery.DeliveryAttachment), Delivery.Id, Delivery.DeliveryAttachmentForm) == "") { return Page(); }
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<AddDeliveryCommand>(Delivery)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		if (AsyncAction == "AddDeliveryApprovalHistory")
		{
			return AddDeliveryApprovalHistory();
		}
		if (AsyncAction == "RemoveDeliveryApprovalHistory")
		{
			return RemoveDeliveryApprovalHistory();
		}
		
		
        return Partial("_InputFieldsPartial", Delivery);
    }
	
	private IActionResult AddDeliveryApprovalHistory()
	{
		ModelState.Clear();
		if (Delivery!.DeliveryApprovalHistoryList == null) { Delivery!.DeliveryApprovalHistoryList = new List<DeliveryApprovalHistoryViewModel>(); }
		Delivery!.DeliveryApprovalHistoryList!.Add(new DeliveryApprovalHistoryViewModel() { DeliveryId = Delivery.Id });
		return Partial("_InputFieldsPartial", Delivery);
	}
	private IActionResult RemoveDeliveryApprovalHistory()
	{
		ModelState.Clear();
		Delivery.DeliveryApprovalHistoryList = Delivery!.DeliveryApprovalHistoryList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", Delivery);
	}
	
}
