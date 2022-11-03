using CTI.ELMS.Application.Features.ELMS.Activity.Commands;
using CTI.ELMS.Web.Areas.ELMS.Models;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.ELMS.Web.Areas.ELMS.Pages.Activity;

[Authorize(Policy = Permission.Activity.Create)]
public class AddModel : BasePageModel<AddModel>
{
    [BindProperty]
    public ActivityViewModel Activity { get; set; } = new();
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
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<AddActivityCommand>(Activity)), "Details", true);
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
		if (AsyncAction == "AddUnitActivity")
		{
			return AddUnitActivity();
		}
		if (AsyncAction == "RemoveUnitActivity")
		{
			return RemoveUnitActivity();
		}
		
		
        return Partial("_InputFieldsPartial", Activity);
    }
	
	private IActionResult AddActivityHistory()
	{
		ModelState.Clear();
		if (Activity!.ActivityHistoryList == null) { Activity!.ActivityHistoryList = new List<ActivityHistoryViewModel>(); }
		Activity!.ActivityHistoryList!.Add(new ActivityHistoryViewModel() { ActivityID = Activity.Id });
		return Partial("_InputFieldsPartial", Activity);
	}
	private IActionResult RemoveActivityHistory()
	{
		ModelState.Clear();
		Activity.ActivityHistoryList = Activity!.ActivityHistoryList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", Activity);
	}

	private IActionResult AddUnitActivity()
	{
		ModelState.Clear();
		if (Activity!.UnitActivityList == null) { Activity!.UnitActivityList = new List<UnitActivityViewModel>(); }
		Activity!.UnitActivityList!.Add(new UnitActivityViewModel() { ActivityID = Activity.Id });
		return Partial("_InputFieldsPartial", Activity);
	}
	private IActionResult RemoveUnitActivity()
	{
		ModelState.Clear();
		Activity.UnitActivityList = Activity!.UnitActivityList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", Activity);
	}
	
}
