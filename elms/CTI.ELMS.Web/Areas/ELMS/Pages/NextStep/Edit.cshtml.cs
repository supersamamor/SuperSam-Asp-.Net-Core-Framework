using CTI.ELMS.Application.Features.ELMS.NextStep.Commands;
using CTI.ELMS.Application.Features.ELMS.NextStep.Queries;
using CTI.ELMS.Web.Areas.ELMS.Models;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.ELMS.Web.Areas.ELMS.Pages.NextStep;

[Authorize(Policy = Permission.NextStep.Edit)]
public class EditModel : BasePageModel<EditModel>
{
    [BindProperty]
    public NextStepViewModel NextStep { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetNextStepByIdQuery(id)), NextStep);
    }

    public async Task<IActionResult> OnPost()
    {		
        if (!ModelState.IsValid)
        {
            return Page();
        }
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<EditNextStepCommand>(NextStep)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		if (AsyncAction == "AddLeadTaskNextStep")
		{
			return AddLeadTaskNextStep();
		}
		if (AsyncAction == "RemoveLeadTaskNextStep")
		{
			return RemoveLeadTaskNextStep();
		}
        return Partial("_InputFieldsPartial", NextStep);
    }
	
	private IActionResult AddLeadTaskNextStep()
	{
		ModelState.Clear();
		if (NextStep!.LeadTaskNextStepList == null) { NextStep!.LeadTaskNextStepList = new List<LeadTaskNextStepViewModel>(); }
		NextStep!.LeadTaskNextStepList!.Add(new LeadTaskNextStepViewModel() { NextStepId = NextStep.Id });
		return Partial("_InputFieldsPartial", NextStep);
	}
	private IActionResult RemoveLeadTaskNextStep()
	{
		ModelState.Clear();
		NextStep.LeadTaskNextStepList = NextStep!.LeadTaskNextStepList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", NextStep);
	}
}
