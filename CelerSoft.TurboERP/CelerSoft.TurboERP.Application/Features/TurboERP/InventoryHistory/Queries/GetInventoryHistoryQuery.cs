using CelerSoft.Common.Core.Queries;
using CelerSoft.Common.Utility.Models;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Infrastructure.Data;
using MediatR;
using CelerSoft.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CelerSoft.TurboERP.Application.Features.TurboERP.InventoryHistory.Queries;

public record GetInventoryHistoryQuery : BaseQuery, IRequest<PagedListResponse<InventoryHistoryState>>;

public class GetInventoryHistoryQueryHandler : BaseQueryHandler<ApplicationContext, InventoryHistoryState, GetInventoryHistoryQuery>, IRequestHandler<GetInventoryHistoryQuery, PagedListResponse<InventoryHistoryState>>
{
    public GetInventoryHistoryQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<InventoryHistoryState>> Handle(GetInventoryHistoryQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<InventoryHistoryState>().Include(l=>l.Inventory)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
