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

namespace CelerSoft.TurboERP.Application.Features.TurboERP.Order.Commands;

public record AddOrderCommand : OrderState, IRequest<Validation<Error, OrderState>>;

public class AddOrderCommandHandler : BaseCommandHandler<ApplicationContext, OrderState, AddOrderCommand>, IRequestHandler<AddOrderCommand, Validation<Error, OrderState>>
{
	private readonly IdentityContext _identityContext;
    public AddOrderCommandHandler(ApplicationContext context,
                                    IMapper mapper,
                                    CompositeValidator<AddOrderCommand> validator,
									IdentityContext identityContext) : base(context, mapper, validator)
    {
		_identityContext = identityContext;
    }

    public async Task<Validation<Error, OrderState>> Handle(AddOrderCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await AddOrder(request, cancellationToken));


	public async Task<Validation<Error, OrderState>> AddOrder(AddOrderCommand request, CancellationToken cancellationToken)
	{
		OrderState entity = Mapper.Map<OrderState>(request);
		UpdateOrderItemList(entity);
		_ = await Context.AddAsync(entity, cancellationToken);
		await AddApprovers(entity.Id, cancellationToken);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, OrderState>(entity);
	}
	
	private void UpdateOrderItemList(OrderState entity)
	{
		if (entity.OrderItemList?.Count > 0)
		{
			foreach (var orderItem in entity.OrderItemList!)
			{
				Context.Entry(orderItem).State = EntityState.Added;
			}
		}
	}
	
	private async Task AddApprovers(string orderId, CancellationToken cancellationToken)
	{
		var approverList = await Context.ApproverAssignment.Include(l=>l.ApproverSetup).Where(l => l.ApproverSetup.TableName == ApprovalModule.Order).AsNoTracking().ToListAsync(cancellationToken);
		if (approverList.Count > 0)
		{
			var approvalRecord = new ApprovalRecordState()
			{
				ApproverSetupId = approverList.FirstOrDefault()!.ApproverSetupId,
				DataId = orderId,
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

public class AddOrderCommandValidator : AbstractValidator<AddOrderCommand>
{
    readonly ApplicationContext _context;

    public AddOrderCommandValidator(ApplicationContext context)
    {
        _context = context;

        RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.NotExists<OrderState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Order with id {PropertyValue} already exists");
        RuleFor(x => x.Code).MustAsync(async (code, cancellation) => await _context.NotExists<OrderState>(x => x.Code == code, cancellationToken: cancellation)).WithMessage("Order with code {PropertyValue} already exists");
	
    }
}
