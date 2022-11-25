using CTI.ELMS.Application.Features.ELMS.Offering.Commands;
using CTI.ELMS.Web.Areas.ELMS.Models;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.ELMS.Web.Areas.ELMS.Pages.Offering;

[Authorize(Policy = Permission.Offering.Create)]
public class AddModel : BasePageModel<AddModel>
{
    [BindProperty]
    public OfferingViewModel Offering { get; set; } = new();
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
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<AddOfferingCommand>(Offering)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
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
        return Partial("_InputFieldsPartial", Offering);
    }
	private IActionResult AddPreSelectedUnit()
	{
		ModelState.Clear();
		if (Offering!.PreSelectedUnitList == null) { Offering!.PreSelectedUnitList = new List<PreSelectedUnitViewModel>(); }
		Offering!.PreSelectedUnitList!.Add(new PreSelectedUnitViewModel() { OfferingID = Offering.Id });
		return Partial("_InputFieldsPartial", Offering);
	}
	private IActionResult RemovePreSelectedUnit()
	{
		ModelState.Clear();
		Offering.PreSelectedUnitList = Offering!.PreSelectedUnitList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", Offering);
	}

	private IActionResult AddUnitOffered()
	{
		ModelState.Clear();
		if (Offering!.UnitOfferedList == null) { Offering!.UnitOfferedList = new List<UnitOfferedViewModel>(); }
		Offering!.UnitOfferedList!.Add(new UnitOfferedViewModel() { OfferingID = Offering.Id });
		return Partial("_InputFieldsPartial", Offering);
	}
	private IActionResult RemoveUnitOffered()
	{
		ModelState.Clear();
		Offering.UnitOfferedList = Offering!.UnitOfferedList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", Offering);
	}
}
