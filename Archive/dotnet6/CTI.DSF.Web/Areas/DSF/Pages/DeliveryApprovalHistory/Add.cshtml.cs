using CTI.DSF.Application.Features.DSF.DeliveryApprovalHistory.Commands;
using CTI.DSF.Web.Areas.DSF.Models;
using CTI.DSF.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.DSF.Web.Areas.DSF.Pages.DeliveryApprovalHistory;

[Authorize(Policy = Permission.DeliveryApprovalHistory.Create)]
public class AddModel : BasePageModel<AddModel>
{
    [BindProperty]
    public DeliveryApprovalHistoryViewModel DeliveryApprovalHistory { get; set; } = new();
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
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<AddDeliveryApprovalHistoryCommand>(DeliveryApprovalHistory)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		
        return Partial("_InputFieldsPartial", DeliveryApprovalHistory);
    }
	
}
