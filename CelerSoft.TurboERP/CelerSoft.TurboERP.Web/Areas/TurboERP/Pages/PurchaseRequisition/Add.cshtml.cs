using CelerSoft.TurboERP.Application.Features.TurboERP.PurchaseRequisition.Commands;
using CelerSoft.TurboERP.Web.Areas.TurboERP.Models;
using CelerSoft.TurboERP.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CelerSoft.TurboERP.Web.Areas.TurboERP.Pages.PurchaseRequisition;

[Authorize(Policy = Permission.PurchaseRequisition.Create)]
public class AddModel : BasePageModel<AddModel>
{
    [BindProperty]
    public PurchaseRequisitionViewModel PurchaseRequisition { get; set; } = new();
    [BindProperty]
    public string? RemoveSubDetailId { get; set; }
    [BindProperty]
    public string? AsyncAction { get; set; }
    public AddModel()
    {
        if (PurchaseRequisition.PurchaseRequisitionItemList == null)
        {
            PurchaseRequisition.PurchaseRequisitionItemList = new List<PurchaseRequisitionItemViewModel>() { new PurchaseRequisitionItemViewModel() { PurchaseRequisitionId = PurchaseRequisition.Id } };
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

        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<AddPurchaseRequisitionCommand>(PurchaseRequisition)), "Details", true);
    }
    public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();
        if (AsyncAction == "AddPurchaseRequisitionItem")
        {
            return AddPurchaseRequisitionItem();
        }
        if (AsyncAction == "RemovePurchaseRequisitionItem")
        {
            return RemovePurchaseRequisitionItem();
        }


        return Partial("_InputFieldsPartial", PurchaseRequisition);
    }

    private IActionResult AddPurchaseRequisitionItem()
    {
        ModelState.Clear();
        if (PurchaseRequisition!.PurchaseRequisitionItemList == null) { PurchaseRequisition!.PurchaseRequisitionItemList = new List<PurchaseRequisitionItemViewModel>(); }
        PurchaseRequisition!.PurchaseRequisitionItemList!.Add(new PurchaseRequisitionItemViewModel() { PurchaseRequisitionId = PurchaseRequisition.Id });
        return Partial("_InputFieldsPartial", PurchaseRequisition);
    }
    private IActionResult RemovePurchaseRequisitionItem()
    {
        ModelState.Clear();
        PurchaseRequisition.PurchaseRequisitionItemList = PurchaseRequisition!.PurchaseRequisitionItemList!.Where(l => l.Id != RemoveSubDetailId).ToList();
        return Partial("_InputFieldsPartial", PurchaseRequisition);
    }

}
