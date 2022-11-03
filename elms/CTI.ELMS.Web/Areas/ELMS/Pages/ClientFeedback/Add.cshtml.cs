using CTI.ELMS.Application.Features.ELMS.ClientFeedback.Commands;
using CTI.ELMS.Web.Areas.ELMS.Models;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.ELMS.Web.Areas.ELMS.Pages.ClientFeedback;

[Authorize(Policy = Permission.ClientFeedback.Create)]
public class AddModel : BasePageModel<AddModel>
{
    [BindProperty]
    public ClientFeedbackViewModel ClientFeedback { get; set; } = new();
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
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<AddClientFeedbackCommand>(ClientFeedback)), "Details", true);
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
		
		
        return Partial("_InputFieldsPartial", ClientFeedback);
    }
	
	private IActionResult AddActivityHistory()
	{
		ModelState.Clear();
		if (ClientFeedback!.ActivityHistoryList == null) { ClientFeedback!.ActivityHistoryList = new List<ActivityHistoryViewModel>(); }
		ClientFeedback!.ActivityHistoryList!.Add(new ActivityHistoryViewModel() { ClientFeedbackId = ClientFeedback.Id });
		return Partial("_InputFieldsPartial", ClientFeedback);
	}
	private IActionResult RemoveActivityHistory()
	{
		ModelState.Clear();
		ClientFeedback.ActivityHistoryList = ClientFeedback!.ActivityHistoryList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", ClientFeedback);
	}
	
}
