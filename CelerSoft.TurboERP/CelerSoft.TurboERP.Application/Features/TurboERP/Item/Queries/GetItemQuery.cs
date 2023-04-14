using CelerSoft.Common.Core.Queries;
using CelerSoft.Common.Utility.Models;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Infrastructure.Data;
using MediatR;
using CelerSoft.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CelerSoft.TurboERP.Application.Features.TurboERP.Item.Queries;

public record GetItemQuery : BaseQuery, IRequest<PagedListResponse<ItemState>>;

public class GetItemQueryHandler : BaseQueryHandler<ApplicationContext, ItemState, GetItemQuery>, IRequestHandler<GetItemQuery, PagedListResponse<ItemState>>
{
    public GetItemQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<ItemState>> Handle(GetItemQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<ItemState>().Include(l=>l.ItemType).Include(l=>l.Unit)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
