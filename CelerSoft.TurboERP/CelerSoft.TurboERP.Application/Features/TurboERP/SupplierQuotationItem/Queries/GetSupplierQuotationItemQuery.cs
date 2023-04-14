using CelerSoft.Common.Core.Queries;
using CelerSoft.Common.Utility.Models;
using CelerSoft.TurboERP.Core.TurboERP;
using CelerSoft.TurboERP.Infrastructure.Data;
using MediatR;
using CelerSoft.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CelerSoft.TurboERP.Application.Features.TurboERP.SupplierQuotationItem.Queries;

public record GetSupplierQuotationItemQuery : BaseQuery, IRequest<PagedListResponse<SupplierQuotationItemState>>;

public class GetSupplierQuotationItemQueryHandler : BaseQueryHandler<ApplicationContext, SupplierQuotationItemState, GetSupplierQuotationItemQuery>, IRequestHandler<GetSupplierQuotationItemQuery, PagedListResponse<SupplierQuotationItemState>>
{
    public GetSupplierQuotationItemQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<SupplierQuotationItemState>> Handle(GetSupplierQuotationItemQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<SupplierQuotationItemState>().Include(l=>l.SupplierQuotation).Include(l=>l.Product)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
