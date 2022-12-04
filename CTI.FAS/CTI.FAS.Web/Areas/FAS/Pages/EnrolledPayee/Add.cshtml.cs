using CTI.FAS.Application.Features.FAS.EnrolledPayee.Commands;
using CTI.FAS.Web.Areas.FAS.Models;
using CTI.FAS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.FAS.Web.Areas.FAS.Pages.EnrolledPayee;

[Authorize(Policy = Permission.EnrolledPayee.Create)]
public class AddModel : BasePageModel<AddModel>
{
    [BindProperty]
    public EnrolledPayeeViewModel EnrolledPayee { get; set; } = new();
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
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<AddEnrolledPayeeCommand>(EnrolledPayee)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		if (AsyncAction == "AddEnrolledPayeeEmail")
		{
			return AddEnrolledPayeeEmail();
		}
		if (AsyncAction == "RemoveEnrolledPayeeEmail")
		{
			return RemoveEnrolledPayeeEmail();
		}
		
		
        return Partial("_InputFieldsPartial", EnrolledPayee);
    }
	
	private IActionResult AddEnrolledPayeeEmail()
	{
		ModelState.Clear();
		if (EnrolledPayee!.EnrolledPayeeEmailList == null) { EnrolledPayee!.EnrolledPayeeEmailList = new List<EnrolledPayeeEmailViewModel>(); }
		EnrolledPayee!.EnrolledPayeeEmailList!.Add(new EnrolledPayeeEmailViewModel() { EnrolledPayeeId = EnrolledPayee.Id });
		return Partial("_InputFieldsPartial", EnrolledPayee);
	}
	private IActionResult RemoveEnrolledPayeeEmail()
	{
		ModelState.Clear();
		EnrolledPayee.EnrolledPayeeEmailList = EnrolledPayee!.EnrolledPayeeEmailList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", EnrolledPayee);
	}
	
}
