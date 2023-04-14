using CelerSoft.Common.Core.Queries;
using CelerSoft.Common.Utility.Models;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Infrastructure.Data;
using MediatR;
using CelerSoft.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CelerSoft.TurboERP.Application.Features.TurboERP.Inventory.Queries;

public record GetInventoryQuery : BaseQuery, IRequest<PagedListResponse<InventoryState>>;

public class GetInventoryQueryHandler : BaseQueryHandler<ApplicationContext, InventoryState, GetInventoryQuery>, IRequestHandler<GetInventoryQuery, PagedListResponse<InventoryState>>
{
    public GetInventoryQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<InventoryState>> Handle(GetInventoryQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<InventoryState>().Include(l=>l.PurchaseItem).Include(l=>l.Product)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
