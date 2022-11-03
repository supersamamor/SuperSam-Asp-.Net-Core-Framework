using CTI.ELMS.Application.Features.ELMS.Unit.Commands;
using CTI.ELMS.Web.Areas.ELMS.Models;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.ELMS.Web.Areas.ELMS.Pages.Unit;

[Authorize(Policy = Permission.Unit.Create)]
public class AddModel : BasePageModel<AddModel>
{
    [BindProperty]
    public UnitViewModel Unit { get; set; } = new();
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
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<AddUnitCommand>(Unit)), "Details", true);
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
		if (AsyncAction == "AddPreSelectedUnit")
		{
			return AddPreSelectedUnit();
		}
		if (AsyncAction == "RemovePreSelectedUnit")
		{
			return RemovePreSelectedUnit();
		}
		if (AsyncAction == "AddUnitOffered")
		{
			return AddUnitOffered();
		}
		if (AsyncAction == "RemoveUnitOffered")
		{
			return RemoveUnitOffered();
		}
		if (AsyncAction == "AddUnitOfferedHistory")
		{
			return AddUnitOfferedHistory();
		}
		if (AsyncAction == "RemoveUnitOfferedHistory")
		{
			return RemoveUnitOfferedHistory();
		}
		
		
        return Partial("_InputFieldsPartial", Unit);
    }
	
	private IActionResult AddUnitActivity()
	{
		ModelState.Clear();
		if (Unit!.UnitActivityList == null) { Unit!.UnitActivityList = new List<UnitActivityViewModel>(); }
		Unit!.UnitActivityList!.Add(new UnitActivityViewModel() { UnitID = Unit.Id });
		return Partial("_InputFieldsPartial", Unit);
	}
	private IActionResult RemoveUnitActivity()
	{
		ModelState.Clear();
		Unit.UnitActivityList = Unit!.UnitActivityList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", Unit);
	}

	private IActionResult AddPreSelectedUnit()
	{
		ModelState.Clear();
		if (Unit!.PreSelectedUnitList == null) { Unit!.PreSelectedUnitList = new List<PreSelectedUnitViewModel>(); }
		Unit!.PreSelectedUnitList!.Add(new PreSelectedUnitViewModel() { UnitID = Unit.Id });
		return Partial("_InputFieldsPartial", Unit);
	}
	private IActionResult RemovePreSelectedUnit()
	{
		ModelState.Clear();
		Unit.PreSelectedUnitList = Unit!.PreSelectedUnitList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", Unit);
	}

	private IActionResult AddUnitOffered()
	{
		ModelState.Clear();
		if (Unit!.UnitOfferedList == null) { Unit!.UnitOfferedList = new List<UnitOfferedViewModel>(); }
		Unit!.UnitOfferedList!.Add(new UnitOfferedViewModel() { UnitID = Unit.Id });
		return Partial("_InputFieldsPartial", Unit);
	}
	private IActionResult RemoveUnitOffered()
	{
		ModelState.Clear();
		Unit.UnitOfferedList = Unit!.UnitOfferedList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", Unit);
	}

	private IActionResult AddUnitOfferedHistory()
	{
		ModelState.Clear();
		if (Unit!.UnitOfferedHistoryList == null) { Unit!.UnitOfferedHistoryList = new List<UnitOfferedHistoryViewModel>(); }
		Unit!.UnitOfferedHistoryList!.Add(new UnitOfferedHistoryViewModel() { UnitID = Unit.Id });
		return Partial("_InputFieldsPartial", Unit);
	}
	private IActionResult RemoveUnitOfferedHistory()
	{
		ModelState.Clear();
		Unit.UnitOfferedHistoryList = Unit!.UnitOfferedHistoryList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", Unit);
	}
	
}
