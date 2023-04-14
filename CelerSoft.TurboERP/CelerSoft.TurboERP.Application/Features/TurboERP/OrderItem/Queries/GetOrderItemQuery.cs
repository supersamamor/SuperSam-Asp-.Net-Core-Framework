using CelerSoft.Common.Core.Queries;
using CelerSoft.Common.Utility.Models;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Infrastructure.Data;
using MediatR;
using CelerSoft.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CelerSoft.TurboERP.Application.Features.TurboERP.OrderItem.Queries;

public record GetOrderItemQuery : BaseQuery, IRequest<PagedListResponse<OrderItemState>>;

public class GetOrderItemQueryHandler : BaseQueryHandler<ApplicationContext, OrderItemState, GetOrderItemQuery>, IRequestHandler<GetOrderItemQuery, PagedListResponse<OrderItemState>>
{
    public GetOrderItemQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<OrderItemState>> Handle(GetOrderItemQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<OrderItemState>().Include(l=>l.Inventory).Include(l=>l.Order)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
