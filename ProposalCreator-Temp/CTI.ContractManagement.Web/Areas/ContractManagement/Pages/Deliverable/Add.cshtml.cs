using CTI.ContractManagement.Application.Features.ContractManagement.Deliverable.Commands;
using CTI.ContractManagement.Web.Areas.ContractManagement.Models;
using CTI.ContractManagement.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.ContractManagement.Web.Areas.ContractManagement.Pages.Deliverable;

[Authorize(Policy = Permission.Deliverable.Create)]
public class AddModel : BasePageModel<AddModel>
{
    [BindProperty]
    public DeliverableViewModel Deliverable { get; set; } = new();
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
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<AddDeliverableCommand>(Deliverable)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		if (AsyncAction == "AddProjectDeliverable")
		{
			return AddProjectDeliverable();
		}
		if (AsyncAction == "RemoveProjectDeliverable")
		{
			return RemoveProjectDeliverable();
		}
		if (AsyncAction == "AddProjectPackageAdditionalDeliverable")
		{
			return AddProjectPackageAdditionalDeliverable();
		}
		if (AsyncAction == "RemoveProjectPackageAdditionalDeliverable")
		{
			return RemoveProjectPackageAdditionalDeliverable();
		}
		if (AsyncAction == "AddProjectDeliverableHistory")
		{
			return AddProjectDeliverableHistory();
		}
		if (AsyncAction == "RemoveProjectDeliverableHistory")
		{
			return RemoveProjectDeliverableHistory();
		}
		if (AsyncAction == "AddProjectPackageAdditionalDeliverableHistory")
		{
			return AddProjectPackageAdditionalDeliverableHistory();
		}
		if (AsyncAction == "RemoveProjectPackageAdditionalDeliverableHistory")
		{
			return RemoveProjectPackageAdditionalDeliverableHistory();
		}
		
		
        return Partial("_InputFieldsPartial", Deliverable);
    }
	
	private IActionResult AddProjectDeliverable()
	{
		ModelState.Clear();
		if (Deliverable!.ProjectDeliverableList == null) { Deliverable!.ProjectDeliverableList = new List<ProjectDeliverableViewModel>(); }
		Deliverable!.ProjectDeliverableList!.Add(new ProjectDeliverableViewModel() { DeliverableId = Deliverable.Id });
		return Partial("_InputFieldsPartial", Deliverable);
	}
	private IActionResult RemoveProjectDeliverable()
	{
		ModelState.Clear();
		Deliverable.ProjectDeliverableList = Deliverable!.ProjectDeliverableList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", Deliverable);
	}

	private IActionResult AddProjectPackageAdditionalDeliverable()
	{
		ModelState.Clear();
		if (Deliverable!.ProjectPackageAdditionalDeliverableList == null) { Deliverable!.ProjectPackageAdditionalDeliverableList = new List<ProjectPackageAdditionalDeliverableViewModel>(); }
		Deliverable!.ProjectPackageAdditionalDeliverableList!.Add(new ProjectPackageAdditionalDeliverableViewModel() { DeliverableId = Deliverable.Id });
		return Partial("_InputFieldsPartial", Deliverable);
	}
	private IActionResult RemoveProjectPackageAdditionalDeliverable()
	{
		ModelState.Clear();
		Deliverable.ProjectPackageAdditionalDeliverableList = Deliverable!.ProjectPackageAdditionalDeliverableList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", Deliverable);
	}

	private IActionResult AddProjectDeliverableHistory()
	{
		ModelState.Clear();
		if (Deliverable!.ProjectDeliverableHistoryList == null) { Deliverable!.ProjectDeliverableHistoryList = new List<ProjectDeliverableHistoryViewModel>(); }
		Deliverable!.ProjectDeliverableHistoryList!.Add(new ProjectDeliverableHistoryViewModel() { DeliverableId = Deliverable.Id });
		return Partial("_InputFieldsPartial", Deliverable);
	}
	private IActionResult RemoveProjectDeliverableHistory()
	{
		ModelState.Clear();
		Deliverable.ProjectDeliverableHistoryList = Deliverable!.ProjectDeliverableHistoryList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", Deliverable);
	}

	private IActionResult AddProjectPackageAdditionalDeliverableHistory()
	{
		ModelState.Clear();
		if (Deliverable!.ProjectPackageAdditionalDeliverableHistoryList == null) { Deliverable!.ProjectPackageAdditionalDeliverableHistoryList = new List<ProjectPackageAdditionalDeliverableHistoryViewModel>(); }
		Deliverable!.ProjectPackageAdditionalDeliverableHistoryList!.Add(new ProjectPackageAdditionalDeliverableHistoryViewModel() { DeliverableId = Deliverable.Id });
		return Partial("_InputFieldsPartial", Deliverable);
	}
	private IActionResult RemoveProjectPackageAdditionalDeliverableHistory()
	{
		ModelState.Clear();
		Deliverable.ProjectPackageAdditionalDeliverableHistoryList = Deliverable!.ProjectPackageAdditionalDeliverableHistoryList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", Deliverable);
	}
	
}
