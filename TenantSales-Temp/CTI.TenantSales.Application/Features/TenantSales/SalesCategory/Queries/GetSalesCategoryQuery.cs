using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.TenantSales.Application.Features.TenantSales.SalesCategory.Queries;

public record GetSalesCategoryQuery : BaseQuery, IRequest<PagedListResponse<SalesCategoryState>>;

public class GetSalesCategoryQueryHandler : BaseQueryHandler<ApplicationContext, SalesCategoryState, GetSalesCategoryQuery>, IRequestHandler<GetSalesCategoryQuery, PagedListResponse<SalesCategoryState>>
{
    public GetSalesCategoryQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<SalesCategoryState>> Handle(GetSalesCategoryQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<SalesCategoryState>().Include(l=>l.Tenant)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
