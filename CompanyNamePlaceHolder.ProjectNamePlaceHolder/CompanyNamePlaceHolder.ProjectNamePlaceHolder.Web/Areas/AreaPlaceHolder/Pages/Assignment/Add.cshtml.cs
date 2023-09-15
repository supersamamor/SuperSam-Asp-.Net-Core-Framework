using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.AreaPlaceHolder.Assignment.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.AreaPlaceHolder.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.AreaPlaceHolder.Pages.Assignment;

[Authorize(Policy = Permission.Assignment.Create)]
public class AddModel : BasePageModel<AddModel>
{
    [BindProperty]
    public AssignmentViewModel Assignment { get; set; } = new();
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
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<AddAssignmentCommand>(Assignment)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		if (AsyncAction == "AddDelivery")
		{
			return AddDelivery();
		}
		if (AsyncAction == "RemoveDelivery")
		{
			return RemoveDelivery();
		}
		
		
        return Partial("_InputFieldsPartial", Assignment);
    }
	
	private IActionResult AddDelivery()
	{
		ModelState.Clear();
		if (Assignment!.DeliveryList == null) { Assignment!.DeliveryList = new List<DeliveryViewModel>(); }
		Assignment!.DeliveryList!.Add(new DeliveryViewModel() { AssignmentCode = Assignment.Id });
		return Partial("_InputFieldsPartial", Assignment);
	}
	private IActionResult RemoveDelivery()
	{
		ModelState.Clear();
		Assignment.DeliveryList = Assignment!.DeliveryList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", Assignment);
	}
	
}
