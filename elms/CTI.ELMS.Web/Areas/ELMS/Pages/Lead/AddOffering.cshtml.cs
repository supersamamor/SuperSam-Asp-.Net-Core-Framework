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
        if (AsyncAction == "AutocalculateYearMonthDayFromStartAndEndDate")
        {
            //return AutocalculateYearMonthDayFromStartAndEndDate();
        }
        return Partial("_OfferingInputFieldsPartial", Offering);
    }
    private IActionResult RemovePreSelectedUnit()
    {
        ModelState.Clear();

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
        return Partial("_OfferingInputFieldsPartial", Offering);
    }
    private IActionResult ChangeProject()
    {
        ModelState.Clear();
        Offering.UnitOfferedList = new List<UnitOfferedViewModel>();
        Offering.PreSelectedUnitList = new List<PreSelectedUnitViewModel>();
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
        return Partial("_OfferingInputFieldsPartial", Offering);
    }
    private async Task<IActionResult> RemoveUnitOffered()
    {
        AddPreSelectedUnitUnitId = RemoveSubDetailId;
        ModelState.Clear();
        await AddPreSelectedUnit();
        Offering.UnitOfferedList = Offering!.UnitOfferedList!.Where(l => l.UnitID != RemoveSubDetailId).ToList();
        return Partial("_OfferingInputFieldsPartial", Offering);
    }
    public IActionResult Test()
    {
        AutocalculateTerminationDateFromYearMonthDate();
        AutocalculateFitOut();
        AutocalculateTurnOverDate();
        AutoCalculateAnnualIncrementAndCAMCArea();
        AutoCalculateTotalSecurityDeposit();
        AutoCalculateTotalConstructionBond();
        AutoCalculateCAMC();
        AutoCalculateAnnualAdsFee();
        AutoCalculateTotalBasicFixedMonthlyRent();
        return Partial("_OfferingInputFieldsPartial", Offering);
    }
    private void AutocalculateTerminationDateFromYearMonthDate()
    {
        var dateSpan = DateHelper.AutocalculateYearMonthDayFromStartAndEndDate((DateTime)Offering.CommencementDate!, (DateTime)Offering.TerminationDate!);
        Offering.Year = dateSpan.Years;
        Offering.Month = dateSpan.Months;
        Offering.Day = dateSpan.Days;
    }
    private void AutocalculateFitOut()
    {
        if (Offering.TurnOverDate != null)
        {
            Offering.FitOutPeriod = (int)((DateTime)Offering.CommencementDate!).Subtract((DateTime)Offering.TurnOverDate).TotalDays;
        }
    }
    private void AutocalculateTurnOverDate()
    {
        Offering.TurnOverDate = ((DateTime)Offering.CommencementDate!).AddDays((int)-Offering.FitOutPeriod!);
    }

    private void AutoCalculateAnnualIncrementAndCAMCArea()
    {
        var newYear = Offering.Year == 0 ? 1 : Offering.Year;
        var currentYear = 1;
        if (Offering.UnitOfferedList != null)
        {
            //Loop All Unit Offered
            decimal totalCAMCArea = 0;
            foreach (var item in Offering.UnitOfferedList)
            {
                currentYear = item.AnnualIncrementList == null ? 1 : item.AnnualIncrementList.Count + 1;
                totalCAMCArea += (item.LotArea == null ? 0 : (decimal)item.LotArea);
                if (item.AnnualIncrementList == null) { item.AnnualIncrementList = new List<AnnualIncrementViewModel>(); }
                //Remove excess annual increment
                if (currentYear > newYear)
                {
                    for (var removeYear = currentYear; removeYear > newYear; removeYear--)
                    {
                        var annualIncToRemove = item.AnnualIncrementList.Where(l => l.Year == removeYear).FirstOrDefault();
                        item.AnnualIncrementList.Remove(annualIncToRemove!);
                        if (removeYear == 1) { break; }
                    }
                }
                //Add annual increment
                else if (newYear > currentYear)
                {
                    for (var addYear = currentYear + 1; addYear <= newYear; addYear++)
                    {
                        var annualInctToAdd = new AnnualIncrementViewModel
                        {
                            Year = addYear
                        };
                        item.AnnualIncrementList.Add(annualInctToAdd);
                    }
                }
                //Calculate based on parameters (Annual Increment)
                int ctr = 0;
                decimal prevBasicFixedMonthlyRent = 0;
                decimal prevMinimumMonthlyRent = 0;
                foreach (var annualIncrementItem in item.AnnualIncrementList)
                {
                    if (item.AnnualIncrement > 0)
                    {
                        //If 2nd Year of the Annual Increment (First data from the list) use Value of the UnitOffered
                        if (ctr == 0)
                        {
                            annualIncrementItem.BasicFixedMonthlyRent = item.BasicFixedMonthlyRent == 0 ? 0 : item.BasicFixedMonthlyRent + (item.BasicFixedMonthlyRent * (item.AnnualIncrement / 100));
                            annualIncrementItem.MinimumMonthlyRent = item.MinimumMonthlyRent == 0 ? 0 : item.MinimumMonthlyRent + (item.MinimumMonthlyRent * (item.AnnualIncrement / 100));
                        }
                        //If not first record, use value from previous record
                        else
                        {
                            annualIncrementItem.BasicFixedMonthlyRent = prevBasicFixedMonthlyRent == 0 ? 0 : prevBasicFixedMonthlyRent + (prevBasicFixedMonthlyRent * (item.AnnualIncrement / 100));
                            annualIncrementItem.MinimumMonthlyRent = prevMinimumMonthlyRent == 0 ? 0 : prevMinimumMonthlyRent + (prevMinimumMonthlyRent * (item.AnnualIncrement / 100));
                        }
                    }
                    //Set Value of previous rates for the next year's incremental
                    prevBasicFixedMonthlyRent = (annualIncrementItem.BasicFixedMonthlyRent == null ? 0 : (decimal)annualIncrementItem.BasicFixedMonthlyRent);
                    prevMinimumMonthlyRent = (annualIncrementItem.MinimumMonthlyRent == null ? 0 : (decimal)annualIncrementItem.MinimumMonthlyRent);
                    annualIncrementItem.PercentageRent = item.PercentageRent;
                    ctr++;
                }
            }
            Offering.CAMCConstructionTotalUnitArea = totalCAMCArea;
        }
    }
    private void AutoCalculateTotalSecurityDeposit()
    {
        if (Offering.AutoComputeTotalSecurityDeposit == true)
        {
            Offering.TotalSecurityDeposit = Offering.TotalBasicFixedMonthlyRent * Offering.SecMonths;
        }
    }
    private void AutoCalculateTotalConstructionBond()
    {
        if (Offering.AutoComputeTotalConstructionBond == true)
        {
            Offering.TotalConstructionBond = Offering.TotalBasicFixedMonthlyRent * Offering.ConstructionMonths;
        }
    }  
    private void AutoCalculateCAMC()
    {
        var constructionCAMC = Offering.CAMCConstructionTotalUnitArea * Offering.ConstructionCAMCRate * Offering.ConstructionCAMCMonths;
        Offering.ConstructionCAMC = constructionCAMC + (constructionCAMC * WebConstants.VATRate);
    }

    private void AutoCalculateAnnualAdsFee()
    {
        if (Offering.SignedOfferSheetDate == null)
        {
            if (Offering.AutoComputeAnnualAdvertisingFee == true)
            {
                decimal autoAnnualAdFee;
                if (Offering.CAMCConstructionTotalUnitArea >= 0 && Offering.CAMCConstructionTotalUnitArea < 51)
                {
                    autoAnnualAdFee = 10000.00M;
                }
                else if (Offering.CAMCConstructionTotalUnitArea >= 51 && Offering.CAMCConstructionTotalUnitArea < 101)
                {
                    autoAnnualAdFee = 20000.00M;
                }
                else if (Offering.CAMCConstructionTotalUnitArea >= 101 && Offering.CAMCConstructionTotalUnitArea < 251)
                {
                    autoAnnualAdFee = 25000.00M;
                }
                else if (Offering.CAMCConstructionTotalUnitArea >= 251)
                {
                    autoAnnualAdFee = 30000.00M;
                }
                else
                {
                    autoAnnualAdFee = 0;
                }
                Offering.AnnualAdvertisingFee = autoAnnualAdFee;
            }
            else
            {
                Offering.AnnualAdvertisingFee = 0;
            }
        }
    }
    private void AutoCalculateTotalBasicFixedMonthlyRent()
    {
        decimal totalBasicFixedMonthlyRent = 0;
        if (Offering.UnitOfferedList != null)
        {
            foreach (var item in Offering.UnitOfferedList)
            {
                var basicMonthlyRent = item.BasicFixedMonthlyRent;
                var minimumMonthlyRent = item.MinimumMonthlyRent;
                var lotArea = item.LotArea;
                decimal rentalRate;
                if (item.BasicFixedMonthlyRent > 0)
                {
                    rentalRate = (basicMonthlyRent == null ? 0 : (decimal)basicMonthlyRent);
                }
                else
                {
                    rentalRate = (minimumMonthlyRent == null ? 0 : (decimal)minimumMonthlyRent);
                }

                if (item.IsFixedMonthlyRent == true)
                {
                    totalBasicFixedMonthlyRent += rentalRate;
                }
                else
                {
                    totalBasicFixedMonthlyRent += ((lotArea == null ? 0 : (decimal)lotArea) * rentalRate);
                }
            }
        }
        Offering.TotalBasicFixedMonthlyRent = totalBasicFixedMonthlyRent;
    }
}
