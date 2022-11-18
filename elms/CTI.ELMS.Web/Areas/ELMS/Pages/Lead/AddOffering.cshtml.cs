using CTI.ELMS.Application.Features.ELMS.Offering.Commands;
using CTI.ELMS.Application.Features.ELMS.TabNavigation.Queries;
using CTI.ELMS.Application.Features.ELMS.Unit.Queries;
using CTI.ELMS.Web.Areas.ELMS.Models;
using CTI.ELMS.Web.Helper;
using CTI.ELMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.ELMS.Web.Areas.ELMS.Pages.Lead;

[Authorize(Policy = Permission.Offering.Create)]
public class AddOfferingModel : BasePageModel<AddOfferingModel>
{
    [BindProperty]
    public OfferingViewModel Offering { get; set; } = new();
    [BindProperty]
    public string? RemoveSubDetailId { get; set; }
    [BindProperty]
    public string? AsyncAction { get; set; }
    [BindProperty]
    public string? AddPreSelectedUnitUnitId { get; set; }
    [BindProperty]
    public string? AddUnitOfferedUnitId { get; set; }
    public LeadTabNavigationPartial LeadTabNavigation { get; set; } = new();
    public async Task<IActionResult> OnGet(string leadId)
    {
        LeadTabNavigation = Mapper.Map<LeadTabNavigationPartial>(await Mediatr.Send(new GetTabNavigationByLeadIdQuery(leadId, Constants.TabNavigation.Offerings)));
        Offering = OfferingHelper.AutocalculateYearMonthDay(Offering);
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        LeadTabNavigation = Mapper.Map<LeadTabNavigationPartial>(await Mediatr.Send(new GetTabNavigationByLeadIdQuery(Offering.LeadID!, Constants.TabNavigation.Offerings)));
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<AddOfferingCommand>(Offering)), "EditOffering", true);
    }
    public async Task<IActionResult> OnPostChangeFormValue()
    {
        ModelState.Clear();
        if (AsyncAction == "RemovePreSelectedUnit")
        {          
            return RemovePreSelectedUnit();
        }
        if (AsyncAction == "AddPreSelectedUnit")
        {           
            return await AddPreSelectedUnit();
        }
        if (AsyncAction == "ChangeProject")
        {
            return ChangeProject();
        }
        if (AsyncAction == "AddUnitOffered")
        {
            return await AddUnitOffered();
        }
        if (AsyncAction == "RemoveUnitOffered")
        {
            return await RemoveUnitOffered();
        }
        if (AsyncAction == "Action_AutocalculateTerminationTurnOverDate")
        {
            Offering = OfferingHelper.AutocalculateTermination(Offering);
            Offering = OfferingHelper.AutocalculateTurnOverDate(Offering);
            Offering = OfferingHelper.AutoCalculateAnnualIncrementAndCAMCArea(Offering);
        }
        if (AsyncAction == "Action_AutocalculateYearMonthDay")
        {
            Offering = OfferingHelper.AutocalculateYearMonthDay(Offering);
            Offering = OfferingHelper.AutoCalculateAnnualIncrementAndCAMCArea(Offering);
        }
        if (AsyncAction == "Action_AutocalculateFitOut")
        {
            Offering = OfferingHelper.AutocalculateFitOut(Offering);
        }
        if (AsyncAction == "Action_AutocalculateTurnOverDate")
        {
            Offering = OfferingHelper.AutocalculateTurnOverDate(Offering);
        }
        if (AsyncAction == "Action_AutoCalculateAnnualIncrement")
        {
            Offering = OfferingHelper.AutoCalculateAllRates(Offering);
        }
        if (AsyncAction == "Action_AutoCalculateTotalSecurityDeposit")
        {
            Offering = OfferingHelper.AutoCalculateTotalSecurityDeposit(Offering);
        }
        if (AsyncAction == "Action_AutoCalculateTotalConstructionBond")
        {
            Offering = OfferingHelper.AutoCalculateTotalConstructionBond(Offering);
        }
        if (AsyncAction == "Action_AutoCalculateCAMC")
        {
            Offering = OfferingHelper.AutoCalculateCAMC(Offering);
        }
        if (AsyncAction == "Action_AutoCalculateAnnualAdsFee")
        {
            Offering = OfferingHelper.AutoCalculateAnnualAdsFee(Offering);
        }
        return Partial("_OfferingInputFieldsPartial", Offering);
    }
    private IActionResult RemovePreSelectedUnit()
    {
        ModelState.Clear();
        Offering.PreSelectedUnitList = Offering!.PreSelectedUnitList!.Where(l => l.Id != RemoveSubDetailId).ToList();
        Offering = OfferingHelper.AutoCalculateAllRates(Offering);
        return Partial("_OfferingInputFieldsPartial", Offering);
    }
    private async Task<IActionResult> AddPreSelectedUnit()
    {
        ModelState.Clear();
        PreSelectedUnitViewModel preSelectedUnitToAdd = new();
        _ = (await Mediatr.Send(new GetUnitByIdQuery(AddPreSelectedUnitUnitId!))).Select(l => preSelectedUnitToAdd = Mapper.Map<PreSelectedUnitViewModel>(l));
        preSelectedUnitToAdd.OfferingID = Offering.Id;
        if (Offering!.PreSelectedUnitList == null) { Offering!.PreSelectedUnitList = new List<PreSelectedUnitViewModel>(); }
        Offering!.PreSelectedUnitList!.Add(preSelectedUnitToAdd);
        Offering = OfferingHelper.AutoCalculateAllRates(Offering);
        return Partial("_OfferingInputFieldsPartial", Offering);
    }
    private IActionResult ChangeProject()
    {
        ModelState.Clear();
        Offering.UnitOfferedList = new List<UnitOfferedViewModel>();
        Offering.PreSelectedUnitList = new List<PreSelectedUnitViewModel>();
        Offering = OfferingHelper.AutoCalculateAllRates(Offering);
        return Partial("_OfferingInputFieldsPartial", Offering);
    }
    private async Task<IActionResult> AddUnitOffered()
    {
        ModelState.Clear();
        UnitOfferedViewModel unitOfferedToAdd = new();
        _ = (await Mediatr.Send(new GetUnitByIdQuery(AddUnitOfferedUnitId!))).Select(l => unitOfferedToAdd = Mapper.Map<UnitOfferedViewModel>(l));
        unitOfferedToAdd.OfferingID = Offering.Id;
        if (Offering!.UnitOfferedList == null) { Offering!.UnitOfferedList = new List<UnitOfferedViewModel>(); }
        Offering!.UnitOfferedList!.Add(unitOfferedToAdd);
        Offering.PreSelectedUnitList = Offering!.PreSelectedUnitList!.Where(l => l.UnitID != AddUnitOfferedUnitId).ToList();
        Offering = OfferingHelper.AutoCalculateAllRates(Offering);
        return Partial("_OfferingInputFieldsPartial", Offering);
    }
    private async Task<IActionResult> RemoveUnitOffered()
    {
        AddPreSelectedUnitUnitId = RemoveSubDetailId;
        ModelState.Clear();
        await AddPreSelectedUnit();
        Offering.UnitOfferedList = Offering!.UnitOfferedList!.Where(l => l.UnitID != RemoveSubDetailId).ToList();
        Offering = OfferingHelper.AutoCalculateAllRates(Offering);
        return Partial("_OfferingInputFieldsPartial", Offering);
    } 
}
