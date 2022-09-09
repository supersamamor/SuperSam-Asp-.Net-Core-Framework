using AutoMapper;
using CompanyNamePlaceHolder.Common.Core.Commands;
using CompanyNamePlaceHolder.Common.Data;
using CompanyNamePlaceHolder.Common.Utility.Validators;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.ProjectNamePlaceHolder;
using CompanyNamePlaceHolder.ProjectNamePlaceHolder.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Application.Features.ProjectNamePlaceHolder.MainModule.Commands;

public record AddMainModuleCommand : MainModuleState, IRequest<Validation<Error, MainModuleState>>;

public class AddMainModuleCommandHandler : BaseCommandHandler<ApplicationContext, MainModuleState, AddMainModuleCommand>, IRequestHandler<AddMainModuleCommand, Validation<Error, MainModuleState>>
{
    private readonly IdentityContext _identityContext;
    public AddMainModuleCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddMainModuleCommand> validator,
                                    IdentityContext identityContext) : base(context, mapper, validator)
    {
        _identityContext = identityContext;
    }

    public async Task<Validation<Error, MainModuleState>> Handle(AddMainModuleCommand request, CancellationToken cancellationToken) =>
        await Validators.ValidateTAsync(request, cancellationToken).BindT(
            async request => await AddMainModule(request, cancellationToken));


    public async Task<Validation<Error, MainModuleState>> AddMainModule(AddMainModuleCommand request, CancellationToken cancellationToken)
    {
        MainModuleState entity = Mapper.Map<MainModuleState>(request);
        UpdateSubDetailItemList(entity);
        UpdateSubDetailListList(entity);
        _ = await Context.AddAsync(entity, cancellationToken);
        await AddApprovers(entity.Id, cancellationToken);
        _ = await Context.SaveChangesAsync(cancellationToken);
        return Success<Error, MainModuleState>(entity);
    }

    private void UpdateSubDetailItemList(MainModuleState entity)
    {
        if (entity.SubDetailItemList?.Count > 0)
        {
            foreach (var subDetailItem in entity.SubDetailItemList!)
            {
                Context.Entry(subDetailItem).State = EntityState.Added;
            }
        }
    }
    private void UpdateSubDetailListList(MainModuleState entity)
    {
        if (entity.SubDetailListList?.Count > 0)
        {
            foreach (var subDetailList in entity.SubDetailListList!)
            {
                Context.Entry(subDetailList).State = EntityState.Added;
            }
        }
    }

    private async Task AddApprovers(string mainModuleId, CancellationToken cancellationToken)
    {
        var approverList = await Context.ApproverAssignment.Include(l => l.ApproverSetup).Where(l => l.ApproverSetup.TableName == ApprovalModule.MainModule).AsNoTracking().ToListAsync(cancellationToken);
        if (approverList.Count > 0)
        {
            var approvalRecord = new ApprovalRecordState()
            {
                ApproverSetupId = approverList.FirstOrDefault()!.ApproverSetupId,
                DataId = mainModuleId,
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

public class AddMainModuleCommandValidator : AbstractValidator<AddMainModuleCommand>
{
    readonly ApplicationContext _context;

    public AddMainModuleCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<MainModuleState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("MainModule with id {PropertyValue} already exists");

    }
}
