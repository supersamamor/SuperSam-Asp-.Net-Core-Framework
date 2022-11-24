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
using CTI.ELMS.Application.Repositories;

namespace CTI.ELMS.Application.Features.ELMS.Offering.Commands;

public record AddOfferingCommand : OfferingState, IRequest<Validation<Error, OfferingState>>;

public class AddOfferingCommandHandler : BaseCommandHandler<ApplicationContext, OfferingState, AddOfferingCommand>, IRequestHandler<AddOfferingCommand, Validation<Error, OfferingState>>
{
    private readonly IdentityContext _identityContext;
    private readonly OfferingRepository _offeringRepository;
    public AddOfferingCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddOfferingCommand> validator,
                                    IdentityContext identityContext,
                                    OfferingRepository offeringRepository) : base(context, mapper, validator)
    {
        _identityContext = identityContext;
        _offeringRepository = offeringRepository;
    }

    public async Task<Validation<Error, OfferingState>> Handle(AddOfferingCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await AddOffering(request, cancellationToken));


    public async Task<Validation<Error, OfferingState>> AddOffering(AddOfferingCommand request, CancellationToken cancellationToken)
    {
        OfferingState entity = Mapper.Map<OfferingState>(request);
        entity.SetOfferSheetId(await _offeringRepository.GenerateOfferSheetIdAsync());
        _offeringRepository.AddPreSelectedUnitList(entity);
        entity.SetOfferSheetPerProjectCounter(await _offeringRepository.GetOfferSheetPerProjectCounter(entity.ProjectID!));
        await _offeringRepository.AutoCalculateOfferSheetFields(entity);
        var offeringHistoryId = await _offeringRepository.AddOfferingHistory(entity);
        _offeringRepository.AddUnitOfferedList(entity, offeringHistoryId);
        _ = await Context.AddAsync(entity, cancellationToken);
        await AddApprovers(entity.Id, cancellationToken);
        await _offeringRepository.UpdateLeadLatestUpdateDate(entity.LeadID!);
        _ = await Context.SaveChangesAsync(cancellationToken);
        return Success<Error, OfferingState>(entity);
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
