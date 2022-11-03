using CTI.ELMS.Application.Features.ELMS.ActivityHistory.Commands;
using CTI.ELMS.Application.Features.ELMS.ActivityHistory.Queries;
using CTI.ELMS.Web.Areas.ELMS.Models;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.ELMS.Web.Areas.ELMS.Pages.ActivityHistory;

[Authorize(Policy = Permission.ActivityHistory.Edit)]
public class EditModel : BasePageModel<EditModel>
{
    [BindProperty]
    public ActivityHistoryViewModel ActivityHistory { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetActivityHistoryByIdQuery(id)), ActivityHistory);
    }

    public async Task<IActionResult> OnPost()
    {		
        if (!ModelState.IsValid)
        {
            return Page();
        }
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<EditActivityHistoryCommand>(ActivityHistory)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		if (AsyncAction == "AddUnitActivity")
		{
			return AddUnitActivity();
		}
		if (AsyncAction == "RemoveUnitActivity")
		{
			return RemoveUnitActivity();
		}
		
		
        return Partial("_InputFieldsPartial", ActivityHistory);
    }
	
	private IActionResult AddUnitActivity()
	{
		ModelState.Clear();
		if (ActivityHistory!.UnitActivityList == null) { ActivityHistory!.UnitActivityList = new List<UnitActivityViewModel>(); }
		ActivityHistory!.UnitActivityList!.Add(new UnitActivityViewModel() { ActivityHistoryID = ActivityHistory.Id });
		return Partial("_InputFieldsPartial", ActivityHistory);
	}
	private IActionResult RemoveUnitActivity()
	{
		ModelState.Clear();
		ActivityHistory.UnitActivityList = ActivityHistory!.UnitActivityList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", ActivityHistory);
	}
	
}
