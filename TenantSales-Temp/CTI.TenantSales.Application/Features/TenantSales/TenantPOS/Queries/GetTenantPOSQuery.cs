using CTI.Common.Core.Queries;
using CTI.Common.Utility.Models;
using CTI.TenantSales.Core.TenantSales;
using CTI.TenantSales.Infrastructure.Data;
using MediatR;
using CTI.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CTI.TenantSales.Application.Features.TenantSales.TenantPOS.Queries;

public record GetTenantPOSQuery : BaseQuery, IRequest<PagedListResponse<TenantPOSState>>;

public class GetTenantPOSQueryHandler : BaseQueryHandler<ApplicationContext, TenantPOSState, GetTenantPOSQuery>, IRequestHandler<GetTenantPOSQuery, PagedListResponse<TenantPOSState>>
{
    public GetTenantPOSQueryHandler(ApplicationContext context) : base(context)
    {
    }
	public override async Task<PagedListResponse<TenantPOSState>> Handle(GetTenantPOSQuery request, CancellationToken cancellationToken = default) =>
		await Context.Set<TenantPOSState>().Include(l=>l.Tenant)
		.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
			request.SortColumn, request.SortOrder,
			request.PageNumber, request.PageSize,
			cancellationToken);	
}
