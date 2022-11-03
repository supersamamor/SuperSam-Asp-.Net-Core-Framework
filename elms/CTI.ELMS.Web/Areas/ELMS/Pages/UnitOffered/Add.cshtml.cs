using CTI.ELMS.Application.Features.ELMS.UnitOffered.Commands;
using CTI.ELMS.Web.Areas.ELMS.Models;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.ELMS.Web.Areas.ELMS.Pages.UnitOffered;

[Authorize(Policy = Permission.UnitOffered.Create)]
public class AddModel : BasePageModel<AddModel>
{
    [BindProperty]
    public UnitOfferedViewModel UnitOffered { get; set; } = new();
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
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<AddUnitOfferedCommand>(UnitOffered)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		if (AsyncAction == "AddAnnualIncrement")
		{
			return AddAnnualIncrement();
		}
		if (AsyncAction == "RemoveAnnualIncrement")
		{
			return RemoveAnnualIncrement();
		}
		
		
        return Partial("_InputFieldsPartial", UnitOffered);
    }
	
	private IActionResult AddAnnualIncrement()
	{
		ModelState.Clear();
		if (UnitOffered!.AnnualIncrementList == null) { UnitOffered!.AnnualIncrementList = new List<AnnualIncrementViewModel>(); }
		UnitOffered!.AnnualIncrementList!.Add(new AnnualIncrementViewModel() { UnitOfferedID = UnitOffered.Id });
		return Partial("_InputFieldsPartial", UnitOffered);
	}
	private IActionResult RemoveAnnualIncrement()
	{
		ModelState.Clear();
		UnitOffered.AnnualIncrementList = UnitOffered!.AnnualIncrementList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", UnitOffered);
	}
	
}
