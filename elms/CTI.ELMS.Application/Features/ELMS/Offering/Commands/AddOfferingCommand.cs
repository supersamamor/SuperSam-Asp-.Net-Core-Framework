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

namespace CTI.ELMS.Application.Features.ELMS.Offering.Commands;

public record AddOfferingCommand : OfferingState, IRequest<Validation<Error, OfferingState>>;

public class AddOfferingCommandHandler : BaseCommandHandler<ApplicationContext, OfferingState, AddOfferingCommand>, IRequestHandler<AddOfferingCommand, Validation<Error, OfferingState>>
{
    private readonly IdentityContext _identityContext;
    public AddOfferingCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddOfferingCommand> validator,
                                    IdentityContext identityContext) : base(context, mapper, validator)
    {
        _identityContext = identityContext;
    }

    public async Task<Validation<Error, OfferingState>> Handle(AddOfferingCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await AddOffering(request, cancellationToken));


    public async Task<Validation<Error, OfferingState>> AddOffering(AddOfferingCommand request, CancellationToken cancellationToken)
    {
        OfferingState entity = Mapper.Map<OfferingState>(request);
        var offeringHistoryId = await AddOfferingHistory(entity);
        UpdatePreSelectedUnitList(entity);
        UpdateUnitOfferedList(entity);
        _ = await Context.AddAsync(entity, cancellationToken);
        await AddApprovers(entity.Id, cancellationToken);
        _ = await Context.SaveChangesAsync(cancellationToken);
        return Success<Error, OfferingState>(entity);
    }
    private async Task<string> AddOfferingHistory(OfferingState entity)
    {
        var offeringVersion = await Context.OfferingHistory.Where(l => l.OfferingID == entity.Id).AsNoTracking().MaxAsync(l => l.OfferingVersion);
        var offeringHistory = Mapper.Map<OfferingHistoryState>(entity);
        offeringHistory.SetOfferingVersion(offeringVersion == null ? 1 : (int)offeringVersion);
        entity.SetOfferingHistoryId(offeringHistory.Id);
        Context.Entry(offeringHistory!).State = EntityState.Added;
        return offeringHistory.Id;
    }
    private void UpdatePreSelectedUnitList(OfferingState entity)
    {
        if (entity.PreSelectedUnitList?.Count > 0)
        {
            foreach (var preSelectedUnit in entity.PreSelectedUnitList!)
            {
                Context.Entry(preSelectedUnit).State = EntityState.Added;
            }
        }
    }
    private void UpdateUnitOfferedList(OfferingState entity)
    {
        if (entity.UnitOfferedList?.Count > 0)
        {
            foreach (var unitOffered in entity.UnitOfferedList!)
            {
                Context.Entry(unitOffered).State = EntityState.Added;
            }
        }
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

public class AddOfferingCommandValidator : AbstractValidator<AddOfferingCommand>
{
    readonly ApplicationContext _context;

    public AddOfferingCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<OfferingState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Offering with id {PropertyValue} already exists");

    }
}
