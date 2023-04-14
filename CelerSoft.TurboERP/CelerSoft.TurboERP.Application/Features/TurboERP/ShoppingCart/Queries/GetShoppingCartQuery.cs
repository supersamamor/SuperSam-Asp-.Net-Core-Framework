using CelerSoft.Common.Core.Queries;
using CelerSoft.Common.Utility.Models;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Infrastructure.Data;
using MediatR;
using CelerSoft.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CelerSoft.TurboERP.Application.Features.TurboERP.ShoppingCart.Queries;

public record GetShoppingCartQuery : BaseQuery, IRequest<PagedListResponse<ShoppingCartState>>;

public class GetShoppingCartQueryHandler : BaseQueryHandler<ApplicationContext, ShoppingCartState, GetShoppingCartQuery>, IRequestHandler<GetShoppingCartQuery, PagedListResponse<ShoppingCartState>>
{
    public GetShoppingCartQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<ShoppingCartState>> Handle(GetShoppingCartQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<ShoppingCartState>().Include(l=>l.Inventory)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
