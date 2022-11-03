using CTI.ELMS.Application.Features.ELMS.UnitOfferedHistory.Commands;
using CTI.ELMS.Web.Areas.ELMS.Models;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.ELMS.Web.Areas.ELMS.Pages.UnitOfferedHistory;

[Authorize(Policy = Permission.UnitOfferedHistory.Create)]
public class AddModel : BasePageModel<AddModel>
{
    [BindProperty]
    public UnitOfferedHistoryViewModel UnitOfferedHistory { get; set; } = new();
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
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<AddUnitOfferedHistoryCommand>(UnitOfferedHistory)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		if (AsyncAction == "AddAnnualIncrementHistory")
		{
			return AddAnnualIncrementHistory();
		}
		if (AsyncAction == "RemoveAnnualIncrementHistory")
		{
			return RemoveAnnualIncrementHistory();
		}
		
		
        return Partial("_InputFieldsPartial", UnitOfferedHistory);
    }
	
	private IActionResult AddAnnualIncrementHistory()
	{
		ModelState.Clear();
		if (UnitOfferedHistory!.AnnualIncrementHistoryList == null) { UnitOfferedHistory!.AnnualIncrementHistoryList = new List<AnnualIncrementHistoryViewModel>(); }
		UnitOfferedHistory!.AnnualIncrementHistoryList!.Add(new AnnualIncrementHistoryViewModel() { UnitOfferedHistoryID = UnitOfferedHistory.Id });
		return Partial("_InputFieldsPartial", UnitOfferedHistory);
	}
	private IActionResult RemoveAnnualIncrementHistory()
	{
		ModelState.Clear();
		UnitOfferedHistory.AnnualIncrementHistoryList = UnitOfferedHistory!.AnnualIncrementHistoryList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", UnitOfferedHistory);
	}
	
}
