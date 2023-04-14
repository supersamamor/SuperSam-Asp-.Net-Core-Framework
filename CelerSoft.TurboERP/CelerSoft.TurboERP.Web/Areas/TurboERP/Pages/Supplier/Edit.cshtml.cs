using CelerSoft.TurboERP.Application.Features.TurboERP.Supplier.Commands;
using CelerSoft.TurboERP.Application.Features.TurboERP.Supplier.Queries;
using CelerSoft.TurboERP.Web.Areas.TurboERP.Models;
using CelerSoft.TurboERP.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CelerSoft.TurboERP.Web.Areas.TurboERP.Pages.Supplier;

[Authorize(Policy = Permission.Supplier.Edit)]
public class EditModel : BasePageModel<EditModel>
{
    [BindProperty]
    public SupplierViewModel Supplier { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetSupplierByIdQuery(id)), Supplier);
    }

    public async Task<IActionResult> OnPost()
    {		
        if (!ModelState.IsValid)
        {
            return Page();
        }
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<EditSupplierCommand>(Supplier)), "Details", true);
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
