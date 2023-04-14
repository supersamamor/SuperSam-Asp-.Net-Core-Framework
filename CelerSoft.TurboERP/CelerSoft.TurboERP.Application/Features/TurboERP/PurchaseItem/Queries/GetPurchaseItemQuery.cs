using CelerSoft.Common.Core.Queries;
using CelerSoft.Common.Utility.Models;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Infrastructure.Data;
using MediatR;
using CelerSoft.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CelerSoft.TurboERP.Application.Features.TurboERP.PurchaseItem.Queries;

public record GetPurchaseItemQuery : BaseQuery, IRequest<PagedListResponse<PurchaseItemState>>;

public class GetPurchaseItemQueryHandler : BaseQueryHandler<ApplicationContext, PurchaseItemState, GetPurchaseItemQuery>, IRequestHandler<GetPurchaseItemQuery, PagedListResponse<PurchaseItemState>>
{
    public GetPurchaseItemQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<PurchaseItemState>> Handle(GetPurchaseItemQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<PurchaseItemState>().Include(l=>l.Product).Include(l=>l.SupplierQuotationItem)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
