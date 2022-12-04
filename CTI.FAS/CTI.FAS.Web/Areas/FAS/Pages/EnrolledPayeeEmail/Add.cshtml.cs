using CTI.FAS.Application.Features.FAS.EnrolledPayeeEmail.Commands;
using CTI.FAS.Web.Areas.FAS.Models;
using CTI.FAS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.FAS.Web.Areas.FAS.Pages.EnrolledPayeeEmail;

[Authorize(Policy = Permission.EnrolledPayeeEmail.Create)]
public class AddModel : BasePageModel<AddModel>
{
    [BindProperty]
    public EnrolledPayeeEmailViewModel EnrolledPayeeEmail { get; set; } = new();
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
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<AddEnrolledPayeeEmailCommand>(EnrolledPayeeEmail)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		
        return Partial("_InputFieldsPartial", EnrolledPayeeEmail);
    }
	
}
