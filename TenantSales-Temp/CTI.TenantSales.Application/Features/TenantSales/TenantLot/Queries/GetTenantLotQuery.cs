using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.TenantSales.Application.Features.TenantSales.TenantLot.Queries;

public record GetTenantLotQuery : BaseQuery, IRequest<PagedListResponse<TenantLotState>>;

public class GetTenantLotQueryHandler : BaseQueryHandler<ApplicationContext, TenantLotState, GetTenantLotQuery>, IRequestHandler<GetTenantLotQuery, PagedListResponse<TenantLotState>>
{
    public GetTenantLotQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<TenantLotState>> Handle(GetTenantLotQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<TenantLotState>().Include(l=>l.Tenant)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
