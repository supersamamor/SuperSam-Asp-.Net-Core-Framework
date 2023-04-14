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

public record EditOrderCommand : OrderState, IRequest<Validation<Error, OrderState>>;

public class EditOrderCommandHandler : BaseCommandHandler<ApplicationContext, OrderState, EditOrderCommand>, IRequestHandler<EditOrderCommand, Validation<Error, OrderState>>
{
    public EditOrderCommandHandler(ApplicationContext context,
                                     IMapper mapper,
                                     CompositeValidator<EditOrderCommand> validator) : base(context, mapper, validator)
    {
    }

    public async Task<Validation<Error, OrderState>> Handle(EditOrderCommand request, CancellationToken cancellationToken) =>
		await Validators.ValidateTAsync(request, cancellationToken).BindT(
			async request => await EditOrder(request, cancellationToken));


	public async Task<Validation<Error, OrderState>> EditOrder(EditOrderCommand request, CancellationToken cancellationToken)
	{
		var entity = await Context.Order.Where(l => l.Id == request.Id).SingleAsync(cancellationToken: cancellationToken);
		Mapper.Map(request, entity);
		await UpdateOrderItemList(entity, request, cancellationToken);
		Context.Update(entity);
		_ = await Context.SaveChangesAsync(cancellationToken);
		return Success<Error, OrderState>(entity);
	}
	
	private async Task UpdateOrderItemList(OrderState entity, EditOrderCommand request, CancellationToken cancellationToken)
	{
		IList<OrderItemState> orderItemListForDeletion = new List<OrderItemState>();
		var queryOrderItemForDeletion = Context.OrderItem.Where(l => l.OrderId == request.Id).AsNoTracking();
		if (entity.OrderItemList?.Count > 0)
		{
			queryOrderItemForDeletion = queryOrderItemForDeletion.Where(l => !(entity.OrderItemList.Select(l => l.Id).ToList().Contains(l.Id)));
		}
		orderItemListForDeletion = await queryOrderItemForDeletion.ToListAsync(cancellationToken: cancellationToken);
		foreach (var orderItem in orderItemListForDeletion!)
		{
			Context.Entry(orderItem).State = EntityState.Deleted;
		}
		if (entity.OrderItemList?.Count > 0)
		{
			foreach (var orderItem in entity.OrderItemList.Where(l => !orderItemListForDeletion.Select(l => l.Id).Contains(l.Id)))
			{
				if (await Context.NotExists<OrderItemState>(x => x.Id == orderItem.Id, cancellationToken: cancellationToken))
				{
					Context.Entry(orderItem).State = EntityState.Added;
				}
				else
				{
					Context.Entry(orderItem).State = EntityState.Modified;
				}
			}
		}
	}
	
}

public class EditOrderCommandValidator : AbstractValidator<EditOrderCommand>
{
    readonly ApplicationContext _context;

    public EditOrderCommandValidator(ApplicationContext context)
    {
        _context = context;
		RuleFor(x => x.Id).MustAsync(async (id, cancellation) => await _context.Exists<OrderState>(x => x.Id == id, cancellationToken: cancellation))
                          .WithMessage("Order with id {PropertyValue} does not exists");
        RuleFor(x => x.Code).MustAsync(async (request, code, cancellation) => await _context.NotExists<OrderState>(x => x.Code == code && x.Id != request.Id, cancellationToken: cancellation)).WithMessage("Order with code {PropertyValue} already exists");
	
    }
}
