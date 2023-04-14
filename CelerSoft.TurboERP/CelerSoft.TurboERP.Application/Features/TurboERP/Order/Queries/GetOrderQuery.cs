using CelerSoft.Common.Core.Queries;
using CelerSoft.Common.Utility.Models;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Infrastructure.Data;
using MediatR;
using CelerSoft.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CelerSoft.TurboERP.Application.Features.TurboERP.Order.Queries;

public record GetOrderQuery : BaseQuery, IRequest<PagedListResponse<OrderState>>;

public class GetOrderQueryHandler : BaseQueryHandler<ApplicationContext, OrderState, GetOrderQuery>, IRequestHandler<GetOrderQuery, PagedListResponse<OrderState>>
{
    public GetOrderQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<OrderState>> Handle(GetOrderQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<OrderState>().Include(l=>l.Customer)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
