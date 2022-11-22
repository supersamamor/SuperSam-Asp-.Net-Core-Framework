using AutoMapper;
using CTI.ELMS.Application.Features.ELMS.Unit.Queries;
using CTI.ELMS.Web.Areas.ELMS.Models;
using CTI.ELMS.Web.Helper;
using MediatR;

namespace CTI.ELMS.Web.Service
{
    public class OfferingServices
    {
        private readonly IMediator _mediatr;
        private readonly IMapper _mapper;
        public OfferingServices(IMediator mediatr, IMapper mapper)
        {
            _mediatr = mediatr;
            _mapper = mapper;
        }
        public static OfferingViewModel AutoCalculateAllRates(OfferingViewModel offering)
        {
            if (offering.UnitOfferedList != null)
            {
                foreach (var item in offering.UnitOfferedList)
                {
                    SetAnnualIncrement(item);
                }
            }
            offering = AutoCalculateAnnualIncrementAndCAMCArea(offering);
            offering = AutoCalculateTotalBasicFixedMonthlyRent(offering);
            offering = AutoCalculateTotalSecurityDeposit(offering);
            offering = AutoCalculateTotalConstructionBond(offering);
            offering = AutoCalculateCAMC(offering);
            return AutoCalculateAnnualAdsFee(offering);
        }
        public static OfferingViewModel AutocalculateTermination(OfferingViewModel offering)
        {
            var day = offering.Day <= 0 || offering.Day == null ? 0 : (int)offering.Day - 1;
            offering.TerminationDate = ((DateTime)offering.CommencementDate!).AddYears((int)offering.Year!).AddMonths((int)offering.Month!).AddDays(day);
            return offering;
        }
        public static OfferingViewModel AutocalculateYearMonthDay(OfferingViewModel offering)
        {
            var dateSpan = DateHelper.AutocalculateYearMonthDayFromStartAndEndDate((DateTime)offering.CommencementDate!, (DateTime)offering.TerminationDate!);
            offering.Year = dateSpan.Years;
            offering.Month = dateSpan.Months;
            offering.Day = dateSpan.Days;
            return offering;
        }
        public static OfferingViewModel AutocalculateFitOut(OfferingViewModel offering)
        {
            if (offering.TurnOverDate != null)
            {
                offering.FitOutPeriod = (int)((DateTime)offering.CommencementDate!).Subtract((DateTime)offering.TurnOverDate).TotalDays;
            }
            return offering;
        }
        public static OfferingViewModel AutocalculateTurnOverDate(OfferingViewModel offering)
        {
            offering.TurnOverDate = ((DateTime)offering.CommencementDate!).AddDays((int)-offering.FitOutPeriod!);
            return offering;
        }

        public static OfferingViewModel AutoCalculateAnnualIncrementAndCAMCArea(OfferingViewModel offering)
        {
            var newYear = offering.Year == 0 ? 1 : offering.Year;
            var currentYear = 1;
            if (offering.UnitOfferedList != null)
            {
                //Loop All Unit Offered
                decimal totalCAMCArea = 0;
                foreach (var item in offering.UnitOfferedList)
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
                                Year = addYear,
                                BasicFixedMonthlyRent = item.BasicFixedMonthlyRent,
                                PercentageRent = item.PercentageRent,
                                MinimumMonthlyRent = item.MinimumMonthlyRent,
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
                offering.CAMCConstructionTotalUnitArea = totalCAMCArea;
            }
            return offering;
        }
        public static OfferingViewModel AutoCalculateTotalSecurityDeposit(OfferingViewModel offering)
        {
            if (offering.AutoComputeTotalSecurityDeposit == true)
            {
                offering.TotalSecurityDeposit = offering.TotalBasicFixedMonthlyRent * offering.SecMonths;
            }
            return offering;
        }
        public static OfferingViewModel AutoCalculateTotalConstructionBond(OfferingViewModel offering)
        {
            if (offering.AutoComputeTotalConstructionBond == true)
            {
                offering.TotalConstructionBond = offering.TotalBasicFixedMonthlyRent * offering.ConstructionMonths;
            }
            return offering;
        }
        public static OfferingViewModel AutoCalculateCAMC(OfferingViewModel offering)
        {
            var constructionCAMC = offering.CAMCConstructionTotalUnitArea * offering.ConstructionCAMCRate * offering.ConstructionCAMCMonths;
            offering.ConstructionCAMC = constructionCAMC + (constructionCAMC * WebConstants.VATRate);
            return offering;
        }

        public static OfferingViewModel AutoCalculateAnnualAdsFee(OfferingViewModel offering)
        {
            if (offering.SignedOfferSheetDate == null)
            {
                if (offering.AutoComputeAnnualAdvertisingFee == true)
                {
                    decimal autoAnnualAdFee;
                    if (offering.CAMCConstructionTotalUnitArea >= 0 && offering.CAMCConstructionTotalUnitArea < 51)
                    {
                        autoAnnualAdFee = 10000.00M;
                    }
                    else if (offering.CAMCConstructionTotalUnitArea >= 51 && offering.CAMCConstructionTotalUnitArea < 101)
                    {
                        autoAnnualAdFee = 20000.00M;
                    }
                    else if (offering.CAMCConstructionTotalUnitArea >= 101 && offering.CAMCConstructionTotalUnitArea < 251)
                    {
                        autoAnnualAdFee = 25000.00M;
                    }
                    else if (offering.CAMCConstructionTotalUnitArea >= 251)
                    {
                        autoAnnualAdFee = 30000.00M;
                    }
                    else
                    {
                        autoAnnualAdFee = 0;
                    }
                    offering.AnnualAdvertisingFee = autoAnnualAdFee;
                }
                else
                {
                    offering.AnnualAdvertisingFee = 0;
                }
            }
            return offering;
        }
        public static OfferingViewModel AutoCalculateTotalBasicFixedMonthlyRent(OfferingViewModel offering)
        {
            decimal totalBasicFixedMonthlyRent = 0;
            if (offering.UnitOfferedList != null)
            {
                foreach (var item in offering.UnitOfferedList)
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
            offering.TotalBasicFixedMonthlyRent = totalBasicFixedMonthlyRent;
            return offering;
        }
        public static OfferingViewModel RemovePreSelectedUnit(OfferingViewModel offering, string? removeSubDetailId)
        {

            offering.PreSelectedUnitList = offering!.PreSelectedUnitList!.Where(l => l.Id != removeSubDetailId).ToList();
            offering = AutoCalculateAllRates(offering);
            return offering;
        }
        public async Task<OfferingViewModel> AddPreSelectedUnit(OfferingViewModel offering, string? AddPreSelectedUnitUnitId)
        {
            PreSelectedUnitViewModel preSelectedUnitToAdd = new();
            _ = (await _mediatr.Send(new GetUnitByIdQuery(AddPreSelectedUnitUnitId!))).Select(l => preSelectedUnitToAdd = _mapper.Map<PreSelectedUnitViewModel>(l));
            preSelectedUnitToAdd.OfferingID = offering.Id;
            if (offering!.PreSelectedUnitList == null) { offering!.PreSelectedUnitList = new List<PreSelectedUnitViewModel>(); }
            offering!.PreSelectedUnitList!.Add(preSelectedUnitToAdd);
            offering = AutoCalculateAllRates(offering);
            return offering;
        }
        public static OfferingViewModel ChangeProject(OfferingViewModel offering)
        {
            offering.UnitOfferedList = new List<UnitOfferedViewModel>();
            offering.PreSelectedUnitList = new List<PreSelectedUnitViewModel>();
            offering = AutoCalculateAllRates(offering);
            return offering;
        }
        public async Task<OfferingViewModel> AddUnitOffered(OfferingViewModel offering, string? addUnitOfferedUnitId)
        {
            var preSelectedUnit = offering!.PreSelectedUnitList!.Where(l => l.UnitID != addUnitOfferedUnitId).FirstOrDefault();
            IList<UnitOfferedViewModel> unitOfferedToAddList = new List<UnitOfferedViewModel>();
            var query = new GetAvailableUnitQuery
            {
                UnitId = addUnitOfferedUnitId,
                CommencementDate = offering.CommencementDate
            };
            unitOfferedToAddList = _mapper.Map<IList<UnitOfferedViewModel>>(await _mediatr.Send(query));         
            unitOfferedToAddList.Single().OfferingID = offering.Id;   
            if (offering!.UnitOfferedList == null) { offering!.UnitOfferedList = new List<UnitOfferedViewModel>(); }
            offering!.UnitOfferedList!.Add(unitOfferedToAddList.Single());
            offering.PreSelectedUnitList = offering!.PreSelectedUnitList!.Where(l => l.UnitID != addUnitOfferedUnitId).ToList();
            offering = AutoCalculateAllRates(offering);
            return offering;
        }
        public async Task<OfferingViewModel> RemoveUnitOffered(OfferingViewModel offering, string? removeSubDetailId)
        {
            await AddPreSelectedUnit(offering, removeSubDetailId);
            offering.UnitOfferedList = offering!.UnitOfferedList!.Where(l => l.UnitID != removeSubDetailId).ToList();
            offering = AutoCalculateAllRates(offering);
            return offering;
        }
        public async Task<OfferingViewModel> RecalculateLotBudget(OfferingViewModel offering)
        {
            if (offering.UnitOfferedList != null)
            {
                foreach (var item in offering.UnitOfferedList)
                {
                   
                    var query = new GetAvailableUnitQuery
                    {
                        UnitId = item.UnitID,
                        CommencementDate = offering.CommencementDate
                    };
                    item.LotBudget = (await _mediatr.Send(query)).FirstOrDefault()!.LotBudget;
                }
            }
            return offering;
        }
        private static UnitOfferedViewModel SetAnnualIncrement(UnitOfferedViewModel unitOffered)
        {
            if (unitOffered.HasAnnualIncrement == true)
            {
                if (unitOffered.AnnualIncrement == null || unitOffered.AnnualIncrement == 0)
                {
                    unitOffered.AnnualIncrement = WebConstants.DefaultAnnualIncrement;
                }
            }
            else
            {
                unitOffered.AnnualIncrement = 0;
            }
            return unitOffered;
        }
    }
}
