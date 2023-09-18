using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.Assignment.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.Assignment.Queries;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.ProjectNamePlaceHolder.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.ProjectNamePlaceHolder.Pages.Assignment;

[Authorize(Policy = Permission.Assignment.Edit)]
public class EditModel : BasePageModel<EditModel>
{
    [BindProperty]
    public AssignmentViewModel Assignment { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetAssignmentByIdQuery(id)), Assignment);
    }

    public async Task<IActionResult> OnPost()
    {		
        if (!ModelState.IsValid)
        {
            return Page();
        }
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<EditAssignmentCommand>(Assignment)), "Details", true);
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
