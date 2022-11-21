using CTI.ELMS.Application.Features.ELMS.Offering.Commands;
using CTI.ELMS.Application.Features.ELMS.Offering.Queries;
using CTI.ELMS.Web.Areas.ELMS.Models;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.ELMS.Web.Areas.ELMS.Pages.Offering;

[Authorize(Policy = Permission.Offering.Edit)]
public class EditModel : BasePageModel<EditModel>
{
    [BindProperty]
    public OfferingViewModel Offering { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetOfferingByIdQuery(id)), Offering);
    }

    public async Task<IActionResult> OnPost()
    {		
        if (!ModelState.IsValid)
        {
            return Page();
        }
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<EditOfferingCommand>(Offering)), "Details", true);
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
	
	private IActionResult AddOfferingHistory()
	{
		ModelState.Clear();
		if (Offering!.OfferingHistoryList == null) { Offering!.OfferingHistoryList = new List<OfferingHistoryViewModel>(); }
		Offering!.OfferingHistoryList!.Add(new OfferingHistoryViewModel() { OfferingID = Offering.Id });
		return Partial("_InputFieldsPartial", Offering);
	}
	private IActionResult RemoveOfferingHistory()
	{
		ModelState.Clear();
		Offering.OfferingHistoryList = Offering!.OfferingHistoryList!.Where(l => l.Id != RemoveSubDetailId).ToList();
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
