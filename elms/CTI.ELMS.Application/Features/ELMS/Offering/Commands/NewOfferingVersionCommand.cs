using AutoMapper;
using CTI.Common.Core.Commands;
using CTI.Common.Data;
using CTI.Common.Utility.Validators;
using CTI.ELMS.Core.ELMS;
using CTI.ELMS.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;
using System.Data;
using CTI.Common.Identity.Abstractions;
using System.Text;

namespace CTI.ELMS.Application.Features.ELMS.Offering.Commands;

public record NewOfferingVersionCommand : OfferingState, IRequest<Validation<Error, OfferingState>>;

public class NewOfferingVersionCommandHandler : BaseCommandHandler<ApplicationContext, OfferingState, NewOfferingVersionCommand>, IRequestHandler<NewOfferingVersionCommand, Validation<Error, OfferingState>>
{
    private readonly IdentityContext _identityContext;
    private readonly IAuthenticatedUser _authenticatedUser;
    public NewOfferingVersionCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<NewOfferingVersionCommand> validator,
                                    IdentityContext identityContext,                          
                                    IAuthenticatedUser authenticatedUser) : base(context, mapper, validator)
    {
        _identityContext = identityContext;
        _authenticatedUser = authenticatedUser;
    }

    public async Task<Validation<Error, OfferingState>> Handle(NewOfferingVersionCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await AddOffering(request, cancellationToken));


    public async Task<Validation<Error, OfferingState>> AddOffering(NewOfferingVersionCommand request, CancellationToken cancellationToken)
    {
        var entity = await Context.Offering.Where(l => l.Id == request.Id).AsNoTracking().SingleAsync(cancellationToken: cancellationToken);
        Mapper.Map(request, entity);
        await RemovePreSelectedUnitList(entity.Id); //exclusive
        await RemoveUnitOfferedList(entity.Id); //exclusive
        //Same On Add Onwards
        //entity.SetOfferSheetId(await GenerateOfferSheetIdAsync());
        AddPreSelectedUnitList(entity);
        //entity.SetOfferSheetPerProjectCounter(await GetOfferSheetPerProjectCounter(entity.ProjectID!));
        await AutoCalculateOfferSheetFields(entity);
        var offeringHistoryId = await AddOfferingHistory(entity);
        AddUnitOfferedList(entity, offeringHistoryId);
        Context.Update(entity);  //Diff    
        await AddApprovers(entity.Id, cancellationToken);
        await UpdateLeadLatestUpdateDate(entity.LeadID!);
        _ = await Context.SaveChangesAsync(cancellationToken);
        return Success<Error, OfferingState>(entity);
    }
    private async Task<string> AddOfferingHistory(OfferingState entity)
    {
        var offeringVersion = await Context.OfferingHistory.Where(l => l.OfferingID == entity.Id).AsNoTracking().MaxAsync(l => l.OfferingVersion);
        var offeringHistory = Mapper.Map<OfferingHistoryState>(entity);
        offeringHistory.SetOfferingVersion(offeringVersion == null ? 1 : (int)offeringVersion + 1);
        entity.SetOfferingHistoryId(offeringHistory.Id);
        Context.Entry(offeringHistory!).State = EntityState.Added;
        return offeringHistory.Id;
    }
    private void AddPreSelectedUnitList(OfferingState entity)
    {
        if (entity.PreSelectedUnitList?.Count > 0)
        {
            foreach (var preSelectedUnit in entity.PreSelectedUnitList!)
            {
                preSelectedUnit.Id = Guid.NewGuid().ToString();
                Context.Entry(preSelectedUnit).State = EntityState.Added;
            }
        }
    }
    private void AddUnitOfferedList(OfferingState entity, string offeringHistoryId)
    {
        if (entity.UnitOfferedList?.Count > 0)
        {
            foreach (var unitOffered in entity.UnitOfferedList!)
            {
                unitOffered.Id = Guid.NewGuid().ToString();
                unitOffered.OfferingID = entity.Id;
                AutoCalculateUnitOfferedAnnualIncrementInformation(unitOffered);
                Context.Entry(unitOffered).State = EntityState.Added;
                var unitOfferedHistoryId = AddUnitOfferedHistory(unitOffered, offeringHistoryId);
                if (unitOffered.AnnualIncrementList?.Count > 0)
                {
                    foreach (var annualIncrement in unitOffered.AnnualIncrementList!)
                    {
                        annualIncrement.UnitOfferedID = unitOffered.Id;
                        annualIncrement.Id = Guid.NewGuid().ToString();
                        Context.Entry(annualIncrement).State = EntityState.Added;
                        AddAnnualIncrementHistory(annualIncrement, unitOfferedHistoryId);
                    }
                }
            }
        }
    }

    private string AddUnitOfferedHistory(UnitOfferedState unitOffered, string offeringHistoryId)
    {
        var unitOfferedHistory = Mapper.Map<UnitOfferedHistoryState>(unitOffered);
        unitOfferedHistory.OfferingHistoryID = offeringHistoryId;
        Context.Entry(unitOfferedHistory).State = EntityState.Added;
        return unitOfferedHistory.Id;
    }
    private void AddAnnualIncrementHistory(AnnualIncrementState annualIncrement, string unitOfferedHistoryID)
    {
        var annualIncrementHistory = Mapper.Map<AnnualIncrementHistoryState>(annualIncrement);
        annualIncrementHistory.UnitOfferedHistoryID = unitOfferedHistoryID;
        Context.Entry(annualIncrementHistory).State = EntityState.Added;
    }
    //private async Task<string> GenerateOfferSheetIdAsync()
    //{
    //    string offerSheetNo = "";
    //    using (var sqlConnection = new SqlConnection(_config.GetConnectionString("ApplicationContext")))
    //    {
    //        using var cmd = new SqlCommand()
    //        {
    //            CommandText = $"SELECT RIGHT('00000' + CAST(NEXT VALUE FOR {Infrastructure.Constants.OfferSheetNoSequence} AS VARCHAR), 5)",
    //            CommandType = CommandType.Text,
    //            Connection = sqlConnection
    //        };
    //        sqlConnection.Open();
    //        using (var reader = await cmd.ExecuteReaderAsync()) { if (await reader.ReadAsync()) { offerSheetNo = reader[0].ToString()!; } }
    //        sqlConnection.Close();
    //    }
    //    return string.IsNullOrEmpty(offerSheetNo) ? string.Empty : offerSheetNo;
    //}
    private async Task UpdateLeadLatestUpdateDate(string leadID)
    {
        var leadToUpdate = await Context.Lead.FindAsync(leadID);
        leadToUpdate!.SetLatestUpdatedInformation(_authenticatedUser.UserId!);
    }
    //private async Task<int> GetOfferSheetPerProjectCounter(string projectId)
    //{
    //    var projectOffers = await Context.Project.Include(l => l.OfferingList).Where(l => l.Id == projectId).AsNoTracking().FirstOrDefaultAsync();
    //    if (projectOffers?.OfferingList != null && projectOffers.OfferingList.Count > 0)
    //    {
    //        return projectOffers!.OfferingList!.Count + 1;
    //    }
    //    else
    //    {
    //        return 1;
    //    }
    //}
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
                var unit = await Context.Unit.Where(l => l.Id == item.UnitID).AsNoTracking().FirstOrDefaultAsync();
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
    private async Task RemovePreSelectedUnitList(string offeringId)
    {
        Context.RemoveRange(await Context.PreSelectedUnit.Where(l => l.OfferingID == offeringId).ToListAsync());
    }
    private async Task RemoveUnitOfferedList(string offeringId)
    {   
        Context.RemoveRange(await Context.UnitOffered.Where(l => l.OfferingID == offeringId).SelectMany(l => l.AnnualIncrementList!).ToListAsync());
        Context.RemoveRange(await Context.UnitOffered.Where(l => l.OfferingID == offeringId).ToListAsync());
    }
    private async Task AddApprovers(string offeringId, CancellationToken cancellationToken)
    {
        var approverList = await Context.ApproverAssignment.Include(l => l.ApproverSetup).Where(l => l.ApproverSetup.TableName == ApprovalModule.Offering).AsNoTracking().ToListAsync(cancellationToken);
        if (approverList.Count > 0)
        {
            var approvalRecord = new ApprovalRecordState()
            {
                ApproverSetupId = approverList.FirstOrDefault()!.ApproverSetupId,
                DataId = offeringId,
                ApprovalList = new List<ApprovalState>()
            };
            foreach (var approverItem in approverList)
            {
                if (approverItem.ApproverType == ApproverTypes.User)
                {
                    var approval = new ApprovalState()
                    {
                        Sequence = approverItem.Sequence,
                        ApproverUserId = approverItem.ApproverUserId!,
                    };
                    if (approverList.FirstOrDefault()!.ApproverSetup.ApprovalType != ApprovalTypes.InSequence)
                    {
                        approval.EmailSendingStatus = SendingStatus.Pending;
                    }
                    approvalRecord.ApprovalList.Add(approval);
                }
                else if (approverItem.ApproverType == ApproverTypes.Role)
                {
                    var userListWithRole = await (from a in _identityContext.Users
                                                  join b in _identityContext.UserRoles on a.Id equals b.UserId
                                                  join c in _identityContext.Roles on b.RoleId equals c.Id
                                                  where c.Id == approverItem.ApproverRoleId
                                                  select a.Id).AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
                    foreach (var userId in userListWithRole)
                    {
                        var approval = new ApprovalState()
                        {
                            Sequence = approverItem.Sequence,
                            ApproverUserId = userId,
                        };
                        if (approverList.FirstOrDefault()!.ApproverSetup.ApprovalType != ApprovalTypes.InSequence)
                        {
                            approval.EmailSendingStatus = SendingStatus.Pending;
                        }
                        approvalRecord.ApprovalList.Add(approval);
                    }
                }
            }
            await Context.AddAsync(approvalRecord, cancellationToken);
        }
    }
}

public class NewOfferingVersionCommandValidator : AbstractValidator<NewOfferingVersionCommand>
{
    readonly ApplicationContext _context;

    public NewOfferingVersionCommandValidator(ApplicationContext context)
    {
        _context = context;
        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<OfferingState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Offering with id {PropertyValue} does not exists");
    }
}
