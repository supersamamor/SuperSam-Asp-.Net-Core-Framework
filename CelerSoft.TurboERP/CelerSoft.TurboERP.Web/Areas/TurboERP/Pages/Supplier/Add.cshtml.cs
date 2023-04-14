using CelerSoft.TurboERP.Application.Features.TurboERP.Supplier.Commands;
using CelerSoft.TurboERP.Web.Areas.TurboERP.Models;
using CelerSoft.TurboERP.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CelerSoft.TurboERP.Web.Areas.TurboERP.Pages.Supplier;

[Authorize(Policy = Permission.Supplier.Create)]
public class AddModel : BasePageModel<AddModel>
{
    [BindProperty]
	public SupplierViewModel Supplier { get; set; } = new();
	[BindProperty]
    public string? RemoveSubDetailId { get; set; }
    [BindProperty]
    public string? AsyncAction { get; set; }
	public AddModel()
	{
		if (Supplier.SupplierContactPersonList == null)
		{
			Supplier.SupplierContactPersonList = new List<SupplierContactPersonViewModel>() { new SupplierContactPersonViewModel() { SupplierId = Supplier.Id } };
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
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<AddSupplierCommand>(Supplier)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		if (AsyncAction == "AddSupplierContactPerson")
		{
			return AddSupplierContactPerson();
		}
		if (AsyncAction == "RemoveSupplierContactPerson")
		{
			return RemoveSupplierContactPerson();
		}
		
		
        return Partial("_InputFieldsPartial", Supplier);
    }
	
	private IActionResult AddSupplierContactPerson()
	{
		ModelState.Clear();
		if (Supplier!.SupplierContactPersonList == null) { Supplier!.SupplierContactPersonList = new List<SupplierContactPersonViewModel>(); }
		Supplier!.SupplierContactPersonList!.Add(new SupplierContactPersonViewModel() { SupplierId = Supplier.Id });
		return Partial("_InputFieldsPartial", Supplier);
	}
	private IActionResult RemoveSupplierContactPerson()
	{
		ModelState.Clear();
		Supplier.SupplierContactPersonList = Supplier!.SupplierContactPersonList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", Supplier);
	}
	
}
