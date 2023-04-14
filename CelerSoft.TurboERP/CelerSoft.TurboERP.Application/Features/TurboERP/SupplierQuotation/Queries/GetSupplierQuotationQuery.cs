using CelerSoft.Common.Core.Queries;
using CelerSoft.Common.Utility.Models;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Infrastructure.Data;
using MediatR;
using CelerSoft.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CelerSoft.TurboERP.Application.Features.TurboERP.SupplierQuotation.Queries;

public record GetSupplierQuotationQuery : BaseQuery, IRequest<PagedListResponse<SupplierQuotationState>>;

public class GetSupplierQuotationQueryHandler : BaseQueryHandler<ApplicationContext, SupplierQuotationState, GetSupplierQuotationQuery>, IRequestHandler<GetSupplierQuotationQuery, PagedListResponse<SupplierQuotationState>>
{
    public GetSupplierQuotationQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<SupplierQuotationState>> Handle(GetSupplierQuotationQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<SupplierQuotationState>().Include(l=>l.Supplier).Include(l=>l.PurchaseRequisition)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
