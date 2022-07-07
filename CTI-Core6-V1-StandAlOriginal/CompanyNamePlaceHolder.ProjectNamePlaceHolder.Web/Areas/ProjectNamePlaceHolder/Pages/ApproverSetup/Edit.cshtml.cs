using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.ApproverSetup.Commands;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.ApproverSetup.Queries;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.ProjectNamePlaceHolder.Models;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Web.Areas.ProjectNamePlaceHolder.Pages.ApproverSetup;

[Authorize(Policy = Permission.ApproverSetup.Edit)]
public class EditModel : BasePageModel<EditModel>
{
    [BindProperty]
    public ApproverSetupViewModel ApproverSetup { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetApproverSetupByIdQuery(id)), ApproverSetup);
    }

    public async Task<IActionResult> OnPost()
    {		
        if (!ModelState.IsValid)
        {
            return Page();
        }
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<EditApproverSetupCommand>(ApproverSetup)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		if (AsyncAction == "AddApproverAssignment")
		{
			return AddApproverAssignment();
		}
		if (AsyncAction == "RemoveApproverAssignment")
		{
			return RemoveApproverAssignment();
		}
		
		
        return Partial("_InputFieldsPartial", ApproverSetup);
    }
	
	private IActionResult AddApproverAssignment()
	{
		ModelState.Clear();
		if (ApproverSetup!.ApproverAssignmentList == null) { ApproverSetup!.ApproverAssignmentList = new List<ApproverAssignmentViewModel>(); }
		ApproverSetup!.ApproverAssignmentList!.Add(new ApproverAssignmentViewModel() { ApproverSetupId = ApproverSetup.Id });
		return Partial("_InputFieldsPartial", ApproverSetup);
	}
	private IActionResult RemoveApproverAssignment()
	{
		ModelState.Clear();
		ApproverSetup.ApproverAssignmentList = ApproverSetup!.ApproverAssignmentList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", ApproverSetup);
	}
	
}
