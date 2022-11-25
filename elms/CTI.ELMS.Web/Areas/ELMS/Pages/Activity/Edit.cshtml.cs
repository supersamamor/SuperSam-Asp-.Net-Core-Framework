using CTI.ELMS.Application.Features.ELMS.Activity.Commands;
using CTI.ELMS.Application.Features.ELMS.Activity.Queries;
using CTI.ELMS.Web.Areas.ELMS.Models;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.ELMS.Web.Areas.ELMS.Pages.Activity;

[Authorize(Policy = Permission.Activity.Edit)]
public class EditModel : BasePageModel<EditModel>
{
    [BindProperty]
    public ActivityViewModel Activity { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetActivityByIdQuery(id)), Activity);
    }

    public async Task<IActionResult> OnPost()
    {		
        if (!ModelState.IsValid)
        {
            return Page();
        }
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<EditActivityCommand>(Activity)), "Details", true);
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
