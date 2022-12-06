using CTI.FAS.Application.Features.FAS.Creditor.Queries;
using CTI.FAS.Application.Features.FAS.EnrolledPayee.Commands;
using CTI.FAS.Application.Features.FAS.EnrolledPayee.Queries;
using CTI.FAS.Web.Areas.FAS.Models;
using CTI.FAS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.FAS.Web.Areas.FAS.Pages.EnrolledPayee;

[Authorize(Policy = Permission.EnrolledPayee.ReEnroll)]
public class ReEnrollModel : BasePageModel<ReEnrollModel>
{
    [BindProperty]
    public EnrolledPayeeViewModel EnrolledPayee { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetEnrolledPayeeByIdQuery(id)), EnrolledPayee);
    }

    public async Task<IActionResult> OnPost()
    {		
        if (!ModelState.IsValid)
        {
            return Page();
        }		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<ReEnrollPayeeCommand>(EnrolledPayee)), "ReEnroll", true);
    }	
	public async Task<IActionResult> OnPostChangeFormValue()
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
        if (AsyncAction == "GetDefaultEmail")
        {
            return await GetDefaultEmail();
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
    private async Task<IActionResult> GetDefaultEmail()
    {
        ModelState.Clear();
        _ = (await Mediatr.Send(new GetCreditorByIdQuery(EnrolledPayee.CreditorId))).Select(l => EnrolledPayee.Email = l.Email);
        return Partial("_InputFieldsPartial", EnrolledPayee);
    }
}
