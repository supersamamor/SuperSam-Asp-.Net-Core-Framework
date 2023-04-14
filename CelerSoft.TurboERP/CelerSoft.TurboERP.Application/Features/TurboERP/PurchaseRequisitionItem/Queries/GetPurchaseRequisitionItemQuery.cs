using CelerSoft.Common.Core.Queries;
using CelerSoft.Common.Utility.Models;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Infrastructure.Data;
using MediatR;
using CelerSoft.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CelerSoft.TurboERP.Application.Features.TurboERP.PurchaseRequisitionItem.Queries;

public record GetPurchaseRequisitionItemQuery : BaseQuery, IRequest<PagedListResponse<PurchaseRequisitionItemState>>;

public class GetPurchaseRequisitionItemQueryHandler : BaseQueryHandler<ApplicationContext, PurchaseRequisitionItemState, GetPurchaseRequisitionItemQuery>, IRequestHandler<GetPurchaseRequisitionItemQuery, PagedListResponse<PurchaseRequisitionItemState>>
{
    public GetPurchaseRequisitionItemQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<PurchaseRequisitionItemState>> Handle(GetPurchaseRequisitionItemQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<PurchaseRequisitionItemState>().Include(l=>l.PurchaseRequisition).Include(l=>l.Product)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
