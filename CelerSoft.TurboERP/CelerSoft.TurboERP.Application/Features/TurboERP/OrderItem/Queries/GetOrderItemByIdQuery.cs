using CelerSoft.Common.Core.Queries;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CelerSoft.TurboERP.Application.Features.TurboERP.OrderItem.Queries;

public record GetOrderItemByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<OrderItemState>>;

public class GetOrderItemByIdQueryHandler : BaseQueryByIdHandler<ApplicationContext, OrderItemState, GetOrderItemByIdQuery>, IRequestHandler<GetOrderItemByIdQuery, Option<OrderItemState>>
{
    public GetOrderItemByIdQueryHandler(ApplicationContext context) : base(context)
    {
    }
	
	public override async Task<Option<OrderItemState>> Handle(GetOrderItemByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.OrderItem.Include(l=>l.Inventory).Include(l=>l.Order)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
