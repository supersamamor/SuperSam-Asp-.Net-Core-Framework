using CTI.ELMS.Application.Features.ELMS.LeadTask.Commands;
using CTI.ELMS.Web.Areas.ELMS.Models;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.ELMS.Web.Areas.ELMS.Pages.LeadTask;

[Authorize(Policy = Permission.LeadTask.Create)]
public class AddModel : BasePageModel<AddModel>
{
    [BindProperty]
    public LeadTaskViewModel LeadTask { get; set; } = new();
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
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<AddLeadTaskCommand>(LeadTask)), "Details", true);
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
		
		
        return Partial("_InputFieldsPartial", LeadTask);
    }
	
	private IActionResult AddActivityHistory()
	{
		ModelState.Clear();
		if (LeadTask!.ActivityHistoryList == null) { LeadTask!.ActivityHistoryList = new List<ActivityHistoryViewModel>(); }
		LeadTask!.ActivityHistoryList!.Add(new ActivityHistoryViewModel() { LeadTaskId = LeadTask.Id });
		return Partial("_InputFieldsPartial", LeadTask);
	}
	private IActionResult RemoveActivityHistory()
	{
		ModelState.Clear();
		LeadTask.ActivityHistoryList = LeadTask!.ActivityHistoryList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", LeadTask);
	}
	
}
