using CTI.ELMS.Application.Features.ELMS.Salutation.Commands;
using CTI.ELMS.Application.Features.ELMS.Salutation.Queries;
using CTI.ELMS.Web.Areas.ELMS.Models;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.ELMS.Web.Areas.ELMS.Pages.Salutation;

[Authorize(Policy = Permission.Salutation.Edit)]
public class EditModel : BasePageModel<EditModel>
{
    [BindProperty]
    public SalutationViewModel Salutation { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetSalutationByIdQuery(id)), Salutation);
    }

    public async Task<IActionResult> OnPost()
    {		
        if (!ModelState.IsValid)
        {
            return Page();
        }
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<EditSalutationCommand>(Salutation)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		if (AsyncAction == "AddContactPerson")
		{
			return AddContactPerson();
		}
		if (AsyncAction == "RemoveContactPerson")
		{
			return RemoveContactPerson();
		}
		
		
        return Partial("_InputFieldsPartial", Salutation);
    }
	
	private IActionResult AddContactPerson()
	{
		ModelState.Clear();
		if (Salutation!.ContactPersonList == null) { Salutation!.ContactPersonList = new List<ContactPersonViewModel>(); }
		Salutation!.ContactPersonList!.Add(new ContactPersonViewModel() { SalutationID = Salutation.Id });
		return Partial("_InputFieldsPartial", Salutation);
	}
	private IActionResult RemoveContactPerson()
	{
		ModelState.Clear();
		Salutation.ContactPersonList = Salutation!.ContactPersonList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", Salutation);
	}
	
}
