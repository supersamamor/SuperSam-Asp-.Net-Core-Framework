using CelerSoft.TurboERP.Application.Features.TurboERP.Customer.Commands;
using CelerSoft.TurboERP.Web.Areas.TurboERP.Models;
using CelerSoft.TurboERP.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CelerSoft.TurboERP.Web.Areas.TurboERP.Pages.Customer;

[Authorize(Policy = Permission.Customer.Create)]
public class AddModel : BasePageModel<AddModel>
{
	[BindProperty]
	public CustomerViewModel Customer { get; set; } = new();
    [BindProperty]
    public string? RemoveSubDetailId { get; set; }
    [BindProperty]
    public string? AsyncAction { get; set; }
	public AddModel()
	{
		if (Customer.CustomerContactPersonList == null)
		{
			Customer.CustomerContactPersonList = new List<CustomerContactPersonViewModel>() { new CustomerContactPersonViewModel() { CustomerId = Customer.Id } };
		}

	}
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
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<AddCustomerCommand>(Customer)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		if (AsyncAction == "AddCustomerContactPerson")
		{
			return AddCustomerContactPerson();
		}
		if (AsyncAction == "RemoveCustomerContactPerson")
		{
			return RemoveCustomerContactPerson();
		}
		
		
        return Partial("_InputFieldsPartial", Customer);
    }
	
	private IActionResult AddCustomerContactPerson()
	{
		ModelState.Clear();
		if (Customer!.CustomerContactPersonList == null) { Customer!.CustomerContactPersonList = new List<CustomerContactPersonViewModel>(); }
		Customer!.CustomerContactPersonList!.Add(new CustomerContactPersonViewModel() { CustomerId = Customer.Id });
		return Partial("_InputFieldsPartial", Customer);
	}
	private IActionResult RemoveCustomerContactPerson()
	{
		ModelState.Clear();
		Customer.CustomerContactPersonList = Customer!.CustomerContactPersonList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", Customer);
	}
	
}
