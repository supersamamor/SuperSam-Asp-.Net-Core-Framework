using CTI.ELMS.Application.Features.ELMS.Project.Commands;
using CTI.ELMS.Web.Areas.ELMS.Models;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.ELMS.Web.Areas.ELMS.Pages.Project;

[Authorize(Policy = Permission.Project.Create)]
public class AddModel : BasePageModel<AddModel>
{
    [BindProperty]
    public ProjectViewModel Project { get; set; } = new();
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
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<AddProjectCommand>(Project)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		if (AsyncAction == "AddOfferingHistory")
		{
			return AddOfferingHistory();
		}
		if (AsyncAction == "RemoveOfferingHistory")
		{
			return RemoveOfferingHistory();
		}
		
		
        return Partial("_InputFieldsPartial", Project);
    }
	
	private IActionResult AddOfferingHistory()
	{
		ModelState.Clear();
		if (Project!.OfferingHistoryList == null) { Project!.OfferingHistoryList = new List<OfferingHistoryViewModel>(); }
		Project!.OfferingHistoryList!.Add(new OfferingHistoryViewModel() { ProjectID = Project.Id });
		return Partial("_InputFieldsPartial", Project);
	}
	private IActionResult RemoveOfferingHistory()
	{
		ModelState.Clear();
		Project.OfferingHistoryList = Project!.OfferingHistoryList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", Project);
	}
	
}
