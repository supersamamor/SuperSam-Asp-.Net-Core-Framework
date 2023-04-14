using CelerSoft.Common.Core.Queries;
using CelerSoft.Common.Utility.Models;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Infrastructure.Data;
using MediatR;
using CelerSoft.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CelerSoft.TurboERP.Application.Features.TurboERP.Purchase.Queries;

public record GetPurchaseQuery : BaseQuery, IRequest<PagedListResponse<PurchaseState>>;

public class GetPurchaseQueryHandler : BaseQueryHandler<ApplicationContext, PurchaseState, GetPurchaseQuery>, IRequestHandler<GetPurchaseQuery, PagedListResponse<PurchaseState>>
{
    public GetPurchaseQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<PurchaseState>> Handle(GetPurchaseQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<PurchaseState>().Include(l=>l.PurchaseRequisition).Include(l=>l.SupplierQuotation)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
