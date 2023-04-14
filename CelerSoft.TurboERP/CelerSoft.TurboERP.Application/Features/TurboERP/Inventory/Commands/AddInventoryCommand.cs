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

namespace CelerSoft.TurboERP.Application.Features.TurboERP.Inventory.Commands;

public record AddInventoryCommand : InventoryState, IRequest<Validation<Error, InventoryState>>;

public class AddInventoryCommandHandler : BaseCommandHandler<ApplicationContext, InventoryState, AddInventoryCommand>, IRequestHandler<AddInventoryCommand, Validation<Error, InventoryState>>
{
	private readonly IdentityContext _identityContext;
    public AddInventoryCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddInventoryCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    public async Task<Validation<Error, InventoryState>> Handle(AddInventoryCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await AddInventory(request, cancellationToken));


	public async Task<Validation<Error, InventoryState>> AddInventory(AddInventoryCommand request, CancellationToken cancellationToken)
	{
		InventoryState entity = Mapper.Map<InventoryState>(request);
		UpdateInventoryHistoryList(entity);
		UpdateOrderItemList(entity);
		_ = await Context.AddAsync(entity, cancellationToken);
		await AddApprovers(entity.Id, cancellationToken);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, InventoryState>(entity);
	}
	
	private void UpdateInventoryHistoryList(InventoryState entity)
	{
		if (entity.InventoryHistoryList?.Count > 0)
		{
			foreach (var inventoryHistory in entity.InventoryHistoryList!)
			{
				Context.Entry(inventoryHistory).State = EntityState.Added;
			}
		}
	}
	private void UpdateOrderItemList(InventoryState entity)
	{
		if (entity.OrderItemList?.Count > 0)
		{
			foreach (var orderItem in entity.OrderItemList!)
			{
				Context.Entry(orderItem).State = EntityState.Added;
			}
		}
	}
	
	private async Task AddApprovers(string inventoryId, CancellationToken cancellationToken)
	{
		var approverList = await Context.ApproverAssignment.Include(l=>l.ApproverSetup).Where(l => l.ApproverSetup.TableName == ApprovalModule.Inventory).AsNoTracking().ToListAsync(cancellationToken);
		if (approverList.Count > 0)
		{
			var approvalRecord = new ApprovalRecordState()
			{
				ApproverSetupId = approverList.FirstOrDefault()!.ApproverSetupId,
				DataId = inventoryId,
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

public class AddInventoryCommandValidator : AbstractValidator<AddInventoryCommand>
{
    readonly ApplicationContext _context;

    public AddInventoryCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<InventoryState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Inventory with id {PropertyValue} already exists");
        
    }
}
