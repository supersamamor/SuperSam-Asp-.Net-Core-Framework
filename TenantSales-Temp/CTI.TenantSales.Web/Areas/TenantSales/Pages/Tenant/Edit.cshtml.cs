using CTI.TenantSales.Application.Features.TenantSales.Tenant.Commands;
using CTI.TenantSales.Application.Features.TenantSales.Tenant.Queries;
using CTI.TenantSales.Web.Areas.TenantSales.Models;
using CTI.TenantSales.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.TenantSales.Web.Areas.TenantSales.Pages.Tenant;

[Authorize(Policy = Permission.Tenant.Edit)]
public class EditModel : BasePageModel<EditModel>
{
    [BindProperty]
    public TenantViewModel Tenant { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetTenantByIdQuery(id)), Tenant);
    }

    public async Task<IActionResult> OnPost()
    {		
        if (!ModelState.IsValid)
        {
            return Page();
        }
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<EditTenantCommand>(Tenant)), "Details", true);
    }	
	public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		if (AsyncAction == "AddTenantLot")
		{
			return AddTenantLot();
		}
		if (AsyncAction == "RemoveTenantLot")
		{
			return RemoveTenantLot();
		}
		if (AsyncAction == "AddSalesCategory")
		{
			return AddSalesCategory();
		}
		if (AsyncAction == "RemoveSalesCategory")
		{
			return RemoveSalesCategory();
		}		
		if (AsyncAction == "RemoveTenantContact")
		{
			return RemoveTenantContact();
		}
		if (AsyncAction == "AddTenantPOS")
		{
			return AddTenantPOS();
		}
		if (AsyncAction == "RemoveTenantPOS")
		{
			return RemoveTenantPOS();
		}
		if (AsyncAction == "AddTenantBranchContact")
		{
			return AddTenantContact(Convert.ToInt32(Core.TenantSales.ContactGroup.Branch));
		}
		if (AsyncAction == "AddTenantITSupportContact")
		{
			return AddTenantContact(Convert.ToInt32(Core.TenantSales.ContactGroup.ITSupport));
		}
		if (AsyncAction == "AddTenantHeadOfficeContact")
		{
			return AddTenantContact(Convert.ToInt32(Core.TenantSales.ContactGroup.HeadOffice));
		}
		return Partial("_InputFieldsPartial", Tenant);
    }
	
	private IActionResult AddTenantLot()
	{
		ModelState.Clear();
		if (Tenant!.TenantLotList == null) { Tenant!.TenantLotList = new List<TenantLotViewModel>(); }
		Tenant!.TenantLotList!.Add(new TenantLotViewModel() { TenantId = Tenant.Id });
		return Partial("_InputFieldsPartial", Tenant);
	}
	private IActionResult RemoveTenantLot()
	{
		ModelState.Clear();
		Tenant.TenantLotList = Tenant!.TenantLotList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", Tenant);
	}

	private IActionResult AddSalesCategory()
	{
		ModelState.Clear();
		if (Tenant!.SalesCategoryList == null) { Tenant!.SalesCategoryList = new List<SalesCategoryViewModel>(); }
		Tenant!.SalesCategoryList!.Add(new SalesCategoryViewModel() { TenantId = Tenant.Id });
		return Partial("_InputFieldsPartial", Tenant);
	}
	private IActionResult RemoveSalesCategory()
	{
		ModelState.Clear();
		Tenant.SalesCategoryList = Tenant!.SalesCategoryList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", Tenant);
	}
	
	private IActionResult RemoveTenantContact()
	{
		ModelState.Clear();
		Tenant.TenantContactList = Tenant!.TenantContactList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", Tenant);
	}

	private IActionResult AddTenantPOS()
	{
		ModelState.Clear();
		if (Tenant!.TenantPOSList == null) { Tenant!.TenantPOSList = new List<TenantPOSViewModel>(); }
		Tenant!.TenantPOSList!.Add(new TenantPOSViewModel() { TenantId = Tenant.Id });
		return Partial("_InputFieldsPartial", Tenant);
	}
	private IActionResult RemoveTenantPOS()
	{
		ModelState.Clear();
		Tenant.TenantPOSList = Tenant!.TenantPOSList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", Tenant);
	}
	private IActionResult AddTenantContact(int group)
	{
		ModelState.Clear();
		if (Tenant!.TenantContactList == null) { Tenant!.TenantContactList = new List<TenantContactViewModel>(); }
		Tenant!.TenantContactList!.Add(new TenantContactViewModel() { TenantId = Tenant.Id, Group = group });
		return Partial("_InputFieldsPartial", Tenant);
	}
}
