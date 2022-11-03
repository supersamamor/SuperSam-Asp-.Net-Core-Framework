using CTI.ELMS.Application.Features.ELMS.Lead.Commands;
using CTI.ELMS.Web.Areas.ELMS.Models;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.ELMS.Web.Areas.ELMS.Pages.Lead;

[Authorize(Policy = Permission.Lead.Create)]
public class AddModel : BasePageModel<AddModel>
{
    [BindProperty]
    public LeadViewModel Lead { get; set; } = new();
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
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<AddLeadCommand>(Lead)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		if (AsyncAction == "AddContact")
		{
			return AddContact();
		}
		if (AsyncAction == "RemoveContact")
		{
			return RemoveContact();
		}
		if (AsyncAction == "AddContactPerson")
		{
			return AddContactPerson();
		}
		if (AsyncAction == "RemoveContactPerson")
		{
			return RemoveContactPerson();
		}
		if (AsyncAction == "AddOfferingHistory")
		{
			return AddOfferingHistory();
		}
		if (AsyncAction == "RemoveOfferingHistory")
		{
			return RemoveOfferingHistory();
		}
		
		
        return Partial("_InputFieldsPartial", Lead);
    }
	
	private IActionResult AddContact()
	{
		ModelState.Clear();
		if (Lead!.ContactList == null) { Lead!.ContactList = new List<ContactViewModel>(); }
		Lead!.ContactList!.Add(new ContactViewModel() { LeadID = Lead.Id });
		return Partial("_InputFieldsPartial", Lead);
	}
	private IActionResult RemoveContact()
	{
		ModelState.Clear();
		Lead.ContactList = Lead!.ContactList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", Lead);
	}

	private IActionResult AddContactPerson()
	{
		ModelState.Clear();
		if (Lead!.ContactPersonList == null) { Lead!.ContactPersonList = new List<ContactPersonViewModel>(); }
		Lead!.ContactPersonList!.Add(new ContactPersonViewModel() { LeadId = Lead.Id });
		return Partial("_InputFieldsPartial", Lead);
	}
	private IActionResult RemoveContactPerson()
	{
		ModelState.Clear();
		Lead.ContactPersonList = Lead!.ContactPersonList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", Lead);
	}

	private IActionResult AddOfferingHistory()
	{
		ModelState.Clear();
		if (Lead!.OfferingHistoryList == null) { Lead!.OfferingHistoryList = new List<OfferingHistoryViewModel>(); }
		Lead!.OfferingHistoryList!.Add(new OfferingHistoryViewModel() { LeadID = Lead.Id });
		return Partial("_InputFieldsPartial", Lead);
	}
	private IActionResult RemoveOfferingHistory()
	{
		ModelState.Clear();
		Lead.OfferingHistoryList = Lead!.OfferingHistoryList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", Lead);
	}
	
}
