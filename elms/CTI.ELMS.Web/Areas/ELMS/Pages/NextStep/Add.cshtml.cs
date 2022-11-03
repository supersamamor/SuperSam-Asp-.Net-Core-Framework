using CTI.ELMS.Application.Features.ELMS.NextStep.Commands;
using CTI.ELMS.Web.Areas.ELMS.Models;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.ELMS.Web.Areas.ELMS.Pages.NextStep;

[Authorize(Policy = Permission.NextStep.Create)]
public class AddModel : BasePageModel<AddModel>
{
    [BindProperty]
    public NextStepViewModel NextStep { get; set; } = new();
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
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<AddNextStepCommand>(NextStep)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		if (AsyncAction == "AddActivityHistory")
		{
			return AddActivityHistory();
		}
		if (AsyncAction == "RemoveActivityHistory")
		{
			return RemoveActivityHistory();
		}
		
		
        return Partial("_InputFieldsPartial", NextStep);
    }
	
	private IActionResult AddActivityHistory()
	{
		ModelState.Clear();
		if (NextStep!.ActivityHistoryList == null) { NextStep!.ActivityHistoryList = new List<ActivityHistoryViewModel>(); }
		NextStep!.ActivityHistoryList!.Add(new ActivityHistoryViewModel() { NextStepId = NextStep.Id });
		return Partial("_InputFieldsPartial", NextStep);
	}
	private IActionResult RemoveActivityHistory()
	{
		ModelState.Clear();
		NextStep.ActivityHistoryList = NextStep!.ActivityHistoryList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", NextStep);
	}
	
}
