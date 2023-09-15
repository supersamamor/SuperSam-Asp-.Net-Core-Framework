using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.Delivery.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.Delivery.Queries;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.AreaPlaceHolder.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.AreaPlaceHolder.Pages.Delivery;

[Authorize(Policy = Permission.Delivery.Edit)]
public class EditModel : BasePageModel<EditModel>
{
    [BindProperty]
    public DeliveryViewModel Delivery { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetDeliveryByIdQuery(id)), Delivery);
    }

    public async Task<IActionResult> OnPost()
    {		
        if (!ModelState.IsValid)
        {
            return Page();
        }
		if (Delivery.DeliveryAttachmentForm != null && await UploadFile<DeliveryViewModel>(WebConstants.Delivery, nameof(Delivery.DeliveryAttachment), Delivery.Id, Delivery.DeliveryAttachmentForm) == "") { return Page(); }
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<EditDeliveryCommand>(Delivery)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		
        return Partial("_InputFieldsPartial", Delivery);
    }
	
}
