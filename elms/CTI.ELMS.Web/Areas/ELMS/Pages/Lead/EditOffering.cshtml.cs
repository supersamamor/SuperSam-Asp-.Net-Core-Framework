using CTI.ELMS.Application.Features.ELMS.Offering.Commands;
using CTI.ELMS.Application.Features.ELMS.Offering.Queries;
using CTI.ELMS.Application.Features.ELMS.TabNavigation.Queries;
using CTI.ELMS.Web.Areas.ELMS.Models;
using CTI.ELMS.Web.Models;
using CTI.ELMS.Web.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CTI.ELMS.Web.Areas.ELMS.Pages.Lead;

[Authorize(Policy = Permission.Offering.Edit)]
public class EditOfferingModel : BasePageModel<EditOfferingModel>
{
    private readonly OfferingServices _offeringServices;
    public EditOfferingModel(OfferingServices offeringServices)
    {
        _offeringServices = offeringServices;
    }
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
    public async Task<IActionResult> OnGet(string id)
    {
        LeadTabNavigation = Mapper.Map<LeadTabNavigationPartial>(await Mediatr.Send(new GetTabNavigationByOfferingIdQuery(id, Constants.TabNavigation.Offerings)));      
        return await PageFrom(async () => await Mediatr.Send(new GetOfferingByIdQuery(id)), Offering);
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        LeadTabNavigation = Mapper.Map<LeadTabNavigationPartial>(await Mediatr.Send(new GetTabNavigationByLeadIdQuery(Offering.LeadID!, Constants.TabNavigation.Offerings)));
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<EditOfferingCommand>(Offering)), "EditOffering", true);
    }
    public async Task<IActionResult> OnPostChangeFormValue()
    {
        ModelState.Clear();
        if (AsyncAction == "RemovePreSelectedUnit")
        {
            Offering = OfferingServices.RemovePreSelectedUnit(Offering, RemoveSubDetailId);
        }
        if (AsyncAction == "AddPreSelectedUnit")
        {
            Offering = await _offeringServices.AddPreSelectedUnit(Offering, AddPreSelectedUnitUnitId);
        }
        if (AsyncAction == "ChangeProject")
        {
            Offering = OfferingServices.ChangeProject(Offering);
            Offering = OfferingServices.AutoCalculateAllRates(Offering);
        }
        if (AsyncAction == "AddUnitOffered")
        {
            Offering = await _offeringServices.AddUnitOffered(Offering, AddUnitOfferedUnitId);
        }
        if (AsyncAction == "RemoveUnitOffered")
        {
            Offering = await _offeringServices.RemoveUnitOffered(Offering, RemoveSubDetailId);
        }
        if (AsyncAction == "Action_AutocalculateTerminationTurnOverDate")
        {
            Offering = await _offeringServices.RecalculateLotBudget(Offering);
            Offering = OfferingServices.AutocalculateTermination(Offering);
            Offering = OfferingServices.AutocalculateTurnOverDate(Offering);
            Offering = OfferingServices.AutoCalculateAnnualIncrementAndCAMCArea(Offering);
        }
        if (AsyncAction == "Action_AutocalculateYearMonthDay")
        {
            Offering = OfferingServices.AutocalculateYearMonthDay(Offering);
            Offering = OfferingServices.AutoCalculateAnnualIncrementAndCAMCArea(Offering);
        }
        if (AsyncAction == "Action_AutocalculateFitOut")
        {
            Offering = OfferingServices.AutocalculateFitOut(Offering);
        }
        if (AsyncAction == "Action_AutocalculateTurnOverDate")
        {
            Offering = OfferingServices.AutocalculateTurnOverDate(Offering);
        }
        if (AsyncAction == "Action_AutoCalculateAnnualIncrement")
        {
            Offering = OfferingServices.AutoCalculateAllRates(Offering);
        }
        if (AsyncAction == "Action_AutoCalculateTotalSecurityDeposit")
        {
            Offering = OfferingServices.AutoCalculateTotalSecurityDeposit(Offering);
        }
        if (AsyncAction == "Action_AutoCalculateTotalConstructionBond")
        {
            Offering = OfferingServices.AutoCalculateTotalConstructionBond(Offering);
        }
        if (AsyncAction == "Action_AutoCalculateCAMC")
        {
            Offering = OfferingServices.AutoCalculateCAMC(Offering);
        }
        if (AsyncAction == "Action_AutoCalculateAnnualAdsFee")
        {
            Offering = OfferingServices.AutoCalculateAnnualAdsFee(Offering);
        }
        return Partial("_OfferingInputFieldsPartial", Offering);
    }
}
