using CTI.ELMS.Application.Features.ELMS.OfferingHistory.Commands;
using CTI.ELMS.Web.Areas.ELMS.Models;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.ELMS.Web.Areas.ELMS.Pages.OfferingHistory;

[Authorize(Policy = Permission.OfferingHistory.Create)]
public class AddModel : BasePageModel<AddModel>
{
    [BindProperty]
    public OfferingHistoryViewModel OfferingHistory { get; set; } = new();
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
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<AddOfferingHistoryCommand>(OfferingHistory)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		if (AsyncAction == "AddUnitOfferedHistory")
		{
			return AddUnitOfferedHistory();
		}
		if (AsyncAction == "RemoveUnitOfferedHistory")
		{
			return RemoveUnitOfferedHistory();
		}
		if (AsyncAction == "AddUnitGroup")
		{
			return AddUnitGroup();
		}
		if (AsyncAction == "RemoveUnitGroup")
		{
			return RemoveUnitGroup();
		}
		
		
        return Partial("_InputFieldsPartial", OfferingHistory);
    }
	
	private IActionResult AddUnitOfferedHistory()
	{
		ModelState.Clear();
		if (OfferingHistory!.UnitOfferedHistoryList == null) { OfferingHistory!.UnitOfferedHistoryList = new List<UnitOfferedHistoryViewModel>(); }
		OfferingHistory!.UnitOfferedHistoryList!.Add(new UnitOfferedHistoryViewModel() { OfferingHistoryID = OfferingHistory.Id });
		return Partial("_InputFieldsPartial", OfferingHistory);
	}
	private IActionResult RemoveUnitOfferedHistory()
	{
		ModelState.Clear();
		OfferingHistory.UnitOfferedHistoryList = OfferingHistory!.UnitOfferedHistoryList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", OfferingHistory);
	}

	private IActionResult AddUnitGroup()
	{
		ModelState.Clear();
		if (OfferingHistory!.UnitGroupList == null) { OfferingHistory!.UnitGroupList = new List<UnitGroupViewModel>(); }
		OfferingHistory!.UnitGroupList!.Add(new UnitGroupViewModel() { OfferingHistoryID = OfferingHistory.Id });
		return Partial("_InputFieldsPartial", OfferingHistory);
	}
	private IActionResult RemoveUnitGroup()
	{
		ModelState.Clear();
		OfferingHistory.UnitGroupList = OfferingHistory!.UnitGroupList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", OfferingHistory);
	}
	
}
