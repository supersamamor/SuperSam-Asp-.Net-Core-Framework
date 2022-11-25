using AutoMapper;
using CTI.Common.Identity.Abstractions;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Text;

namespace CTI.ELMS.Application.Repositories
{
    public class OfferingRepository
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IAuthenticatedUser _authenticatedUser;
        public OfferingRepository(ApplicationContext context, IMapper mapper, IConfiguration config,
                                    IAuthenticatedUser authenticatedUser)
        {
            _context = context;
            _mapper = mapper;
            _config = config;
            _authenticatedUser = authenticatedUser;
        }
        public async Task<string> AddOfferingHistory(OfferingState entity)
        {
            var offeringVersion = await _context.OfferingHistory.Where(l => l.OfferingID == entity.Id).AsNoTracking().MaxAsync(l => l.OfferingVersion);
            var offeringHistory = _mapper.Map<OfferingHistoryState>(entity);
            offeringHistory.SetOfferingVersion(offeringVersion == null ? 1 : (int)offeringVersion + 1);
            entity.SetOfferingHistoryId(offeringHistory.Id);
            _context.Entry(offeringHistory!).State = EntityState.Added;
            return offeringHistory.Id;
        }
        public async Task UpdateOfferingHistory(OfferingState entity)
        {
            var offeringHistory = await _context.OfferingHistory.Where(l => l.Id == entity.OfferingHistoryID).AsNoTracking().FirstOrDefaultAsync();
            _mapper.Map(entity, offeringHistory);
            _context.Entry(offeringHistory!).State = EntityState.Modified;
        }
        public void AddPreSelectedUnitList(OfferingState entity)
        {
            if (entity.PreSelectedUnitList?.Count > 0)
            {
                foreach (var preSelectedUnit in entity.PreSelectedUnitList!)
                {
                    preSelectedUnit.Id = Guid.NewGuid().ToString();
                    _context.Entry(preSelectedUnit).State = EntityState.Added;
                }
            }
        }
        public void AddUnitOfferedList(OfferingState entity, string offeringHistoryId, bool disableHistory = false)
        {
            if (entity.UnitOfferedList?.Count > 0)
            {
                foreach (var unitOffered in entity.UnitOfferedList!)
                {
                    unitOffered.Id = Guid.NewGuid().ToString();
                    unitOffered.OfferingID = entity.Id;
                    AutoCalculateUnitOfferedAnnualIncrementInformation(unitOffered);
                    _context.Entry(unitOffered).State = EntityState.Added;
                    string unitOfferedHistoryId = "";
                    if (disableHistory == false)
                    {
                        unitOfferedHistoryId = AddUnitOfferedHistory(unitOffered, offeringHistoryId);
                    }
                    if (unitOffered.AnnualIncrementList?.Count > 0)
                    {
                        foreach (var annualIncrement in unitOffered.AnnualIncrementList!)
                        {
                            annualIncrement.UnitOfferedID = unitOffered.Id;
                            annualIncrement.Id = Guid.NewGuid().ToString();
                            _context.Entry(annualIncrement).State = EntityState.Added;
                            if (disableHistory == false)
                            {
                                AddAnnualIncrementHistory(annualIncrement, unitOfferedHistoryId);
                            }
                        }
                    }
                }
            }
        }
        public async Task<string> GenerateOfferSheetIdAsync()
        {
            string offerSheetNo = "";
            using (var sqlConnection = new SqlConnection(_config.GetConnectionString("ApplicationContext")))
            {
                using var cmd = new SqlCommand()
                {
                    CommandText = $"SELECT RIGHT('00000' + CAST(NEXT VALUE FOR {Infrastructure.Constants.OfferSheetNoSequence} AS VARCHAR), 5)",
                    CommandType = CommandType.Text,
                    Connection = sqlConnection
                };
                sqlConnection.Open();
                using (var reader = await cmd.ExecuteReaderAsync()) { if (await reader.ReadAsync()) { offerSheetNo = reader[0].ToString()!; } }
                sqlConnection.Close();
            }
            return string.IsNullOrEmpty(offerSheetNo) ? string.Empty : offerSheetNo;
        }
        public async Task UpdateLeadLatestUpdateDate(string leadID)
        {
            var leadToUpdate = await _context.Lead.FindAsync(leadID);
            leadToUpdate!.SetLatestUpdatedInformation(_authenticatedUser.UserId!);
        }
        public async Task<int> GetOfferSheetPerProjectCounter(string projectId)
        {
            var projectOffers = await _context.Project.Include(l => l.OfferingList).Where(l => l.Id == projectId).AsNoTracking().FirstOrDefaultAsync();
            if (projectOffers?.OfferingList != null && projectOffers.OfferingList.Count > 0)
            {
                return projectOffers!.OfferingList!.Count + 1;
            }
            else
            {
                return 1;
            }
        }
        public async Task RemovePreSelectedUnitList(string offeringId)
        {
            _context.RemoveRange(await _context.PreSelectedUnit.Where(l => l.OfferingID == offeringId).ToListAsync());
        }
        public async Task RemoveUnitOfferedList(string offeringId)
        {
            _context.RemoveRange(await _context.UnitOffered.Where(l => l.OfferingID == offeringId).SelectMany(l => l.AnnualIncrementList!).ToListAsync());
            _context.RemoveRange(await _context.UnitOffered.Where(l => l.OfferingID == offeringId).ToListAsync());
        }
        public async Task AutoCalculateOfferSheetFields(OfferingState offering)
        {
            StringBuilder sbUnitInfo = new();
            decimal unitAreaTotal = 0;
            bool isPos = false;
            string location = "N/A";
            string unitType = "N/A";
            decimal totalBasicFixedMonthlyRent = 0;
            decimal totalMinimumMonthlyRent = 0;
            decimal totalLotBudget = 0;
            decimal totalPercentageRent = 0;
            if (offering.UnitOfferedList != null)
            {
                int counter = 1;
                foreach (var item in offering.UnitOfferedList)
                {
                    var unit = await _context.Unit.Where(l => l.Id == item.UnitID).AsNoTracking().FirstOrDefaultAsync();
                    sbUnitInfo.Append(unit!.UnitNo + ", ");
                    unitAreaTotal += unit.LotArea;
                    if (item.PercentageRent > 0)
                    {
                        isPos = true;
                    }
                    if (counter == 1)
                    {
                        location = unit.Location!;
                        unitType = unit.UnitType!;
                    }
                    totalBasicFixedMonthlyRent += ((item.BasicFixedMonthlyRent == null ? 0 : (decimal)item.BasicFixedMonthlyRent) * unit.LotArea);
                    totalMinimumMonthlyRent += ((item.MinimumMonthlyRent == null ? 0 : (decimal)item.MinimumMonthlyRent) * unit.LotArea);
                    totalLotBudget += ((item.LotBudget == null ? 0 : (decimal)item.LotBudget) * unit.LotArea);
                    totalPercentageRent += ((item.PercentageRent == null ? 0 : (decimal)item.PercentageRent) * unit.LotArea);
                    counter++;
                }
            }
            string unitInfoString = sbUnitInfo.ToString();
            if (unitInfoString.Length >= 2)
            { offering.UnitsInformation = unitInfoString[0..^2]; }
            offering.TotalUnitArea = unitAreaTotal;
            offering.IsPOS = isPos;
            offering.Location = location;
            offering.UnitType = unitType;
            offering.TotalBasicFixedMonthlyRent = totalBasicFixedMonthlyRent;
            offering.TotalMinimumMonthlyRent = totalMinimumMonthlyRent;
            offering.TotalLotBudget = totalLotBudget;
            offering.TotalPercentageRent = totalPercentageRent;
            string anType = "";
            if (offering.IsPOS == true)
            {
                if (offering.Year >= 2)
                {
                    anType = Core.Constants.ANType.NonFixed;
                }
                else
                {
                    anType = Core.Constants.ANType.NonFixedKiosk;
                }
            }
            else
            {
                if (offering.Year >= 2)
                {
                    anType = Core.Constants.ANType.Fixed;
                }
                else
                {
                    anType = Core.Constants.ANType.FixedKiosk;
                }
            }
            offering.ANType = anType;
            string anTermType = "";
            if (offering.ANType == Core.Constants.ANType.Fixed || offering.ANType == Core.Constants.ANType.NonFixed)
            {
                anTermType = Core.Constants.ANTermType.Inline;
            }
            else
            {
                anTermType = Core.Constants.ANTermType.Kiosk;
            }
            offering.ANTermType = anTermType;
        }
        private string AddUnitOfferedHistory(UnitOfferedState unitOffered, string offeringHistoryId)
        {
            var unitOfferedHistory = _mapper.Map<UnitOfferedHistoryState>(unitOffered);
            unitOfferedHistory.OfferingHistoryID = offeringHistoryId;
            _context.Entry(unitOfferedHistory).State = EntityState.Added;
            return unitOfferedHistory.Id;
        }
        private void AddAnnualIncrementHistory(AnnualIncrementState annualIncrement, string unitOfferedHistoryID)
        {
            var annualIncrementHistory = _mapper.Map<AnnualIncrementHistoryState>(annualIncrement);
            annualIncrementHistory.UnitOfferedHistoryID = unitOfferedHistoryID;
            _context.Entry(annualIncrementHistory).State = EntityState.Added;
        }
        private static void AutoCalculateUnitOfferedAnnualIncrementInformation(UnitOfferedState unitOffered)
        {
            StringBuilder sbannualIncrementInformation = new();
            var annualIncrementInfoStringItem = "";
            if (unitOffered.IsFixedMonthlyRent == false)
            {
                annualIncrementInfoStringItem = "Y1-"
                    + (unitOffered.BasicFixedMonthlyRent != null ? ((decimal)unitOffered.BasicFixedMonthlyRent).ToString("0.##") : "0.00")
                    + "/" + (unitOffered.PercentageRent != null ? ((decimal)unitOffered.PercentageRent).ToString("0.##") : "0.00")
                    + "%/" + (unitOffered.MinimumMonthlyRent != null ? ((decimal)unitOffered.MinimumMonthlyRent).ToString("0.##") : "0.00");
            }
            else
            {
                annualIncrementInfoStringItem = "Y1-" + (unitOffered.BasicFixedMonthlyRent != null ? ((decimal)unitOffered.BasicFixedMonthlyRent).ToString("0.##") : "0.00") + "/month";
            }
            sbannualIncrementInformation.Append(annualIncrementInfoStringItem + "   ");
            foreach (var item in unitOffered.AnnualIncrementList!.OrderBy(l => l.Year).ToList())
            {
                if (unitOffered.IsFixedMonthlyRent == false)
                {
                    annualIncrementInfoStringItem = "Y" + item.Year
                        + "-" + (item.BasicFixedMonthlyRent != null ? ((decimal)item.BasicFixedMonthlyRent).ToString("0.##") : "0.00")
                        + "/" + (item.PercentageRent != null ? ((decimal)item.PercentageRent).ToString("0.##") : "0.00")
                        + "%/" + (item.MinimumMonthlyRent != null ? ((decimal)item.MinimumMonthlyRent).ToString("0.##") : "0.00");
                }
                else
                {
                    annualIncrementInfoStringItem = "Y"
                        + item.Year
                        + "-" + (item.BasicFixedMonthlyRent != null ? ((decimal)item.BasicFixedMonthlyRent).ToString("0.##") : "0.00") + "/month";
                }
                sbannualIncrementInformation.Append(annualIncrementInfoStringItem + "   ");
            }
            string annualIncrementInformationString = sbannualIncrementInformation.ToString();
            if (annualIncrementInformationString.Length >= 2)
            {
                unitOffered.AnnualIncrementInformation = annualIncrementInformationString[0..^3];
            }
        }
    }
}
