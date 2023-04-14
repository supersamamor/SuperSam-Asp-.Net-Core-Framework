using AutoMapper;
using CelerSoft.Common.Core.Commands;
using CelerSoft.Common.Data;
using CelerSoft.Common.Utility.Validators;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Infrastructure.Data;
using FluentValidation;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

namespace CelerSoft.TurboERP.Application.Features.TurboERP.PurchaseRequisition.Commands;

public record AddPurchaseRequisitionCommand : PurchaseRequisitionState, IRequest<Validation<Error, PurchaseRequisitionState>>;

public class AddPurchaseRequisitionCommandHandler : BaseCommandHandler<ApplicationContext, PurchaseRequisitionState, AddPurchaseRequisitionCommand>, IRequestHandler<AddPurchaseRequisitionCommand, Validation<Error, PurchaseRequisitionState>>
{
	private readonly IdentityContext _identityContext;
    public AddPurchaseRequisitionCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddPurchaseRequisitionCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    public async Task<Validation<Error, PurchaseRequisitionState>> Handle(AddPurchaseRequisitionCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await AddPurchaseRequisition(request, cancellationToken));


	public async Task<Validation<Error, PurchaseRequisitionState>> AddPurchaseRequisition(AddPurchaseRequisitionCommand request, CancellationToken cancellationToken)
	{
		PurchaseRequisitionState entity = Mapper.Map<PurchaseRequisitionState>(request);
		UpdatePurchaseRequisitionItemList(entity);
		_ = await Context.AddAsync(entity, cancellationToken);
		await AddApprovers(entity.Id, cancellationToken);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, PurchaseRequisitionState>(entity);
	}
	
	private void UpdatePurchaseRequisitionItemList(PurchaseRequisitionState entity)
	{
		if (entity.PurchaseRequisitionItemList?.Count > 0)
		{
			foreach (var purchaseRequisitionItem in entity.PurchaseRequisitionItemList!)
			{
				Context.Entry(purchaseRequisitionItem).State = EntityState.Added;
			}
		}
	}
	
	private async Task AddApprovers(string purchaseRequisitionId, CancellationToken cancellationToken)
	{
		var approverList = await Context.ApproverAssignment.Include(l=>l.ApproverSetup).Where(l => l.ApproverSetup.TableName == ApprovalModule.PurchaseRequisition).AsNoTracking().ToListAsync(cancellationToken);
		if (approverList.Count > 0)
		{
			var approvalRecord = new ApprovalRecordState()
			{
				ApproverSetupId = approverList.FirstOrDefault()!.ApproverSetupId,
				DataId = purchaseRequisitionId,
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

public class AddPurchaseRequisitionCommandValidator : AbstractValidator<AddPurchaseRequisitionCommand>
{
    readonly ApplicationContext _context;

    public AddPurchaseRequisitionCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<PurchaseRequisitionState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("PurchaseRequisition with id {PropertyValue} already exists");
        
    }
}
